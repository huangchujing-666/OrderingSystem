using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class LevelMap: EntityTypeConfiguration<Level>
    {

        public LevelMap()
        {
            this.ToTable("Level");
            this.HasKey(m => m.LevelId);
            this.Property(m => m.LevelId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Name);
        }

    }
}

