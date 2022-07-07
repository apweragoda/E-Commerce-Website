using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
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

namespace Web_Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc(options => options.EnableEndpointRouting = false);
            var connectionString = _configuration.GetConnectionString("FlyBuyDb");
            services.AddDbContext<FlyBuyDbContext>(opt => opt.UseSqlServer(connectionString));

            var appSettingsSection = _configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

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
            app.UseMvc();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
