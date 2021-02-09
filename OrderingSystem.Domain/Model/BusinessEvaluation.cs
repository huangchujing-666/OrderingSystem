using System;
namespace OrderingSystem.Domain.Model
{
    /// <summary>
    /// 商家测评表
    /// </summary>
    public class BusinessEvaluation : IAggregateRoot
    {
        /// <summary>
        /// 商家测评id
        /// </summary>
        public virtual int BusinessEvaluationId { get; set; }
        /// <summary>
        /// 环境评分
        /// </summary>
        public virtual decimal Environment { get; set; }
        /// <summary>
        /// 服务评分
        /// </summary>
        public virtual decimal Service { get; set; }
        /// <summary>
        /// 设施评分
        /// </summary>
        public virtual decimal Facilities { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public virtual DateTime EditTime { get; set; } 
    }

}

