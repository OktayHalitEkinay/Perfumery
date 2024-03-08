using System.Text;
using Newtonsoft.Json;
using Business.Abstract;
using Microsoft.AspNetCore.Http;

namespace Business.Middlewares
{
    public class CheckUserSessionMiddleware
    {
        private const int DEFAULT_USER_ID = 1;
        private readonly RequestDelegate _next;
        private readonly IUserDetailService _userDetailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CheckUserSessionMiddleware(RequestDelegate next, IUserDetailService userDetailService, IHttpContextAccessor httpContextAccessor)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _userDetailService = userDetailService ?? throw new ArgumentNullException(nameof(userDetailService));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task Invoke(HttpContext context)
        {
            if (_httpContextAccessor.HttpContext?.Session == null)
            {
                throw new InvalidOperationException("Session unavailable.");
            }

            var session = _httpContextAccessor.HttpContext.Session;
            if (!session.TryGetValue("UserDetail", out byte[] userIdByteValue))
            {
                var defaultUserDetail = _userDetailService.GetById(DEFAULT_USER_ID).Data;
                var userDetailJson = JsonConvert.SerializeObject(defaultUserDetail);
                session.Set("UserDetail", Encoding.ASCII.GetBytes(userDetailJson));
            }

            await _next(context);
        }
    }

}
