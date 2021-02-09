using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class RiceDateUserMap: EntityTypeConfiguration<RiceDateUser>
    {

        public RiceDateUserMap()
        {
            this.ToTable("RiceDateUser");
            this.HasKey(m => m.RiceDateUserId);
            this.Property(m => m.RiceDateUserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.ApplyStatus);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime);
            this.Property(m => m.RiceDateId);
            this.Property(m => m.UserId);
            HasRequired(t => t.User);
            HasRequired(t => t.RiceDate);


            HasRequired(t => t.User);
        }

    }
}

