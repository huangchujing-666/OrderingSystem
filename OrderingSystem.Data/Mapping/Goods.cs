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

    public class GoodsMap : EntityTypeConfiguration<Goods>
    {

        public GoodsMap()
        {
            this.ToTable("Goods");
            this.HasKey(m => m.GoodsId);
            this.Property(m => m.GoodsId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.BaseImageId);
            this.Property(m => m.BusinessInfoId);
            this.Property(m => m.CreateTime);
            this.Property(m => m.Descript);
            this.Property(m => m.EditTime);
            this.Property(m => m.GoodsId);
            this.Property(m => m.IsDelete);
            this.Property(m => m.Status);
            this.Property(m => m.Name);
            this.Property(m => m.RealPrice);
            this.Property(m => m.OrignPrice);
            this.Property(m => m.CreateTime);
            this.Property(m => m.Size);
            this.Property(m => m.CategoryId);

            HasRequired(t => t.BusinessInfo);
            HasRequired(t => t.BaseImage);

            HasMany(m => m.GoodsImageList).WithRequired(n => n.Goods);
        }
    }
}
