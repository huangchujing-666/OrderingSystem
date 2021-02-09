
using OrderingSystem.Admin.Models;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{

    /// <summary>
    /// 商家分组VM
    /// </summary>
    public class BusinessGroupVM
    {

        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        public int BusinessInfoId { get; set; }
        /// <summary>
        /// 菜品信息
        /// </summary>
        public BusinessGroup BusinessGroup { get; set; }

        public Paging<BusinessGroup> Paging { get; set; }

         
        /// <summary>
        /// 查询名称
        /// </summary>
        public string QueryName { get; set; }
 

    }
}