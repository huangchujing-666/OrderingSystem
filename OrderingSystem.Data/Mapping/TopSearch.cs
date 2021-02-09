using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class TopSearchMap: EntityTypeConfiguration<TopSearch>
    {

        public TopSearchMap()
        {
            this.ToTable("TopSearch");
            this.HasKey(m => m.TopSearchId);
            this.Property(m => m.TopSearchId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Contents);
            this.Property(m => m.SortNo);
            this.Property(m => m.Status);
            this.Property(m => m.IsDelete);
            this.Property(m => m.EditTime);
            this.Property(m => m.CreateTime);
            this.Property(m => m.CreatePersonId);
            this.Property(m => m.EditPersonId);
            this.Property(m => m.TypeId);
        }

	}
}

