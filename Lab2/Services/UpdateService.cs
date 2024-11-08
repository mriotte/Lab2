using System.Transactions;
using Lab2.DAL;
using Lab2.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LabSolution.API.Services;

public class UpdateService : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UpdateService(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var mongoService = scope.ServiceProvider.GetRequiredService<IMongoService>(); // MongoDB-сервіс
        var dbContext = scope.ServiceProvider.GetRequiredService<LabDbContext>(); // SQL DbContext

        using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        try
        {
            var students = dbContext.Students.ToList();

            foreach (var student in students)
            {
                var studentDto = new StudentDto
                {
                    Name = student.FirstName,
                    Birth = student.Birth
                };

                // Формування унікального Id для MongoDB
                studentDto.Id = $"{student.FirstName}-{student.Birth.ToLongDateString()}";

                await mongoService.CreateAsync(studentDto);
            }

            transaction.Complete();
        }
        catch (Exception ex)
        {
            transaction.Dispose(); // Rollback транзакції
            throw new Exception("Transaction failed", ex);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
