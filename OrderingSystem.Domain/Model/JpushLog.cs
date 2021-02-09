using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Domain.Model
{
    public class JpushLog: IAggregateRoot
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual int JpushLogId { get; set; }

        /// <summary>
        /// 推送信息
        /// </summary>
        public virtual string PushMsg { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public virtual string ParamString { get; set; }

        /// <summary>
        /// 推送人id
        /// </summary>
        public virtual int PushId { get; set; }

        /// <summary>
        /// 接收人Id
        /// </summary>
        public virtual string BePushId { get; set; }

        /// <summary>
        /// 是否推送全部
        /// </summary>
        public virtual int IsToAll { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
    }
}
