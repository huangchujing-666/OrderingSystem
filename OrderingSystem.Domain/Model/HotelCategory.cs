using System;
namespace OrderingSystem.Domain.Model
{
	public class HotelCategory : IAggregateRoot
    { 
		public virtual int HotelCategoryId { get; set; } 
        /// <summary>
        /// 类别名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

    }
}

