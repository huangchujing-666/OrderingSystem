
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class BusinessTypeMap: EntityTypeConfiguration<BusinessType>
    {

        public BusinessTypeMap()
        {
            this.ToTable("BusinessType");
            this.HasKey(m => m.BusinessTypeId);
            this.Property(m => m.BusinessTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.TypeName);
        }
        
    }
}

