
using System;
using System.Collections.Generic;

namespace OrderingSystem.Domain.Model
{
	public class BaseArea : IAggregateRoot
    {
        public virtual int BaseAreaId { get; set; }
        /// <summary>
        /// 父Id
        /// </summary>
        public virtual int FId { get; set; }
        /// <summary>
        /// 地区名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 等级（0为国家，1为省份，2为市级）
        /// </summary>
        public virtual int Grade { get; set; }
        /// <summary>
        /// 状态（0无效，1有效）
        /// </summary>
        public virtual int Status { get; set; }
        /// <summary>
        /// 是否删除（0否，1是）
        /// </summary>
        public virtual int IsDelete { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public virtual DateTime EditTime { get; set; } 
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; } 
        
        /// <summary>
        /// 地区包含的线路
        /// </summary>
        public virtual List<BaseLine> BaseLine { get; set; }

    }
}

