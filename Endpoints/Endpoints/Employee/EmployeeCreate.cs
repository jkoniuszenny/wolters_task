using Application.CQRS.Employees.Commands.Create;
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
    public async Task<IResult> ExecuteAsync(IMediator mediator, EmployeeCreateInput input)
    {
        var result = await mediator.Send(new EmployeeCreateCommand(input));

        return Results.Ok(result);
    }
}
