using System;
namespace OrderingSystem.Domain.Model
{
	public class Customer: IAggregateRoot
    {
		public virtual int CustomerId { get; set; }
        /// <summary>
        /// 顾客姓名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 证件类别ID
        /// </summary>
        public virtual int CardType { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public virtual string CardNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }

        public virtual int UserId { get; set; }

        public virtual User User { get; set; }
    }
}

