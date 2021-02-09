 
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Admin.Models
{
    public class GoodsImageVM : BaseImgInfoVM
    {
         
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        
        public int GoodsId { get; set; }
        
        public GoodsImage GoodsImage { get; set; }
       
        public List<GoodsImage> GoodsImages { get; set; }
        /// <summary>
        /// 分页
        /// </summary>
        public Paging<GoodsImage> Paging { get; set; }

       
        //查询条件
        public string QueryName { get; set; }
        public int QueryType { get; set; }
    }
}