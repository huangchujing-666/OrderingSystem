using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {

        public UserMap()
        {
            this.ToTable("User");
            this.HasKey(m => m.UserId);
            this.Property(m => m.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.NickName);
            this.Property(m => m.PhoneNo);
            this.Property(m => m.BaseImageId);
            this.Property(m => m.EditTime);
            this.Property(m => m.CreateTime);
            this.Property(m => m.OpenId);
            this.Property(m => m.CreatePersonId);
            this.Property(m => m.UserName);
            this.Property(m => m.CardNo);
            this.Property(m => m.BirthDay);
            this.Property(m => m.Sex);
            HasRequired(t => t.BaseImage);
        }

    }
}

