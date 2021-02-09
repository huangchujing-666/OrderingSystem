using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Data.Mapping
{
    public class BusinessMap : EntityTypeConfiguration<BusinessInfo>
    {

        public BusinessMap()
        {
            this.ToTable("BusinessInfo");
            this.HasKey(m => m.BusinessInfoId);
            this.Property(m => m.BusinessInfoId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(m => m.Name);
            this.Property(m => m.BaseImageId);
            this.Property(m => m.BaseAreaId);
            this.Property(m => m.BaseLineId);
            this.Property(m => m.BaseStationId);
            this.Property(m => m.BusinessTypeId);
            this.Property(m => m.Notic);
            this.Property(m => m.Introduction);
            this.Property(m => m.Address);
            this.Property(m => m.BusinessHour);
            this.Property(m => m.Mobile);
            this.Property(m => m.Longitude);
            this.Property(m => m.OrderCountPerMonth);
            this.Property(m => m.AveragePay);
            this.Property(m => m.Grade);
            this.Property(m => m.Status);
            this.Property(m => m.IsDelete);
            this.Property(m => m.SortNo);
            this.Property(m => m.IsTop);
            this.Property(m => m.Distance);
            this.Property(m => m.BusinessGroupId);
            this.Property(m => m.Services);


            this.Property(m => m.CreatePersonId);
            this.Property(m => m.CreateTime);
            this.Property(m => m.EditPersonId);
            this.Property(m => m.EditTime);
            this.Property(m => m.ActivityDiscountId);
            this.Property(m => m.BusinessEvaluationId);
            this.Property(m => m.District);

            this.Property(m => m.RefreshDate);
            this.Property(m => m.TotalFloors);
            this.Property(m => m.TotalRooms);
            this.Property(m => m.OpenDate);


            HasRequired(t => t.BusinessGroup);
            HasRequired(t => t.BaseImage);
            HasRequired(t => t.ActivityDiscount);
            HasRequired(t => t.BusinessEvaluation);
            //HasRequired(t => t.HotelCategory);

            HasMany(m => m.HotelCategoryList).WithRequired(n => n.BusinessInfo);

            HasMany(m => m.ActivityMinusList).WithRequired(n => n.BusinessInfo);
            HasMany(m => m.DishesList).WithRequired(n => n.BusinessInfo);

            HasMany(m => m.ProductList).WithRequired(n => n.BusinessInfo);
            HasMany(m => m.BusinessCommentList).WithRequired(n => n.BusinessInfo);
            HasMany(m => m.BusinessLableList).WithRequired(n => n.BusinessInfo);
            HasMany(m => m.BusinessRoomList).WithRequired(n => n.BusinessInfo);
            HasMany(m => m.BusinessGoodsList).WithRequired(n => n.BusinessInfo);
            HasMany(m => m.BusinessJourneyArticleList).WithRequired(n => n.BusinessInfo);
            HasMany(m => m.BusinessTicketList).WithRequired(n => n.BusinessInfo);
        }

    }
}

