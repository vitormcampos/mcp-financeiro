using Domain;
using Domain.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Application.Services;

public class ContaService(FinanceiroContext context)
{
    public async Task<IEnumerable<Conta>> GetAllAsync(GetAll query)
    {
        var queryable = context.Contas.AsQueryable();

        if (query.From.HasValue)
        {
            queryable = queryable.Where(c => c.CreatedAt >= query.From.Value);
        }

        if (query.To.HasValue)
        {
            queryable = queryable.Where(c => c.CreatedAt <= query.To.Value);
        }

        if (!string.IsNullOrEmpty(query.status))
        {
            queryable = queryable.Where(c => c.Status == query.status);
        }

        if (!string.IsNullOrEmpty(query.type))
        {
            queryable = queryable.Where(c => c.Type == query.type);
        }

        return await queryable.ToListAsync();
    }

    public async Task<Conta> AddAsync(CreateConta createContaconta)
    {
        var conta = new Conta
        {
            Id = Guid.NewGuid().ToString(),
            Description = createContaconta.Description,
            Amount = createContaconta.Amount,
            Status = createContaconta.Status,
            Type = createContaconta.Type,
            Mouth = DateTime.UtcNow.Month,
            Year = DateTime.UtcNow.Year,
            CreatedAt = DateTime.UtcNow,
        };

        context.Contas.Add(conta);
        await context.SaveChangesAsync();

        return conta;
    }

    public async Task<Conta> GetByIdAsync(string id)
    {
        return await context.Contas.FirstAsync(c => c.Id == id);
    }

    public async Task<Conta> UpdateAsync(string id, CreateConta createContaconta)
    {
        context
            .Contas.Where(c => c.Id == id)
            .ExecuteUpdate(c =>
                c.SetProperty(c => c.Description, createContaconta.Description)
                    .SetProperty(c => c.Amount, createContaconta.Amount)
                    .SetProperty(c => c.Status, createContaconta.Status)
            );

        await context.SaveChangesAsync();

        var conta = await context.Contas.FirstAsync(c => c.Id == id);
        return conta;
    }

    public async Task DeleteAsync(string id)
    {
        await context.Contas.Where(c => c.Id == id).ExecuteDeleteAsync();
    }
}
