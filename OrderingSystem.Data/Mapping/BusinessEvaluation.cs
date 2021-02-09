using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class BusinessEvaluationMap: EntityTypeConfiguration<BusinessEvaluation>
    {

        public BusinessEvaluationMap()
        {
            this.ToTable("BusinessEvaluation");
            this.HasKey(m => m.BusinessEvaluationId);
            this.Property(m => m.BusinessEvaluationId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
           // this.Property(m => m.BusinessInfoId); 
            this.Property(m => m.Environment); 
            this.Property(m => m.Service); 
            this.Property(m => m.Facilities);
            this.Property(m => m.CreateTime); 
            this.Property(m => m.EditTime);


        }

    }
}

