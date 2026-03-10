using GameExchange.Communication.Response;
using GameExchange.Excptions;
using GameExchange.Excptions.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameExchange.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is GameExchangeException gameExchangeException)
                HandleProjectException(gameExchangeException, context);
            else
                ThrowUnknowException(context);
        }

        private static void HandleProjectException(GameExchangeException myRecipeBookException, ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)myRecipeBookException.GetStatusCode();
            context.Result = new ObjectResult(new ResponseErrorJson(myRecipeBookException.GetErrorMessages()));
        }

        private static void ThrowUnknowException(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson(ResourceMessagesException.UNKNOW_ERROR));
        }
    }
}
