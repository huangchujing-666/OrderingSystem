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
    public class GoodsImageMap : EntityTypeConfiguration<GoodsImage>
    {

        public GoodsImageMap()
        {
            this.ToTable("GoodsImage");
            this.HasKey(m => m.GoodsImageId);
            this.Property(m => m.GoodsImageId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //this.Property(m => m.BusinessInfoId);
            this.Property(m => m.BaseImageId);
            this.Property(m => m.CreatePersonId);
            this.Property(m => m.EditPersonId);
            this.Property(m => m.EditTime);
            this.Property(m => m.GoodsId);
            this.Property(m => m.IsDelete);
            this.Property(m => m.Status);
            this.Property(m => m.Type);
            this.Property(m => m.CreateTime);

            HasRequired(t => t.Goods);
            HasRequired(t => t.BaseImage);
        }
    }
}
