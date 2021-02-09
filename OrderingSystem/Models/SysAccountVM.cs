using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrderingSystem.Admin.Models;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Admin.Models
{
    public class SysAccountVM: BaseImgInfoVM
    {
        public int V { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 实体信息
        /// </summary>
        public SysAccount SysAccount { get; set; }
        /// <summary>
        /// 实体集合
        /// </summary>
        public List<SysAccount> SysAccounts { get; set; }

        public List<SysRole> SysRoles { get; set; }
        /// <summary>
        /// 分页
        /// </summary>
        public Paging<SysAccount> Paging { get; set; }

        //查询条件
        public string QueryName { get; set; }
        public int QueryType { get; set; }
    }
}