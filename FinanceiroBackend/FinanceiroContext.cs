using System;
using FinanceiroBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceiroBackend;

public class FinanceiroContext : DbContext
{
    public FinanceiroContext(DbContextOptions<FinanceiroContext> options)
        : base(options) { }

    public DbSet<Conta> Contas { get; set; }
    public DbSet<ContaTipo> ContaTipos { get; set; }
}
