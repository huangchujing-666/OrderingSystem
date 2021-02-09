
using System;
namespace OrderingSystem.Domain.Model
{
	public class BaseStation : IAggregateRoot
    {
        public virtual int BaseStationId { get; set; }
        public virtual int BaseAreaId { get; set; }
        public virtual int BaseLineId { get; set; }
        /// <summary>
        /// 站点名称
        /// </summary>
        public virtual string Name { get; set; } 
        /// <summary>
        /// 是否删除（0否，1是）
        /// </summary>
        public virtual int IsDelete { get; set; }
        /// <summary>
        /// 状态（0无效，1有效）
        /// </summary>
        public virtual int Status { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public virtual DateTime EditTime { get; set; } 
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        public virtual BaseLine BaseLine { get; set; }
        public virtual BaseArea BaseArea { get; set; }

    }
}

