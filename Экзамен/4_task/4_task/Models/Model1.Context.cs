﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _4_task.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Travel_CompanyEntities : DbContext
    {
        public Travel_CompanyEntities()
            : base("name=Travel_CompanyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Country> Country { get; set; }
        public DbSet<Managers> Managers { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<Tour> Tour { get; set; }
    }
}
