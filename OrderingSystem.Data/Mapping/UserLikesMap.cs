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
    public class UserLikesMap : EntityTypeConfiguration<UserLikes>
    {
        public UserLikesMap()
        {
            this.ToTable("UserLikes");
            this.HasKey(m => m.UserLikesId);
            this.Property(m => m.UserLikesId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.JourneyArticleId);
            this.Property(m => m.UserId);
            this.Property(m => m.CreateTime);

            HasRequired(t => t.User);
            HasRequired(t => t.JourneyArticle);
        }
    }
}
