using Microsoft.EntityFrameworkCore;
using FinancialData.Shared.Models;

namespace FinancialData.API.Data
{
    public class FinancialDataContext : DbContext
    {
        public FinancialDataContext(DbContextOptions<FinancialDataContext> options) : base(options) { }
        public DbSet<Record> Record { get; set; }
        public DbSet<Frequency> Frequency { get; set; }
        public DbSet<PresentationType> PresentationType { get; set; }
        public DbSet<DataType> DataType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Record>()
                .Property(r => r.Date)
                .HasColumnType("DATE");
        }
    }
}
