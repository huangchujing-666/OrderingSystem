
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{
    /// <summary>
    /// 拼饭反馈VM
    /// </summary>
    public class RiceDateFeedbackVM
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; } 
        /// <summary>
        /// 分页
        /// </summary>
        public List<RiceDateFeedback> Paging { get; set; }

        //查询条件
        public string QueryName { get; set; } 
    }
}