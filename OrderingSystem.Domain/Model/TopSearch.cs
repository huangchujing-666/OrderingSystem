using System;
namespace OrderingSystem.Domain.Model
{
	public class TopSearch: IAggregateRoot
    {
		/// <summary>
		/// 主键
		/// </summary>
		public virtual int TopSearchId { get; set; }
		/// <summary>
		/// 搜索内容
		/// </summary>
		public virtual string Contents { get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
        public virtual int SortNo { get; set; }
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
        public virtual int? TypeId { get; set; }
    }
}

