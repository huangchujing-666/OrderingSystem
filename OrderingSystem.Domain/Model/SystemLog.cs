using System;
namespace OrderingSystem.Domain.Model
{
	public class SystemLog: IAggregateRoot
    {
		/// <summary>
		/// 
		/// </summary>
		public virtual int SystemLogId { get; set; }
		/// <summary>
		/// url
		/// </summary>
		public virtual string Url { get; set; }
        /// <summary>
        /// Ip地址
        /// </summary>
        public virtual string Ip { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public virtual DateTime OperateTime { get; set; }

    }
}

