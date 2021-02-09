using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Data.Mapping
{
    public class JpushLogMap: EntityTypeConfiguration<JpushLog>
    {
        public JpushLogMap() { 
        this.ToTable("JpushLog");
            this.HasKey(m => m.JpushLogId);
            this.Property(m => m.JpushLogId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //this.Property(m => m.BusinessInfoId);
            this.Property(m => m.IsToAll);
            this.Property(m => m.ParamString);
            this.Property(m => m.PushId);
            this.Property(m => m.PushMsg);
            this.Property(m => m.CreateTime);
            this.Property(m => m.BePushId);
        }
    }
}
