﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rent.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class RentEntities : DbContext
    {
        public RentEntities()
            : base("name=RentEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<RentPayment> RentPayments { get; set; }
        public virtual DbSet<RentPaymentNoticeSendLog> RentPaymentNoticeSendLogs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersLog> UsersLogs { get; set; }
        public virtual DbSet<UsersPassword> UsersPasswords { get; set; }
        public virtual DbSet<UsersNotification> UsersNotifications { get; set; }
        public virtual DbSet<LogError> LogErrors { get; set; }
        public virtual DbSet<UsersAccess> UsersAccesses { get; set; }
        public virtual DbSet<UsersRole> UsersRoles { get; set; }
        public virtual DbSet<Role1> Role1 { get; set; }
        public virtual DbSet<UsersManager> UsersManagers { get; set; }
        public virtual DbSet<LogEmail> LogEmails { get; set; }
    }
}
