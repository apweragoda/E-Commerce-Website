namespace Web_Api.Services
{
    public interface IMailService
    {
        void SendMessage(string to, string subject, string body);
    }
}