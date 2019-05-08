using DAL.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace WebAPI.ExceptionHandlers
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception is NotFoundException || context.Exception is EntityModelException)
            {
                context.Result = new GlobalErrorResult()
                {
                    HttpStatusCode = HttpStatusCode.BadRequest,
                    RequestMessage = context.ExceptionContext.Request,
                    Content = context.Exception.Message
                };
            }
            else if(context.Exception is DBConcurrencyException)
            {
                context.Result = new GlobalErrorResult()
                {
                    HttpStatusCode = HttpStatusCode.Conflict,
                    RequestMessage = context.ExceptionContext.Request,
                    Content = "Can't update entity, because it has already changed in db."
                };
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                context.Result = new GlobalErrorResult()
                {
                    HttpStatusCode = HttpStatusCode.Unauthorized,
                    RequestMessage = context.ExceptionContext.Request,
                    Content = "Can't identify user."
                };
            }
            else
            {
                context.Result = new GlobalErrorResult()
                {
                    HttpStatusCode = HttpStatusCode.InternalServerError,
                    RequestMessage = context.ExceptionContext.Request,
                    Content = "Error occurred in server part, contact with administration!"
                };
            }
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return true;
        }

        private class GlobalErrorResult : IHttpActionResult
        {
            public HttpStatusCode HttpStatusCode { get; set; }

            public HttpRequestMessage RequestMessage { private get; set; }

            public string Content { private get; set; }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage(HttpStatusCode)
                {
                    Content = new StringContent(Content),
                    RequestMessage = RequestMessage
                };

                return Task.FromResult(response);
            }
        }
    }
}