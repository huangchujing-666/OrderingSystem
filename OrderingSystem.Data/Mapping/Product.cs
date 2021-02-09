using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {

        public ProductMap()
        {
            this.ToTable("Product");
            this.HasKey(m => m.ProductId);
            this.Property(m => m.ProductId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Name);
            this.Property(m => m.BusinessInfoId);
            this.Property(m => m.Descript);
            this.Property(m => m.OrignPrice);
            this.Property(m => m.RealPrice);
            this.Property(m => m.Content);
            this.Property(m => m.Remark);
            this.Property(m => m.Status);
            this.Property(m => m.IsDelete);
            this.Property(m => m.StartDate);
            this.Property(m => m.EndDate);
            this.Property(m => m.UseDateLimit);
            this.Property(m => m.Notice);
            this.Property(m => m.Rules);
            this.Property(m => m.CreatePersonId);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditPersonId);
            this.Property(m => m.EditTime);
             
            HasMany(m => m.ProductRelateLableList).WithRequired(n => n.Product); 
        }

    }
}

