using Microsoft.EntityFrameworkCore;
using PaymentServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentServices.Infrastructure.Data
{
    public class ApplicationDbContext:DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<LoanPayment> LoanPayments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LoanPayment>().HasKey(x => x.PaymentId);

            modelBuilder.Entity<LoanPayment>().ToTable("LoanPayments");

            modelBuilder.Entity<LoanPayment>()
           .Property(x => x.PaymentStatus)
           .HasConversion<string>();

            modelBuilder.Entity<LoanPayment>()
              .Property(x => x.PaymentMethod)
              .HasConversion<string>();

           



            modelBuilder.Entity<LoanPayment>()
                .Property(x => x.PaymentAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<LoanPayment>()
                .Property(x => x.PrincipalPaid)
                .HasPrecision(18, 2);

            modelBuilder.Entity<LoanPayment>()
                .Property(x => x.InterestPaid)
                .HasPrecision(18, 2);

            modelBuilder.Entity<LoanPayment>()
                .Property(x => x.PenaltyPaid)
                .HasPrecision(18, 2);

            modelBuilder.Entity<LoanPayment>()
                .Property(x => x.AdvancePayment)
                .HasPrecision(18, 2);


         

        }



    }
}   
