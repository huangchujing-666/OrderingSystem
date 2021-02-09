using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Data.Mapping
{
    public class BusinessLableMap : EntityTypeConfiguration<BusinessLable>
    {

        public BusinessLableMap()
        {
            this.ToTable("BusinessLable");
            this.HasKey(m => m.BusinessLableId);
            this.Property(m => m.BusinessLableId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Name);
            this.Property(m => m.BusinessInfoId);
        }

    }
}
