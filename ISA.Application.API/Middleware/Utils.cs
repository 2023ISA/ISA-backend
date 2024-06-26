﻿using System.Net.Mime;
using System.Net;

namespace ISA.Application.API.Middleware;

public static class Utils
{
    internal static HttpStatusCode ExceptionToStatusCode(this Exception exception)
        => exception switch
        {
            ArgumentException => HttpStatusCode.BadRequest,
            KeyNotFoundException => HttpStatusCode.NotFound,
            _ => HttpStatusCode.InternalServerError
        };

    internal static async Task WriteJsonToHttpResponseAsync<TResponse>(HttpResponse httpResponse, HttpStatusCode statusCode, TResponse response)
    {
        httpResponse.ContentType = MediaTypeNames.Application.Json;
        httpResponse.StatusCode = (int)statusCode;
        await httpResponse.WriteAsJsonAsync(response);
    }
}