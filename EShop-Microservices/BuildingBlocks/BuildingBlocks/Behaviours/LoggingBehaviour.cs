﻿using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging; 

namespace BuildingBlocks.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest: notnull, IRequest<TResponse>
        where TResponse: notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation($"[START] Handle request={typeof(TRequest).Name} - Response={typeof(TResponse).Name} - RequestData={request}");

            var timer = new Stopwatch();
            timer.Start();

            var response = await next();

            timer.Stop();

            var timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 3)
                logger.LogWarning($"[PERFORMANCE] The request {{typeof(TRequest).Name}} took {timeTaken.Seconds}");

            logger.LogInformation($"[END] Handled request={typeof(TRequest).Name} with Response={typeof(TResponse).Name}.6");
            return response;
        }
    }
}
