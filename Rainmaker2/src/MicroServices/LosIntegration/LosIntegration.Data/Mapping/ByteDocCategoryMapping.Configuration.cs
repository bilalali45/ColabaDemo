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
    /// There are no comments for ByteDocCategoryMappingConfiguration in the schema.
    /// </summary>
    public partial class ByteDocCategoryMappingConfiguration : IEntityTypeConfiguration<ByteDocCategoryMapping>
    {
        /// <summary>
        /// There are no comments for Configure(EntityTypeBuilder<ByteDocCategoryMapping> builder) method in the schema.
        /// </summary>
        public void Configure(EntityTypeBuilder<ByteDocCategoryMapping> builder)
        {
            builder.ToTable(@"ByteDocCategoryMapping", @"dbo");
            builder.Property<int>(x => x.Id).HasColumnName(@"Id").HasColumnType(@"int").IsRequired().ValueGeneratedOnAdd();
            builder.Property<string>(x => x.ByteDocCategoryName).HasColumnName(@"ByteDocCategoryName").HasColumnType(@"varchar(50)").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            builder.Property<string>(x => x.RmDocCategoryName).HasColumnName(@"RmDocCategoryName").HasColumnType(@"varchar(50)").IsRequired().ValueGeneratedNever().HasMaxLength(50);
            builder.HasKey(@"Id");
            builder.HasMany(x => x.ByteDocTypeMappings).WithOne(op => op.ByteDocCategoryMapping).IsRequired(true).HasForeignKey(@"ByteDocCategoryId");

            CustomizeConfiguration(builder);
        }

        #region Partial Methods

        partial void CustomizeConfiguration(EntityTypeBuilder<ByteDocCategoryMapping> builder);

        #endregion
    }

}
