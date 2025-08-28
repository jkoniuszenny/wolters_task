using Application.CQRS.Employees.Commands.Create;
using Domain.ValueObject;
using FastEndpoints.Configuration;
using FastEndpoints.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EndpointsController.Endpoints.Employee;

public class WalletCreate : FastEndpoint
{
    //created 5/7/2025 8:59:41 AM by Jakub.Koniuszenny
    public WalletCreate()
    {
        Method = HttpRequestMethodTypes.Post;
        Url = "/Create";
        Name = "Create";
        Tag = "Employee";
    }

    /// <summary>
    /// WalletCreate
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    //[Authorize]
    public async Task<IResult> ExecuteAsync(IMediator mediator, EmployeeCreateData input)
    {
        var requestValue = new EmployeeCreateInput(
            LastName: new LastName(input.LastName),
            Sex: new Sex(input.Gender)
            );


        var result = await mediator.Send(new EmployeeCreateCommand(requestValue));

        return Results.Ok(result);
    }
}
