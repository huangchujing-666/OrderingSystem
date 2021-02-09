
using System;
namespace OrderingSystem.Domain.Model
{
    /// <summary>
    /// 商家支付配置 用于商家手动操作改变订单未支付状态为已支付
    /// </summary>
	public class BusinessPayConfig : IAggregateRoot
    { 
		public virtual int BusinessPayConfigId { get; set; }
        /// <summary>
        /// 支付方式描述
        /// </summary>
        public virtual string Name { get; set; }
        public virtual int BusinessInfoId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

    }
}

