﻿using Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Services.RatingService;
using System.Threading.Tasks;

namespace MyShop.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class RatingMiddleware
    {
        private readonly RequestDelegate _next;

        public RatingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IRatingService ratingService)
        {
            Rating newRating = new() { 
                Host = httpContext.Request.Host.ToString(), 
                Method = httpContext.Request.Method, 
                Path = httpContext.Request.Path, 
                RecordDate = DateTime.Now, 
                Referer = httpContext.Request.Headers.Referer, 
                UserAgent = httpContext.Request.Headers.UserAgent };
          await  ratingService.CreateRating(newRating);
            await _next(httpContext);

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RatingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRatingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RatingMiddleware>();
        }
    }
}
