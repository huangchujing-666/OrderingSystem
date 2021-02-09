 
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Admin.Models
{
    public class ProductImageVM : BaseImgInfoVM
    {
         
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 菜品ID
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 菜单信息
        /// </summary>
        public ProductImage ProductImage { get; set; }
        /// <summary>
        /// 菜单集合
        /// </summary>
        public List<ProductImage> ProductImages { get; set; }
        /// <summary>
        /// 分页
        /// </summary>
        public Paging<ProductImage> Paging { get; set; }

       
        //查询条件
        public string QueryName { get; set; }
        public int QueryType { get; set; }
    }
}