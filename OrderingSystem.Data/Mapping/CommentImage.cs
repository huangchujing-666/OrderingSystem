using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class CommentImageMap: EntityTypeConfiguration<CommentImage>
    {

        public CommentImageMap()
        {
            this.ToTable("CommentImage");
            this.HasKey(m => m.CommentImageId);
            this.Property(m => m.CommentImageId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.BusinessCommentId);
            this.Property(m => m.BaseImageId);


            HasRequired(t => t.BaseImage);
        }

    }
}

