using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class BaseStationMap: EntityTypeConfiguration<BaseStation>
    {

        public BaseStationMap()
        {
            this.ToTable("BaseStation");
            this.HasKey(m => m.BaseStationId);
            this.Property(m => m.BaseStationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.IsDelete);
            this.Property(m => m.Name);
            this.Property(m => m.Status);
            this.Property(m => m.EditTime);
            this.Property(m => m.CreateTime);
        }

    }
}

