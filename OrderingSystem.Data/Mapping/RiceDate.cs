using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class RiceDateMap: EntityTypeConfiguration<RiceDate>
    {

        public RiceDateMap()
        {
            this.ToTable("RiceDate");
            this.HasKey(m => m.RiceDateId);
            this.Property(m => m.RiceDateId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Address);
            this.Property(m => m.Age);
            this.Property(m => m.BaseImageIds);
            this.Property(m => m.BeginDate);
            this.Property(m => m.BusinessName);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime);
            this.Property(m => m.PayWay);
            this.Property(m => m.Remark);
            this.Property(m => m.Sex);
            this.Property(m => m.Taste);
            this.Property(m => m.UserCount);
            this.Property(m => m.UserId);
            this.Property(m => m.Zone); 

            this.Property(m => m.Status);
            this.Property(m => m.IsDelete);
            HasRequired(t => t.User);

            HasMany(m => m.RiceDateUserList).WithRequired(n => n.RiceDate);
        }

    }
}

