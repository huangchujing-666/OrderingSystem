using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderingSystem.Data.Mapping
{
    public class SysMenuMap : EntityTypeConfiguration<SysMenu>
    {
        public SysMenuMap()
        {
            this.ToTable("SysMenu");
            this.HasKey(m => m.SysMenuId);
            this.Property(m => m.SysMenuId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Fid);
            this.Property(m => m.Icon); 
            this.Property(m => m.Name);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime);
            this.Property(m => m.IsDelete);
            this.Property(m => m.SortNo);
            this.Property(m => m.Status);
            this.Property(m => m.Url);
             
        }
    }
}
