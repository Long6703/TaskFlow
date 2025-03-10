using AutoMapper;
using Client.Web.IServices;
using Client.Web.Model;
using Client.Web.Model.DTO;
using Client.Web.Services;
using Microsoft.AspNetCore.Components;

namespace Client.Web.Pages
{
    public partial class Register
    {
        protected RegisterViewModel registerModel = new();
        [Inject] protected NavigationManager Navigation { get; set; } = default!;
        [Inject] protected IEmailService EmailService { get; set; } = default!;
        [Inject] protected IAuthService AuthService { get; set; } = default!;
        [Inject] IMapper mapper { get; set; } = default!;

        private bool _visible = false;
        private bool isVerified = false;
        private bool showErrors = false;
        private string verificationCode = "";
        private string enteredCode = "";

        protected async Task HandleRegister()
        {
            verificationCode = await EmailService.SendEmailAsync(registerModel.Email);
            _visible = true;
            isVerified = false;
        }

        private async void VerifyCode()
        {
            if (enteredCode == verificationCode)
            {
                var userCreateModel = mapper.Map<UserCreateDTO>(registerModel);
                if (await AuthService.Register(userCreateModel))
                {
                    isVerified = true;
                }
            }
            else
            {
                isVerified = false;
                showErrors = true;
            }
        }

        private void NavigateToLogin()
        {
            Navigation.NavigateTo("/login");
        }
    }
}