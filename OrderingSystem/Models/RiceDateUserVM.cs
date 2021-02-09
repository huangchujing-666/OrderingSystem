
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{
    /// <summary>
    /// 拼饭用户VM
    /// </summary>
    public class RiceDateUserVM
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; } 
        /// <summary>
        /// 分页
        /// </summary>
        public List<RiceDateUser> Paging { get; set; }

        //查询条件
        public string QueryName { get; set; } 
    }
}