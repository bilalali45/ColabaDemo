﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 8/6/2020 2:55:41 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Reflection;
using System.Data.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using LosIntegration.Data.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using LosIntegration.Entity.Models;

namespace LosIntegration.Data
{

    public partial class Context : DbContext
    {

        public Context() :
            base()
        {
            OnCreated();
        }

        public Context(DbContextOptions<Context> options) :
            base(options)
        {
            OnCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured ||
                (!optionsBuilder.Options.Extensions.OfType<RelationalOptionsExtension>().Any(ext => !string.IsNullOrEmpty(ext.ConnectionString) || ext.Connection != null) &&
                 !optionsBuilder.Options.Extensions.Any(ext => !(ext is RelationalOptionsExtension) && !(ext is CoreOptionsExtension))))
            {
                optionsBuilder.UseSqlServer(@"Data Source=rsserver1;Initial Catalog=LosIntegration;Integrated Security=False;Persist Security Info=True;User ID=sa;Password=test123");
            }
            CustomizeConfiguration(ref optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }

        partial void CustomizeConfiguration(ref DbContextOptionsBuilder optionsBuilder);

        public virtual DbSet<ByteDocCategoryMapping> ByteDocCategoryMappings
        {
            get;
            set;
        }

        public virtual DbSet<ByteDocStatusMapping> ByteDocStatusMappings
        {
            get;
            set;
        }

        public virtual DbSet<ByteDocTypeMapping> ByteDocTypeMappings
        {
            get;
            set;
        }

        public virtual DbSet<ExternalOriginator> ExternalOriginators
        {
            get;
            set;
        }

        public virtual DbSet<Entity.Models.Mapping> Mappings
        {
            get;
            set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<ByteDocCategoryMapping>(new ByteDocCategoryMappingConfiguration());
            modelBuilder.ApplyConfiguration<ByteDocStatusMapping>(new ByteDocStatusMappingConfiguration());
            modelBuilder.ApplyConfiguration<ByteDocTypeMapping>(new ByteDocTypeMappingConfiguration());
            modelBuilder.ApplyConfiguration<ExternalOriginator>(new ExternalOriginatorConfiguration());
            modelBuilder.ApplyConfiguration<Entity.Models.Mapping>(new MappingConfiguration());
            CustomizeMapping(ref modelBuilder);
        }

        partial void CustomizeMapping(ref ModelBuilder modelBuilder);

        public bool HasChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Added || e.State == Microsoft.EntityFrameworkCore.EntityState.Modified || e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);
        }

        partial void OnCreated();
    }
}