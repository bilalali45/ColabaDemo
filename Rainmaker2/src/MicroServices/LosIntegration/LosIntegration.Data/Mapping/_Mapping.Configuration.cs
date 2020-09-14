﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 2020-09-11 5:20:34 AM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LosIntegration.Entity.Models;



namespace LosIntegration.Data.Mapping
{
    /// <summary>
    /// There are no comments for _MappingConfiguration in the schema.
    /// </summary>
    public partial class _MappingConfiguration : IEntityTypeConfiguration<_Mapping>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<_Mapping> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<_Mapping> builder)
        {
            builder.ToTable(@"Mapping", @"dbo");
            builder.Property<long>(x => x.Id).HasColumnName(@"Id").HasColumnType(@"bigint").IsRequired().ValueGeneratedOnAdd();
            builder.Property<string>(x => x.RMEntityName).HasColumnName(@"RMEntityName").HasColumnType(@"varchar(50)").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            builder.Property<string>(x => x.RMEnittyId).HasColumnName(@"RMEnittyId").HasColumnType(@"varchar(50)").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            builder.Property<string>(x => x.ExtOriginatorEntityName).HasColumnName(@"ExtOriginatorEntityName").HasColumnType(@"varchar(50)").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            builder.Property<string>(x => x.ExtOriginatorEntityId).HasColumnName(@"ExtOriginatorEntityId").HasColumnType(@"varchar(50)").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            builder.Property<short>(x => x.ExtOriginatorId).HasColumnName(@"ExtOriginatorId").HasColumnType(@"smallint").IsRequired().ValueGeneratedNever();
            builder.HasKey(@"Id");
            builder.HasOne(x => x.ExternalOriginator).WithMany(op => op.Mappings).IsRequired(true).HasForeignKey(@"ExtOriginatorId");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<_Mapping> builder);

        #endregion
    }

}