using System;
using FinanceiroBackend.Dtos;
using FinanceiroBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceiroBackend.Services;

public class ContaService
{
    private readonly FinanceiroContext _context;

    public ContaService(FinanceiroContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Conta>> GetAllAsync()
    {
        return await _context.Contas.ToListAsync();
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

        _context.Contas.Add(conta);
        await _context.SaveChangesAsync();

        return conta;
    }

    public async Task<Conta> GetByIdAsync(string id)
    {
        return await _context.Contas.FirstAsync(c => c.Id == id);
    }

    public async Task<Conta> UpdateAsync(string id, Conta conta)
    {
        _context
            .Contas.Where(c => c.Id == id)
            .ExecuteUpdate(c =>
                c.SetProperty(c => c.TipoId, conta.TipoId)
                    .SetProperty(c => c.Valor, conta.Valor)
                    .SetProperty(c => c.Status, conta.Status)
                    .SetProperty(c => c.DataCriacao, DateTime.UtcNow)
            );

        await _context.SaveChangesAsync();
        return conta;
    }

    public async Task DeleteAsync(string id)
    {
        await _context.Contas.Where(c => c.Id == id).ExecuteDeleteAsync();
    }
}
