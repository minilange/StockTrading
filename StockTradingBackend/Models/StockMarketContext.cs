using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StockTradingBackend.Models
{
    public partial class StockMarketContext : DbContext
    {
        public StockMarketContext()
        {
        }

        public StockMarketContext(DbContextOptions<StockMarketContext> options)
            : base(options)
        {
        }

        public virtual DbSet<History> Histories { get; set; } = null!;
        public virtual DbSet<Stock> Stocks { get; set; } = null!;
        public virtual DbSet<Transaction> Transactions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=StockMarket;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("history");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Ticker)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("ticker");

                entity.Property(e => e.TimeStamp)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("timeStamp");
            });

            modelBuilder.Entity<Stock>(entity =>
            {
                entity.ToTable("stocks");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Available).HasColumnName("available");

                entity.Property(e => e.Issued).HasColumnName("issued");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Ticker)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("ticker");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transactions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NewPrice).HasColumnName("newPrice");

                entity.Property(e => e.OldPrice).HasColumnName("oldPrice");

                entity.Property(e => e.Operation)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("operation");

                entity.Property(e => e.Stock)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("stock");

                entity.Property(e => e.TimeStamp)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("timeStamp");

                entity.Property(e => e.Transactor)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("transactor");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
