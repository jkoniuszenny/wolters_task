using Application.Interfaces.Repositories;
using Domain.Entities;
using MongoDB.Driver.Linq;

namespace Infrastructure.Providers;

public class EmployeeNrGeneratorService : IEmployeeNrGeneratorService
{
    private readonly IAsyncRepository _repository;

    public EmployeeNrGeneratorService(
        IAsyncRepository repository)
    {
        _repository = repository;
    }

    public async Task SaveActualRates()
    {



        var actualNbpTableB = await _nbpApiProvider.NbpSync();

        var isExistedActualTabeleB = await (await _repository.AsQueryable<Employees>())
            .Where(x => x.No == actualNbpTableB[0].No)
            .AnyAsync();

        if (isExistedActualTabeleB) 
            return;
        
        var mappedResult = actualNbpTableB
            .Select(x =>  new Employees
            {
                No = x.No,
                EffectiveDate = x.EffectiveDate,
                Rates = [.. x.Rates.Select(s => new Rates
                {
                    Currency = s.Currency,
                    Code = s.Code,
                    Mid = s.Mid
                })]
            })
            .ToList();

        await _repository.InsertList(mappedResult);
    }
}
