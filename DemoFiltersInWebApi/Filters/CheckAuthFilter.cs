using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoFiltersInWebApi.Filters
{
    public class CheckAuthFilter : IActionFilter
    {
        bool State;
        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Append("AuthenticationState", State.ToString());
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var auth = context.HttpContext.Request.Headers.Authorization;
            if (auth.Count != 0)
            {
                context.HttpContext.Request.Headers["AuthenticationState"] = "true";
                State = true;
            }
            else
            {
                context.HttpContext.Request.Headers["AuthenticationState"] = "false";
                State = false;
            }
        }
    }
}
