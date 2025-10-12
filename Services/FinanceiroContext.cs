using Domain;
using Microsoft.EntityFrameworkCore;

namespace Application;

public class FinanceiroContext : DbContext
{
    public FinanceiroContext(DbContextOptions<FinanceiroContext> options)
        : base(options) { }

    public DbSet<Conta> Contas { get; set; }
}
