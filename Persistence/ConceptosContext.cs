using Microsoft.EntityFrameworkCore;
using TestMediaTR.Persistence.Models;

namespace TestMediaTR.Persistence
{
    public class ConceptosContext : DbContext
    {
        public virtual DbSet<PrcConceptos_Model> Conceptos { get; set; }

        public ConceptosContext(DbContextOptions options) 
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PrcConceptos_Model>(entity =>
            {
                entity.HasKey(e => e.conNumero);
            });

        }
        
    }
}