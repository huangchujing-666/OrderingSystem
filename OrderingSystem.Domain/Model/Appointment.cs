
using System;
using System.Collections.Generic;
using static OrderingSystem.Domain.EnumHelp;

namespace OrderingSystem.Domain.Model
{
    /// <summary>
    /// 衣品预约表
    /// </summary>
	public class Appointment : IAggregateRoot
    {
        public virtual int AppointmentId { get; set; }

        /// <summary>
        /// 商家
        /// </summary>
        public virtual BusinessInfo BusinessInfo { get; set; }

        /// <summary>
        /// 商家id
        /// </summary>
        public virtual int BusinessInfoId { get; set; }

        /// <summary>
        /// 对象值ID（多个用英文‘,’分开）
        /// </summary>
        public virtual string ValueIds { get; set; }
        /// <summary>
        /// 预约时间
        /// </summary>
        public virtual DateTime AppointmentTime { get; set; }
        /// <summary>
        /// 模块
        /// </summary>
        public virtual int ModuleId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary>
        /// 联系方式
        /// </summary>
        public virtual string Phone { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual int UserId { get; set; }
        /// <summary>
        /// 状态（0无效，1有效）
        /// </summary>
        public virtual int Status { get; set; }
        /// <summary>
        /// 是否删除（0否，1是）
        /// </summary>
        public virtual int IsDelete { get; set; }
        /// <summary>
        /// 驳回原因
        /// </summary>
        public virtual string DenyReason { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        public string StatusName
        {
            get
            {
                if (this.Status == (int)RiceDateApplyStatus.申请中)
                {
                    return "申请中";
                }else if (this.Status == (int)RiceDateApplyStatus.取消申请)
                {
                    return "取消申请";
                }
                else if (this.Status == (int)RiceDateApplyStatus.申请通过)
                {
                    return "申请通过";
                }
                else if (this.Status == (int)RiceDateApplyStatus.被驳回)
                {
                    return "被驳回";
                }
                else
                {
                    return "";
                }
            }
        }
    }
}

