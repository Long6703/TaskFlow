using Client.Web.IServices;
using Client.Web.Model;

namespace Client.Web.Services
{
    public class EmailService : BaseApiService ,IEmailService
    {
        public EmailService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public async Task<string> SendEmailAsync(string email)
        {
            string endpoint = $"api/Verify/verify-email?email={email}";
            return await GetAsync<string>(endpoint);
        }
    }
}
