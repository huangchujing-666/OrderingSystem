using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Admin.Models
{
    public class SysRoleVM
    {
        public int V { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 实体信息
        /// </summary>
        public SysRole SysRole { get; set; }
        /// <summary>
        /// 实体集合
        /// </summary>
        public List<SysRole> SysRoles { get; set; }
        /// <summary>
        /// 分页
        /// </summary>
        public Paging<SysRole> Paging { get; set; }

        //查询条件
        public string QueryName { get; set; }
        public int QueryType { get; set; }
    }
}