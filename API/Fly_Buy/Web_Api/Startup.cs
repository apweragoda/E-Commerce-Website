using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Data_Access_Layer;
using Data_Access_Layer.Repository;
using Business_Logic_Layer;
using Serilog;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Http;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Web_Api
{
    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly string defaultPolicy = "default";

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors((setup) =>
            {
                setup.AddPolicy(defaultPolicy, (options) =>
                {
                    options.WithOrigins("http://localhost:5000", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });

            services.AddMvc(options => options.EnableEndpointRouting = false);

            var connectionString = configuration.GetConnectionString("FlyBuyDb");
            services.AddDbContext<FlyBuyDbContext>(opt => opt.UseSqlServer(connectionString));

            var appSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // JWT
            var appSettings = appSettingsSection.Get<AppSettings>();
            var jwtKey = Encoding.ASCII.GetBytes(appSettings.JWTkey);
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt => {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSingleton<AppSettings>();
            services.AddTransient<IMailService, NullMailService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<IShippingRepository, ShippingRepository>();
            services.AddScoped<IBillRepository, BillRepository>();

            services.AddControllers().AddNewtonsoftJson(cfg => cfg.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            RegisterService.ConfigureServices(services);
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IProductBLL, ProductBLL>();
            services.AddScoped<IUserBLL, UserBLL>();
            services.AddScoped<IOrderBLL, OrderBLL>();
            services.AddScoped<IBillBLL, BillBLL>();
            services.AddScoped<IShippingBLL, ShippingBLL>();

            
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-local-development");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseCors(defaultPolicy);

            app.UseMvc();

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async ctx => 
                {
                    await ctx.Response.WriteAsync("API Initiated");
                });
            });
        }
    }
}
