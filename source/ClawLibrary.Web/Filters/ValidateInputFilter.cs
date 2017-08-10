using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ClawLibrary.Web.Filters
{
    public class ValidateInputFilter : IActionFilter
    {
        #region
        //private readonly ILogger _logger;
        #endregion

        #region Constructor
        public ValidateInputFilter()
        {
            //_logger = logger.ForContext<ValidateInputFilter>();
        }
        #endregion

        #region IActionFilter Implementation
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            //using (LogContext.PushProperties(BuildIdentityEnrichers(context.HttpContext.User)))
            //{
            //    _logger.Warning("Model validation failed for {@Input} with validation {@Errors}",
            //        context.ActionArguments,
            //        context.ModelState?
            //            .SelectMany(kvp => kvp.Value.Errors)
            //            .Select(e => e.ErrorMessage));
            //}

            context.Result = new BadRequestObjectResult(
                from kvp in context.ModelState
                from e in kvp.Value.Errors
                let k = kvp.Key
                select new ValidationError(ValidationError.Type.Input, k, e.ErrorMessage));
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // This filter doesn't do anything post action.
        }
        #endregion
    }

    public class ValidationError
    {
        #region Constants
        public static class Type
        {
            public static string Input = "input";
        }
        #endregion

        #region Properties
        public string ErrorType { get; set; }
        public object Source { get; set; }
        public string ErrorMessage { get; set; }
        #endregion

        #region Constructor
        public ValidationError()
        {
        }

        public ValidationError(string type, object source, string message)
        {
            ErrorType = type;
            Source = source;
            ErrorMessage = message;
        }
        #endregion
    }
}
