using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class SystemLogMap: EntityTypeConfiguration<SystemLog>
    {

        public SystemLogMap()
        {
            this.ToTable("SystemLog");
            this.HasKey(m => m.SystemLogId);
            this.Property(m => m.SystemLogId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Url);
            this.Property(m => m.Ip);
            this.Property(m => m.OperateTime);
        }

    }
}

