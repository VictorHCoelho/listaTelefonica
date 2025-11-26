using ListaTelefonica.Models;
using Microsoft.EntityFrameworkCore;

namespace ListaTelefonica.Data;

public class AgendaDbContext : DbContext
{
    public DbSet<Contato> Contatos { get; set; }

    public AgendaDbContext(DbContextOptions<AgendaDbContext> options) : base(options)
    {
    }
}
