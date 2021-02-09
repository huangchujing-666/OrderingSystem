using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class SmsLogMap: EntityTypeConfiguration<SmsLog>
    {

        public SmsLogMap()
        {
            this.ToTable("SmsLog");
            this.HasKey(m => m.SmsLogId);
            this.Property(m => m.SmsLogId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Code);
            this.Property(m => m.CreateTime);
            this.Property(m => m.Module);
            this.Property(m => m.Phone);
            this.Property(m => m.Remark);
        }

    }
}

