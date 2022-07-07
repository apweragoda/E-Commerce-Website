namespace Business_Logic_Layer.Services
{
    public interface IMailService
    {
        void SendMessage(string to, string subject, string body);
    }
}