using System.Reflection;
using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using Mediator;

namespace Pineapple.GrpcMock.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async ValueTask<TResponse> Handle(
        TRequest message, MessageHandlerDelegate<TRequest, TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator == null)
        {
            return await next(message, cancellationToken);
        }

        var validationResult = await _validator.ValidateAsync(message, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next(message, cancellationToken);
        }

        return TryCreateResponseFromErrors(validationResult.Errors, out var response)
            ? response
            : throw new ValidationException(validationResult.Errors);   
    }

    private static bool TryCreateResponseFromErrors(List<ValidationFailure> validationFailures, out TResponse response)
    {
        List<Error> errors = validationFailures.ConvertAll(x => Error.Validation(
                code: x.PropertyName,
                description: x.ErrorMessage));

        response = (TResponse?) typeof(TResponse)
            .GetMethod(
                name: nameof(ErrorOr<object>.From),
                bindingAttr: BindingFlags.Static | BindingFlags.Public,
                types: new[] { typeof(List<Error>) })?
            .Invoke(null, new[] { errors })!;

        return response is not null;
    }
}