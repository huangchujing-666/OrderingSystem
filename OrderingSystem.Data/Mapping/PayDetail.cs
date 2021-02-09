using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class PayDetailMap: EntityTypeConfiguration<PayDetail>
    {

        public PayDetailMap()
        {
            this.ToTable("PayDetail");
            this.HasKey(m => m.PayDetailId);
            this.Property(m => m.PayDetailId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.OrderNo);
            this.Property(m => m.UserId);
            this.Property(m => m.Amount);
            this.Property(m => m.PayType);
            this.Property(m => m.OrderTime);
            this.Property(m => m.PaySerialNo);
            this.Property(m => m.PayStatus);
            this.Property(m => m.Remark);
            this.Property(m => m.OrderTime);
            this.Property(m => m.PayTime);
        }

    }
}

