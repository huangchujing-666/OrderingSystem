﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class RiceDateFeedbackMap: EntityTypeConfiguration<RiceDateFeedback>
    {

        public RiceDateFeedbackMap()
        {
            this.ToTable("RiceDateFeedback");
            this.HasKey(m => m.RiceDateFeedbackId);
            this.Property(m => m.RiceDateFeedbackId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Content);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditTime);
            this.Property(m => m.RiceDateId);
            this.Property(m => m.UserId);

            HasRequired(t => t.RiceDate);


            HasRequired(t => t.User);
        }

    }
}

