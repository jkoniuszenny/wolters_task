using Application.CQRS.Employees.Commands.Create;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Shared.Settings;

namespace Application.CQRS.Employees.Commands.Update;

internal sealed class EmployeeUpdateCommandHandler : IRequestHandler<EmployeeCreateCommand, GlobalResponse>
{
    private readonly IAsyncRepository _repository;
    private readonly CurrencySettings _currencySettings;

    public EmployeeUpdateCommandHandler(
        IAsyncRepository repository,
         CurrencySettings currencySettings)
    {
        _repository = repository;
        _currencySettings = currencySettings;
    }

    public async Task<GlobalResponse> Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
    {

        var newWallet = new Wallets
        {
            Name = request.Name,
            Currencies = [.. request.InitValue.Select(s => new Currency
            {
                Name = _currencySettings.Codes.First(f=>f.Code == s.Code).Name,
                Code = s.Code,
                Value = s.Value
            })]
        };

        await _repository.InsertList([newWallet]);

        return await GlobalResponse.SuccessAsync();
    }
}
