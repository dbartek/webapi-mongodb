using FluentValidation;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;

namespace SimpleWebApi.Web.Attributes
{
    public class ErrorFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            base.OnException(context);

            if (context.Exception is HttpException)
            {
                var httpException = context.Exception as HttpException;
                context.Response = context.Request.CreateErrorResponse((HttpStatusCode)httpException.GetHttpCode(), context.Exception.Message);
            }
            else if (context.Exception is ValidationException)
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, PrepareErrors(context.Exception as ValidationException));
            }
            else
            {
                context.Response = context.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "An error has occurred.");
            }
        }

        private static ModelStateDictionary PrepareErrors(ValidationException exception)
        {
            var errors = new ModelStateDictionary();

            foreach (var item in exception.Errors)
            {
                errors.AddModelError(item.PropertyName, item.ErrorMessage);
            }

            return errors;
        }
    }
}