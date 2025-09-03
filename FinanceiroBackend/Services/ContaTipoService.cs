using Domain;
using FinanceiroBackend.Dtos;
using Microsoft.EntityFrameworkCore;

namespace FinanceiroBackend.Services;

public class ContaTipoService
{
    private readonly FinanceiroContext _context;

    public ContaTipoService(FinanceiroContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ContaTipo>> GetAllAsync()
    {
        return await _context.ContaTipos.ToListAsync();
    }

    public async Task<ContaTipo> AddAsync(CreateContaTipo createContaTipo)
    {
        var contaTipo = new ContaTipo
        {
            Id = Guid.NewGuid().ToString(),
            Nome = createContaTipo.Nome,
        };

        _context.ContaTipos.Add(contaTipo);
        await _context.SaveChangesAsync();

        return contaTipo;
    }

    public async Task<ContaTipo> GetByIdAsync(string id)
    {
        return await _context.ContaTipos.FirstAsync(c => c.Id == id || c.Nome == id);
    }

    public async Task<ContaTipo> UpdateAsync(string id, CreateContaTipo createContaTipo)
    {
        await _context
            .ContaTipos.Where(c => c.Id == id)
            .ExecuteUpdateAsync(c => c.SetProperty(c => c.Nome, createContaTipo.Nome));

        var contaTipo = await GetByIdAsync(id);

        return contaTipo;
    }

    public async Task DeleteAsync(string id)
    {
        await _context.ContaTipos.Where(c => c.Id == id).ExecuteDeleteAsync();
    }
}
