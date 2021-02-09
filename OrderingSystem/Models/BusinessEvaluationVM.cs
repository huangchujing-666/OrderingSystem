
using OrderingSystem.Admin.Models;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{

    /// <summary>
    /// 商家分组VM
    /// </summary>
    public class BusinessEvaluationVM
    {

        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        public int BusinessInfoId { get; set; } 
        public BusinessEvaluationModel BusinessEvaluationModel { get; set; }

        public Paging<BusinessInfo> Paging { get; set; }


        /// <summary>
        /// 查询名称
        /// </summary>
        public string QueryName { get; set; }
 

    }

    /// <summary>
    /// 商家评测
    /// </summary>
    public class BusinessEvaluationModel
    {
        /// <summary>
        /// 商家测评id
        /// </summary>
        public virtual int BusinessEvaluationId { get; set; }
        /// <summary>
        /// 商家id
        /// </summary>
        public virtual int BusinessInfoId { get; set; } 
        /// <summary>
        /// 综合评分
        /// </summary>
        public virtual decimal Grade { get; set; }
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
    }

}