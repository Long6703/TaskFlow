namespace Client.Web.IServices
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(string email);
    }
}
