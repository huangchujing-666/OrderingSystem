using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
    public class ProductLableMap : EntityTypeConfiguration<ProductLable>
    {

        public ProductLableMap()
        {
            this.ToTable("ProductLable");
            this.HasKey(m => m.ProductLableId);
            this.Property(m => m.ProductLableId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Name);
            this.Property(m => m.BusinessInfoId); 
        }

    }
}

