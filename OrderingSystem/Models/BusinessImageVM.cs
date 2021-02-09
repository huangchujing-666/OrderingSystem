 
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Admin.Models
{
    public class BusinessImageVM : BaseImgInfoVM
    {
         
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 商家ID
        /// </summary>
        public int BusinessId { get; set; }
        /// <summary>
        /// 菜单信息
        /// </summary>
        public BusinessImage BusinessImage { get; set; }
        /// <summary>
        /// 菜单集合
        /// </summary>
        public List<BusinessImage> BusinessImages { get; set; }

        public int RoleId { get; set; }
        /// <summary>
        /// 分页
        /// </summary>
        public Paging<BusinessImage> Paging { get; set; }

       
        //查询条件
        public string QueryName { get; set; }
        public int QueryType { get; set; }
    }
}