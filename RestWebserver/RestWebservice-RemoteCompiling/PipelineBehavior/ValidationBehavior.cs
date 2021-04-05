using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using RestWebservice_RemoteCompiling.Entities;
using Serilog;

namespace RestWebservice_RemoteCompiling.PipelineBehavior
{
    public class ValidationBehavior<TRequest,TResponse> : IPipelineBehavior<TRequest,TResponse> 
        where TResponse : CustomResponse, new()
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(ILogger<ValidationBehavior<TRequest, TResponse>> logger,IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //pre
            var context = new ValidationContext<object>(request);
            var failures = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .ToList();

            foreach (var failure in failures)
            {
                Log.Warning($"{typeof(TRequest).Name} - Validation failed.\nReason: {failure.ErrorMessage}");
                var response = new TResponse();
                response.BadRequest(failure.ErrorMessage);
                return response;
            }

            return await next();
            //post
        }
    }
}