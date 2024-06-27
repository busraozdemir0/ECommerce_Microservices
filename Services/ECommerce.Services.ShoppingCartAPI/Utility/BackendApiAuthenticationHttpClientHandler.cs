using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace ECommerce.Services.ShoppingCartAPI.Utility
{
    // Bu sinifin temel islevi; HTTP isteklerine otomatik olarak bir "Bearer" yetkilendirme basligi eklemektir. Bu sayede yetkilendirilmis istekler gönderebiliriz.
    public class BackendApiAuthenticationHttpClientHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _accessor;

        public BackendApiAuthenticationHttpClientHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _accessor.HttpContext.GetTokenAsync("access_token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);  
        }
    }
}
