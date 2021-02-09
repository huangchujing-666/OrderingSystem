using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
    public class JourneyArticleMap : EntityTypeConfiguration<JourneyArticle>
    {

        public JourneyArticleMap()
        {
            this.ToTable("JourneyArticle");
            this.HasKey(m => m.JourneyArticleId);
            this.Property(m => m.JourneyArticleId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Name);
            this.Property(m => m.BusinessInfoId); 
            this.Property(m => m.BaseImageId);
            this.Property(m => m.Content);
            this.Property(m => m.Reads);
            this.Property(m => m.Likes); 
            this.Property(m => m.Status);
            this.Property(m => m.IsDelete);  
            this.Property(m => m.CreateTime); 
            this.Property(m => m.EditTime);
            this.Property(m => m.UserId);
            this.Property(m => m.Module);


            HasRequired(t => t.BaseImage);
            HasRequired(t => t.User);
            HasRequired(t => t.BusinessInfo);
        }

    }
}

