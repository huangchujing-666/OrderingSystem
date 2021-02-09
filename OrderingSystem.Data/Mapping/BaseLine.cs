using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class BaseLineMap: EntityTypeConfiguration<BaseLine>
    {

        public BaseLineMap()
        {
            this.ToTable("BaseLine");
            this.HasKey(m => m.BaseLineId);
            this.Property(m => m.BaseLineId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.BaseAreaId);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime);
            this.Property(m => m.IsDelete);
            this.Property(m => m.LineName);
            this.Property(m => m.LineNumber);
            this.Property(m => m.Remarks);
            this.Property(m => m.Status);


            HasRequired(t => t.BaseImage);
            HasMany(m => m.BaseStationList).WithRequired(n => n.BaseLine);
        }

    }
}

