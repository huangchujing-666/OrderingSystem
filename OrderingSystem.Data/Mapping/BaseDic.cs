using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class BaseDicMap: EntityTypeConfiguration<BaseDic>
    {

        public BaseDicMap()
        {
            this.ToTable("BaseDic");
            this.HasKey(m => m.BaseDicId);
            this.Property(m => m.BaseDicId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime);
            this.Property(m => m.IsDelete);
            this.Property(m => m.Name);
            this.Property(m => m.SortNo);
            this.Property(m => m.Status);
            this.Property(m => m.Type);
            this.Property(m => m.IsDelete);
        }

    }
}

