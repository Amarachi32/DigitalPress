﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PressCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressCore.DBContext.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // builder.Property<int>("Id");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p =>p.Description).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(b => b.ProductBrand).WithMany().HasForeignKey(p => p.ProductBrandId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(t => t.ProductType).WithMany().HasForeignKey(t => t.ProductTypeId);
        }
    }
}
