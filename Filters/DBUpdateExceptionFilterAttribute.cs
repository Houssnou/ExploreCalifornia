using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace ExploreCalifornia.Filters
{
    public class DBUpdateExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (!(context.Exception is DbUpdateException)) return;

            var sqlException = context.Exception?.InnerException?.InnerException as SqlException;

            if (sqlException?.Number == 2627)
                context.Response = new HttpResponseMessage(HttpStatusCode.Conflict);

            context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);

        }
    }
}