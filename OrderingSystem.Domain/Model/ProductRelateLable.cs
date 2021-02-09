using System;
namespace OrderingSystem.Domain.Model
{
	public class ProductRelateLable : IAggregateRoot
    {
		/// <summary>
		/// 
		/// </summary>
		public virtual int ProductRelateLableId { get; set; }
        /// <summary>
        /// 菜品Id
        /// </summary>
        public virtual int ProductId { get; set; }
        public virtual int BusinessInfoId { get; set; } 
        public virtual int ProductLableId { get; set; }

        public virtual ProductLable ProductLable { get; set; }
        public virtual Product Product { get; set; }

    }
}

