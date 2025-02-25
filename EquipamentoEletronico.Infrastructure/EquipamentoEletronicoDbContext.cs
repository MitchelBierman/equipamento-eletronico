using Microsoft.EntityFrameworkCore;
using EquipamentoEletronico.Domain.Entities;

namespace EquipamentoEletronico.Infrastructure
{
    public class EquipamentoEletronicoDbContext : DbContext
    {
        public EquipamentoEletronicoDbContext(DbContextOptions<EquipamentoEletronicoDbContext> options)
            : base(options) { }

        public DbSet<Equipamento> Equipamentos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
