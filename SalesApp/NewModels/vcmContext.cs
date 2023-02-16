using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SalesApp.NewModels
{
    public partial class vcmContext : DbContext
    {
        public vcmContext()
        {
        }

        public vcmContext(DbContextOptions<vcmContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Quality> Quality { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-KUR20V3\\MSSQLSERVER01;Database=vcm;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quality>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.QualityName)
                    .HasName("Quality_QualityName");

                entity.HasIndex(e => new { e.QualityId, e.ItemId })
                    .HasName("QualityIndex");

                entity.Property(e => e.Hscode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Instruction).HasDefaultValueSql("('')");

                entity.Property(e => e.ItemId).HasColumnName("Item_Id");

                entity.Property(e => e.Loss).HasColumnName("loss");

                entity.Property(e => e.QualityCode).HasMaxLength(100);

                entity.Property(e => e.QualityName).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(2500);

                entity.Property(e => e.Userid).HasColumnName("userid");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
