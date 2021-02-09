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
    public class RoomImageMap : EntityTypeConfiguration<RoomImage>
    {
        public RoomImageMap()
        {
            this.ToTable("RoomImage");
            this.HasKey(m => m.RoomImageId);
            this.Property(m => m.RoomImageId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.RoomId);
            this.Property(m => m.BaseImageId);
            this.Property(m => m.Type);

            this.Property(m => m.Status);
            this.Property(m => m.IsDelete);

            this.Property(m => m.CreatePersonId);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditPersonId);
            this.Property(m => m.EditTime);


            HasRequired(t => t.BaseImage);
            HasRequired(t => t.Room);
        }
    }
}
