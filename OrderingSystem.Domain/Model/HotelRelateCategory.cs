using System;
namespace OrderingSystem.Domain.Model
{
	public class HotelRelateCategory : IAggregateRoot
    {
 
		public virtual int HotelRelateCategoryId { get; set; }
 
        public virtual int BusinessInfoId { get; set; } 
        public virtual int HotelCategoryId { get; set; }
        public virtual DateTime CreateTime { get; set; }

        public virtual BusinessInfo BusinessInfo { get; set; }
        public virtual HotelCategory HotelCategory { get; set; }

    }
}

