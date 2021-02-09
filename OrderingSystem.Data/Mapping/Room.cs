using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
    public class RoomMap : EntityTypeConfiguration<Room>
    {

        public RoomMap()
        {
            this.ToTable("Room");
            this.HasKey(m => m.RoomId);
            this.Property(m => m.RoomId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Name);
            this.Property(m => m.BusinessInfoId); 
            this.Property(m => m.OrignPrice);
            this.Property(m => m.RealPrice); 
            this.Property(m => m.Status);
            this.Property(m => m.IsDelete); 
            this.Property(m => m.Notice);
            this.Property(m => m.Rules); 
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime); 
            this.Property(m => m.Window); 
            this.Property(m => m.Breakfast); 
            this.Property(m => m.Area);
            this.Property(m => m.Internet);
            this.Property(m => m.Bed);
            this.Property(m => m.BedType); 
            this.Property(m => m.Bathroom);
            this.Property(m => m.Floor);
            this.Property(m => m.AirConditioner);
            this.Property(m => m.Notice);
            this.Property(m => m.Remain);
            this.Property(m => m.OrignPrice);
            this.Property(m => m.RealPrice); 
             
        }

    }
}

