using Microsoft.EntityFrameworkCore;
using ProjetoLead.API.Models;

namespace ProjetoLead.API.Data
{
    public class ProjetoLeadDbContext : DbContext
    {
        public ProjetoLeadDbContext(DbContextOptions<ProjetoLeadDbContext> options) : base(options) { }

        public DbSet<Lead> CadastroLead { get; set; }
    }
}
