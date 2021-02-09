using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class BusinessCommentMap: EntityTypeConfiguration<BusinessComment>
    {

        public BusinessCommentMap()
        {
            this.ToTable("BusinessComment");
            this.HasKey(m => m.BusinessCommentId);
            this.Property(m => m.BusinessCommentId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.BusinessInfoId);
            this.Property(m => m.OrderId);
            this.Property(m => m.UserId);
            this.Property(m => m.Contents);
            this.Property(m => m.LevelId);
            this.Property(m => m.IsAnonymous);
            this.Property(m => m.CreateTime);
            this.Property(m => m.RecommendDishes);
            this.Property(m => m.Status);
            this.Property(m => m.IsDelete);

            HasRequired(t => t.BusinessInfo);
            HasRequired(t => t.Order);

            HasRequired(t => t.Level);
            HasRequired(t => t.User);

            HasMany(m => m.CommentImageList).WithRequired(n => n.BusinessComment);
        }

    }
}

