using DigitalWallet.Modules.Wallets.Application.Results;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DigitalWallet.Modules.Wallets.Application.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) 
                                         : IPipelineBehavior<TRequest, TResponse>
                                           where TRequest : notnull
                                           where TResponse : IResultFactory<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validatorList = validators.ToArray();

            if (validatorList.Length == 0)
            {
                return await next(cancellationToken);
            }

            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(validatorList.Select(validator => validator.ValidateAsync(context, cancellationToken)));

            var errors = validationResults
                                        .SelectMany(result => result.Errors)
                                        .Where(failure => failure is not null)
                                        .Select(failure =>
                                            Error.Validation(
                                                failure.ErrorCode,
                                                failure.ErrorMessage))
                                        .ToArray();

            if (errors.Length > 0)
            {
                return TResponse.Failure(errors);
            }

            return await next(cancellationToken);
        }
    }
}
