using KissLog;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreIdentity.Extensions
{
    public class AuditoriaFilter : IActionFilter
    {
        private readonly IKLogger _logger;

        public AuditoriaFilter(IKLogger logger)
        {
            _logger = logger;
        }

        //Before
        public void OnActionExecuting(ActionExecutingContext context){}

        //After
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var message = context.HttpContext.User.Identity.Name + " Acessou: " +
                    context.HttpContext.Request.GetDisplayUrl();

                _logger.Info(message);
            }
        }
    }
}
