using Application.CQRS.Employees.Commands.Update;
using Domain.ValueObject;
using FastEndpoints.Configuration;
using FastEndpoints.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace EndpointsController.Endpoints.Employee;

public class EmployeeUpdate : FastEndpoint
{
    //created 5/7/2025 11:02:14 AM by Jakub.Koniuszenny
    public EmployeeUpdate()
    {
        Method = HttpRequestMethodTypes.Put;
        Url = "/Update/{employeeId}";
        Name = "Update";
        Tag = "Employee";
    }

    /// <summary>
    /// EmployeeUpdate
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="input"></param>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    public async Task<IResult> ExecuteAsync(IMediator mediator, EmployeeUpdateData input, string employeeId)
    {
        var data = new EmployeeUpdateInput(
            EmployeeId: employeeId, 
            LastName:  new LastName(input.LastName), 
            Sex: new Sex(input.Sex)
        );


        var result = await mediator.Send(new EmployeeUpdateCommand(data));
        return Results.Ok(result);
    }
}
