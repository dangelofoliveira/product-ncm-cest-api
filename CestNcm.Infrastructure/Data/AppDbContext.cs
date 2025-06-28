using CestNcm.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CestNcm.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<ProdutoCest> ProdutosCest => Set<ProdutoCest>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProdutoCest>(entity =>
        {
            entity.ToTable("produtos_cest");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id).HasColumnName("id");
            entity.Property(p => p.Secao).IsRequired().HasColumnName("secao");
            entity.Property(p => p.Cest).IsRequired().HasColumnName("cest");
            entity.Property(p => p.Ncm).IsRequired().HasColumnName("ncm");
            entity.Property(p => p.Descricao).IsRequired().HasColumnName("descricao");
            entity.Property(p => p.MvaSubstituto).HasColumnName("mva_substituto");
            entity.Property(p => p.MvaOriginal).HasColumnName("mva_original");
            entity.Property(p => p.MvaAjustada12).HasColumnName("mva_ajustada_12");
            entity.Property(p => p.MvaAjustada4).HasColumnName("mva_ajustada_4");
        });
    }
}
