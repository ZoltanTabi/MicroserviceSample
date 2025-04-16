using FluentValidation;

namespace MicroserviceSample.CommandService.Common.Filters;

public class ValidationFilter<T>(IValidator<T> validator) : IEndpointFilter where T : class
{
    private readonly IValidator<T> validator = validator;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (context.Arguments.FirstOrDefault(x => x?.GetType() == typeof(T)) is not T obj)
        {
            return Results.BadRequest();
        }

        var validationResult = await validator.ValidateAsync(obj);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest(string.Join("/n", validationResult.Errors));
        }

        return await next(context);
    }
}
