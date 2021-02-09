
using System;
using System.Collections.Generic;

namespace OrderingSystem.Domain.Model
{
	public class BaseLine : IAggregateRoot
    {
        public virtual int BaseLineId { get; set; }
        public virtual int BaseAreaId { get; set; }
        /// <summary>
        /// 线路图id
        /// </summary>
        public virtual int BaseImageId { get; set; }
        /// <summary>
        /// 线路名称
        /// </summary>
        public virtual string LineName { get; set; }
        /// <summary>
        /// 线路编号
        /// </summary>
        public virtual int LineNumber { get; set; } 
        /// <summary>
        /// 是否删除（0否，1是）
        /// </summary>
        public virtual int IsDelete { get; set; }
        /// <summary>
        /// 状态（0无效，1有效）
        /// </summary>
        public virtual int Status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remarks { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public virtual DateTime EditTime { get; set; } 
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; } 
        /// <summary>
        /// 线路包含的站点
        /// </summary>
        public virtual List<BaseStation> BaseStationList { get; set; }
        public virtual BaseImage BaseImage  { get; set; }

        public virtual BaseArea BaseArea { get; set; }
    }
}

