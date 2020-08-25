﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 8/18/2020 2:58:01 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using LosIntegration.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LosIntegration.Data.Mapping
{
    /// <summary>
    /// There are no comments for ByteDocStatusMappingConfiguration in the schema.
    /// </summary>
    public partial class ByteDocStatusMappingConfiguration : IEntityTypeConfiguration<ByteDocStatusMapping>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<ByteDocStatusMapping> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<ByteDocStatusMapping> builder)
        {
            builder.ToTable(@"ByteDocStatusMapping", @"dbo");
            builder.Property<int>(x => x.Id).HasColumnName(@"Id").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd();
            builder.Property<string>(x => x.ByteDocStatusName).HasColumnName(@"ByteDocStatusName").HasColumnType(@"varchar(50)").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            builder.Property<string>(x => x.RmDocStatusName).HasColumnName(@"RmDocStatusName").HasColumnType(@"varchar(50)").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            builder.HasKey(@"Id");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<ByteDocStatusMapping> builder);

        #endregion
    }

}
