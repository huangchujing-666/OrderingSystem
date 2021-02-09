using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
    public class ProductRelateLableMap : EntityTypeConfiguration<ProductRelateLable>
    {

        public ProductRelateLableMap()
        {
            this.ToTable("ProductRelateLable");
            this.HasKey(m => m.ProductRelateLableId);
            this.Property(m => m.ProductRelateLableId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.BusinessInfoId); 
            this.Property(m => m.ProductId);
            this.Property(m => m.ProductRelateLableId);

            HasRequired(t => t.ProductLable);
        }

    }
}

