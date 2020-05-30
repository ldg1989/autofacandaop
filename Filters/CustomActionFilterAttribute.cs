using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoFacAop
{
    /// <summary>
    /// Action的Filter`
    /// </summary>
    public class CustomActionFilterAttribute : Attribute, IActionFilter
    {
        private ILogger<CustomActionFilterAttribute> _logger = null;
        public CustomActionFilterAttribute(ILogger<CustomActionFilterAttribute> logger)
        {
            this._logger = logger;
        } 
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //context.HttpContext.Response.WriteAsync("ActionFilter Executed!");
            Console.WriteLine("ActionFilter Executed!");
            //this._logger.LogDebug("ActionFilter Executed!");
        } 
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //context.HttpContext.Response.WriteAsync("ActionFilter Executing!");
            Console.WriteLine("ActionFilter Executing!");
            //this._logger.LogDebug("ActionFilter Executing!");
        }
    }

    /// <summary>
    /// 标记到Controller
    /// </summary>
    public class CustomControllerActionFilterAttribute : Attribute, IActionFilter
    {
        private ILogger<CustomControllerActionFilterAttribute> _logger = null;
        public CustomControllerActionFilterAttribute(ILogger<CustomControllerActionFilterAttribute> logger)
        {
            this._logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //context.HttpContext.Response.WriteAsync("ActionFilter Executed!");
            Console.WriteLine("ActionFilter Executed!");
            //this._logger.LogDebug("ActionFilter Executed!");
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //context.HttpContext.Response.WriteAsync("ActionFilter Executing!");
            Console.WriteLine("ActionFilter Executing!");
            //this._logger.LogDebug("ActionFilter Executing!");
        }
    }

    /// <summary>
    ///  注册到全局
    /// </summary>
    public class CustomGlobalActionFilterAttribute : Attribute, IActionFilter
    {
        private ILogger<CustomGlobalActionFilterAttribute> _logger = null;
        public CustomGlobalActionFilterAttribute(ILogger<CustomGlobalActionFilterAttribute> logger)
        {
            this._logger = logger;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //context.HttpContext.Response.WriteAsync("ActionFilter Executed!");
            Console.WriteLine("ActionFilter Executed!");
            //this._logger.LogDebug("ActionFilter Executed!");
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //context.HttpContext.Response.WriteAsync("ActionFilter Executing!");
            Console.WriteLine("ActionFilter Executing!");
            //this._logger.LogDebug("ActionFilter Executing!");
        }
    }
}
