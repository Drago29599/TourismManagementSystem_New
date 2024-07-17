using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TourismManagementSystem.Models
{
    public class TourismDbContext:DbContext
    {
        public TourismDbContext(): base("sqlcon")//name to communicate with data base
        { }

        public DbSet<Package> Packages { get; set; }

        
        public DbSet<Customer> Customers { get; set; }

        

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Payment> payments { get; set; }

        protected override void OnModelCreating(DbModelBuilder mB)
        {
            base.OnModelCreating(mB);

            mB.Entity<Package>().HasKey(P => P.PackId);
            mB.Entity<Package>().Property(P => P.PackId)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            mB.Entity<Package>().Property(P => P.PackageName).HasMaxLength(100).IsRequired();
            mB.Entity<Package>().Property(P => P.Price).IsRequired();
            mB.Entity<Package>().Property(P => P.StartDate).IsRequired();
            mB.Entity<Package>().Property(P => P.EndDate).IsRequired();
            mB.Entity<Package>().Property(P => P.PickupPoint).HasMaxLength(100).IsRequired();
            mB.Entity<Package>().Property(P => P.PackageDetails).HasMaxLength(200).IsRequired();
            mB.Entity<Package>().Property(P => P.Country).HasMaxLength(50).IsRequired();
            mB.Entity<Package>().Property(P => P.State).HasMaxLength(50).IsRequired();
            mB.Entity<Package>().Property(P => P.Destination).HasMaxLength(50).IsRequired();
            
            

            //Fluent API for Customer table

            mB.Entity<Customer>().HasKey(C => C.CustomerId);
            mB.Entity<Customer>().Property(C => C.CustomerId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            mB.Entity<Customer>().Property(C => C.CustomerName).HasMaxLength(100).IsRequired();
            mB.Entity<Customer>().Property(C => C.CustomerAdress).HasMaxLength(100).IsRequired();

            

            //Fluent API for Admin table
            mB.Entity<Admin>().HasKey(A => A.AdminId);
            mB.Entity<Admin>().Property(A => A.AdminId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            mB.Entity<Admin>().Property(A => A.AdminName).HasMaxLength(150).IsRequired();
            mB.Entity<Admin>().Property(A => A.AdminDesignation).HasMaxLength(100).IsRequired();


            //Fluent API for Employee table
            mB.Entity<Employee>().HasKey(E => E.EmployeeId);
            mB.Entity<Employee>().Property(E => E.EmployeeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            mB.Entity<Employee>().Property(E => E.EmpUserName).HasMaxLength(50).IsRequired();
            mB.Entity<Employee>().Property(E => E.EmpFirstName).HasMaxLength(100).IsRequired();
            mB.Entity<Employee>().Property(E => E.EmpLastName).HasMaxLength(100).IsRequired();
            mB.Entity<Employee>().Property(E => E.EmpPhoneNumber).IsRequired();
            mB.Entity<Employee>().Property(E => E.EmpResidentialAddress).HasMaxLength(500).IsRequired();
            mB.Entity<Employee>().Property(E => E.EmployeeRole).HasMaxLength(100).IsRequired();

            mB.Entity<Payment>().HasKey(P => P.PaymentId);
            mB.Entity<Payment>().Property(P => P.PaymentId)
            .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            


            //FOREIGEN KEYS
            mB.Entity<Package>()
                .HasMany(P => P.Payments).WithRequired()
                .HasForeignKey(B => B.PackageId);
            mB.Entity<Customer>()
                .HasMany(C => C.Payments).WithRequired().
                HasForeignKey(B => B.CustomerId);


        }

    }
}