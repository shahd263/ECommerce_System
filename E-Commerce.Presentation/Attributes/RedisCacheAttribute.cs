using E_Commerce.Services_Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Attributes
{
    internal class RedisCacheAttribute : ActionFilterAttribute
    {
        private readonly int _durationInMinutes;

        public RedisCacheAttribute(int DurationInMinutes = 5)
        {
            _durationInMinutes = DurationInMinutes;
        }

        ////Performs after the action(Endpoint)
        //public override void OnActionExecuted(ActionExecutedContext context)
        //{
        //    base.OnActionExecuted(context);
        //}

        ////Performs Before the action(Endpoint)
        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    base.OnActionExecuting(context);
        //}

        //Performs before & after the action(Endpoint)
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Get Cache Service From DI Container (Manually)(in the attribute class can't get it from the constructor Automatically)
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            //Create Cache Key Based On Request Path & Query String 
            var cacheKey = CreateCacheKey(context.HttpContext.Request);

            // Check if Chached Data Exists 
            var cacheValue = await cacheService.GetAsync(cacheKey);
            //If Exists, Return Cached Data and Skip Executing Of Endpoint 
            if(cacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/Json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            //If Not Exists, Execute The Endpoint and Store The Result In Cache if 200 OK Response
            var ExecutedContext = await next.Invoke();
            if(ExecutedContext.Result is OkObjectResult result)
            {
                await cacheService.SetAsync(cacheKey, result.Value!, TimeSpan.FromMinutes(_durationInMinutes));
            }
        }



        // /api/Products?brandId=2&typeId=1
        private string CreateCacheKey(HttpRequest request)
        {
            StringBuilder Key = new StringBuilder(); // To Be Muttable
            Key.Append(request.Path); // api/Products
            foreach (var item in request.Query.OrderBy(X => X.Key))
               Key.Append($"|{item.Key}-{item.Value}"); // api/Products|brandId-2|typeId-1
            return Key.ToString();
            
        }
    }
}
