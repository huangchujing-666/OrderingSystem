using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class AppointmentMap : EntityTypeConfiguration<Appointment>
    {

        public AppointmentMap()
        {
            this.ToTable("Appointment");
            this.HasKey(m => m.AppointmentId);
            this.Property(m => m.AppointmentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.AppointmentTime);
            this.Property(m => m.BusinessInfoId);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime);
            this.Property(m => m.IsDelete);
            this.Property(m => m.DenyReason);            
            this.Property(m => m.ModuleId);
            this.Property(m => m.Phone);
            this.Property(m => m.Remark);
            this.Property(m => m.Status);
            this.Property(m => m.UserId);
            this.Property(m => m.UserName);
            this.Property(m => m.ValueIds);


            HasRequired(t => t.BusinessInfo);
        }
	}
}

