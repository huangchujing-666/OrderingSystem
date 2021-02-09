using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class BaseAreaMap: EntityTypeConfiguration<BaseArea>
    {

        public BaseAreaMap()
        {
            this.ToTable("BaseArea");
            this.HasKey(m => m.BaseAreaId);
            this.Property(m => m.BaseAreaId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime);
            this.Property(m => m.FId);
            this.Property(m => m.Grade);
            this.Property(m => m.IsDelete);
            this.Property(m => m.Name);
            this.Property(m => m.Status); 
        }

    }
}

