﻿using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace Client.Web.Services
{
    public class JwtAuthorizationHandler : DelegatingHandler
    {
        private readonly AuthenticationStateProvider _authStateProvider;

        public JwtAuthorizationHandler(AuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = authStateProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                var token = user.FindFirst("access_token")?.Value;
                if (!string.IsNullOrEmpty(token))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
