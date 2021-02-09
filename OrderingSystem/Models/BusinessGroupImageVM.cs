 
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Admin.Models
{
    public class BusinessGroupImageVM : BaseImgInfoVM
    {
        public int GroupId { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 菜单信息
        /// </summary>
        public BusinessGroup BusinessGroup { get; set; }
        public BusinessGroupImage BusinessGroupImage { get; set; }
        /// <summary>
        /// 菜单集合
        /// </summary>
        public List<BusinessGroupImage> BusinessGroupImages { get; set; }
         
        /// <summary>
        /// 分页
        /// </summary>
        public Paging<BusinessGroupImage> Paging { get; set; }
         

    } 

}