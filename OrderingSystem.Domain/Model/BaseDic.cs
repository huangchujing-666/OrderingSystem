using System;
namespace OrderingSystem.Domain.Model
{
	public class BaseDic : IAggregateRoot
    {
		/// <summary>
		/// 
		/// </summary>
		public virtual int BaseDicId { get; set; }
		/// <summary>
		/// 名称
		/// </summary>
		public virtual string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public virtual string Type { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public virtual int SortNo { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public virtual int IsDelete { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public virtual int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        public virtual DateTime EditTime { get; set; }

    }
}

