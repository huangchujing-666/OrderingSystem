using System;
namespace OrderingSystem.Domain.Model
{
	public class ProductLable : IAggregateRoot
    { 
		public virtual int ProductLableId { get; set; }
        /// <summary>
        /// 标签名称
        /// </summary>
        public virtual string Name { get; set; }
        public virtual int ProductId { get; set; }
        public virtual int BusinessInfoId { get; set; }
         

    }
}

