using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
	public class OrderMap: EntityTypeConfiguration<Order>
    {

        public OrderMap()
        {
            this.ToTable("Order");
            this.HasKey(m => m.OrderId);
            this.Property(m => m.OrderId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.OrderNo);
            this.Property(m => m.UserId);
            this.Property(m => m.BusinessInfoId);
            this.Property(m => m.OrignAmount);
            this.Property(m => m.MinusAmount);
            this.Property(m => m.RealAmount);
            this.Property(m => m.Remark);
            this.Property(m => m.OrderStatusId);
            this.Property(m => m.EditTime);
            this.Property(m => m.OrderTime);
            this.Property(m => m.PayTime);
            this.Property(m => m.DiscountRemark);
            this.Property(m => m.ActivityType);
            this.Property(m => m.SeatNo);
            this.Property(m => m.UseFromDate);
            this.Property(m => m.UseEndDate);
            this.Property(m => m.Count); 

            HasRequired(t => t.BusinessInfo);
            HasRequired(t => t.User);
            HasRequired(t => t.OrderStatus);


            HasMany(m => m.BusinessCommentList).WithRequired(n => n.Order);
            HasMany(m => m.OrderDetailList).WithRequired(n => n.Order);
            HasMany(m => m.OrderCustomerList).WithRequired(n => n.Order);
        }
    }
}

