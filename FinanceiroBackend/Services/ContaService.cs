using Domain;
using FinanceiroBackend.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FinanceiroBackend.Services;

public class ContaService(FinanceiroContext context)
{
    public async Task<IEnumerable<Conta>> GetAllAsync(GetAll query)
    {
        var queryable = context.Contas.Include(conta => conta.Tipo).AsQueryable();

        if (query.From.HasValue)
        {
            queryable = queryable.Where(c =>
                c.DataCriacao >= query.From.Value.ToDateTime(TimeOnly.MinValue)
            );
        }

        if (query.To.HasValue)
        {
            queryable = queryable.Where(c =>
                c.DataCriacao <= query.To.Value.ToDateTime(TimeOnly.MinValue)
            );
        }

        if (!string.IsNullOrEmpty(query.status))
        {
            queryable = queryable.Where(c => c.Status == query.status);
        }

        if (!string.IsNullOrEmpty(query.type))
        {
            queryable = queryable.Where(c => c.Tipo.Nome == query.type);
        }

        return await queryable.ToListAsync();
    }

    public async Task<Conta> AddAsync(CreateConta createContaconta)
    {
        var conta = new Conta
        {
            Id = Guid.NewGuid().ToString(),
            TipoId = createContaconta.TipoId,
            Valor = createContaconta.Valor,
            Status = createContaconta.Status,
            DataCriacao = DateTime.UtcNow,
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
                c.SetProperty(c => c.TipoId, createContaconta.TipoId)
                    .SetProperty(c => c.Valor, createContaconta.Valor)
                    .SetProperty(c => c.Status, createContaconta.Status)
                    .SetProperty(c => c.DataCriacao, DateTime.UtcNow)
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
