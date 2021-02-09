
using System;
namespace OrderingSystem.Domain.Model
{
	public  class ActivityMinus: IAggregateRoot
    {
		public virtual int ActivityMinusId { get; set; }
		/// <summary>
		/// 商家Id
		/// </summary>
		public virtual int BusinessInfoId { get; set; }
		/// <summary>
		/// 到达金额
		/// </summary>
		public virtual decimal AchiveAmount { get; set; }
		/// <summary>
		/// 优惠金额
		/// </summary>
		public virtual decimal MinusAmount { get; set; }


        public virtual BusinessInfo BusinessInfo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual int? Status { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual int? IsDelete { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public virtual DateTime? EditTime { get; set; }

        /// <summary>
        /// 编辑人Id
        /// </summary>
        public virtual int? EditPersonId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime? CreateTime { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public virtual int? CreatePersonId { get; set; }
    }
}

