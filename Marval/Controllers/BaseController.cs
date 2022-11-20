using AutoMapper;
using Marval.HelperInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace Marval.Controllers
{
    public class BaseController<T> : Controller where T : BaseController<T>
    {
        private ILogger<T> logger;
        private IActionContextAccessor actionContextAccessor;
        private IHttpContextAccessor httpContextAccessor;
        private IMapper mapper;
        private IAppUtil appUtil;

        protected ILogger<T> _logger => logger ?? (logger = HttpContext?.RequestServices.GetService<ILogger<T>>());
        protected IActionContextAccessor _actionContextAccessor => actionContextAccessor ?? (actionContextAccessor = HttpContext?.RequestServices.GetService<IActionContextAccessor>());
        protected IHttpContextAccessor _httpContextAccessor => httpContextAccessor ?? (httpContextAccessor = HttpContext?.RequestServices.GetService<IHttpContextAccessor>());
        protected IMapper _mapper => mapper ?? (mapper = HttpContext?.RequestServices.GetService<IMapper>());
        protected IAppUtil _appUtil => appUtil ?? (appUtil = HttpContext?.RequestServices.GetService<IAppUtil>());


        [NonAction]
        public string GetUserName()
        {
            string username = "Guest";
            try
            {
                //Check if there a logged in user
            }
            catch (Exception) { }
            return username;
        }

        [NonAction]
        public string GetIPAddress()
        {
            string ipAddress = "127.0.0.0";
            try
            {
                var ip = _actionContextAccessor.ActionContext?.HttpContext.Connection.RemoteIpAddress;
                if(ip != null)
                {
                    ipAddress = ip.ToString();
                }
            }
            catch (Exception) { }
            return ipAddress;
        }

    }
}
