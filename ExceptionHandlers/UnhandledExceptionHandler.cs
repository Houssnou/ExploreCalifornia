using ExploreCalifornia.HttpActionResults;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace ExploreCalifornia.ExceptionHandlers
{
    public class UnhandledExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            base.Handle(context);
#if DEBUG
            var content = JsonConvert.SerializeObject(context.Exception);
#else
            var content = @"{""message"":""Ooops, something unexpected went wrong.""}";
#endif

            context.Result = new ErrorContentResult(content, "application/json", context.Request);
        }
    }
}