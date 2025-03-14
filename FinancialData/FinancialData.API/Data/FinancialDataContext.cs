using Microsoft.EntityFrameworkCore;
using FinancialData.Shared.Models;

namespace FinancialData.API.Data
{
    public class FinancialDataContext : DbContext
    {
        public FinancialDataContext(DbContextOptions<FinancialDataContext> options) : base(options) { }

        public DbSet<InflationData> InflationData { get; set; }
        public DbSet<Record> Record { get; set; }
        public DbSet<Frequency> Frequency { get; set; }
        public DbSet<PresentationType> PresentationType { get; set; }
        public DbSet<DataType> DataType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InflationData>()
                .Property(d => d.Date)
                .HasColumnType("DATE");
            modelBuilder.Entity<Record>()
                .HasOne(r => r.Frequency)
                .WithMany()
                .HasForeignKey(r => r.FrequencyId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Record>()
                .HasOne(r => r.PresentationType)
                .WithMany()
                .HasForeignKey(r => r.PresentationTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Record>()
                .HasOne(r => r.DataType)
                .WithMany()
                .HasForeignKey(r => r.DataTypeId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Record>()
                .Property(r => r.Date)
                .HasColumnType("DATE");
        }
    }
}
