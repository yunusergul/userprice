using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace userprice.Models
{
    public partial class BankAppContext : DbContext
    {
        public BankAppContext()
        {
        }

        public BankAppContext(DbContextOptions<BankAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ExchangeRate> ExchangeRate { get; set; }
        public virtual DbSet<ExchangeRateName> ExchangeRateName { get; set; }
        public virtual DbSet<MoneyTransactions> MoneyTransactions { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-HPV10CK;Database=BankApp;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExchangeRate>(entity =>
            {
                entity.ToTable("exchangeRate");

                entity.Property(e => e.ExchangeRateId)
                    .HasColumnName("exchangeRate_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.RateP).HasColumnName("rate_p");
            });

            modelBuilder.Entity<ExchangeRateName>(entity =>
            {
                entity.ToTable("exchangeRateName");

                entity.Property(e => e.ExchangeRateNameId)
                    .HasColumnName("exchangeRateName_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.HasOne(d => d.ExchangeRateNameNavigation)
                    .WithOne(p => p.ExchangeRateName)
                    .HasForeignKey<ExchangeRateName>(d => d.ExchangeRateNameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_exchangeRateName_exchangeRate");
            });

            modelBuilder.Entity<MoneyTransactions>(entity =>
            {
                entity.ToTable("moneyTransactions");

                entity.Property(e => e.MoneyTransactionsId).HasColumnName("moneyTransactions_id");

                entity.Property(e => e.AmountMoney).HasColumnName("amountMoney");

                entity.Property(e => e.BalanceCont).HasColumnName("balance_cont");

                entity.Property(e => e.BalanceGrId).HasColumnName("balanceGr_id");

                entity.Property(e => e.BalenceTipId).HasColumnName("balenceTip_id");

                entity.Property(e => e.OpaoperationId).HasColumnName("opaoperation_id");

                entity.Property(e => e.TrDate)
                    .HasColumnName("tr_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.BalanceGr)
                    .WithMany(p => p.MoneyTransactionsBalanceGr)
                    .HasForeignKey(d => d.BalanceGrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_moneyTransactions_exchangeRate1");

                entity.HasOne(d => d.BalenceTip)
                    .WithMany(p => p.MoneyTransactionsBalenceTip)
                    .HasForeignKey(d => d.BalenceTipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_moneyTransactions_exchangeRate");

                entity.HasOne(d => d.Opaoperation)
                    .WithMany(p => p.MoneyTransactions)
                    .HasForeignKey(d => d.OpaoperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_moneyTransactions_operation");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MoneyTransactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_moneyTransactions_user");
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.ToTable("operation");

                entity.Property(e => e.OperationId).HasColumnName("operation_id");

                entity.Property(e => e.OperationName)
                    .IsRequired()
                    .HasColumnName("operation_name")
                    .HasMaxLength(25)
                    .IsFixedLength();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserLastname)
                    .IsRequired()
                    .HasColumnName("user_lastname")
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("user_name")
                    .HasMaxLength(50);

                entity.Property(e => e.UserPass)
                    .HasColumnName("user_pass")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.UserPpNo)
                    .IsRequired()
                    .HasColumnName("user_pp_no")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
