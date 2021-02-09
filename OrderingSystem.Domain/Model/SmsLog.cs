using System;
namespace OrderingSystem.Domain.Model
{
    public class SmsLog : IAggregateRoot
    {
        public virtual int SmsLogId { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public virtual string Phone { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public virtual int Module { get; set; }
    }
}

