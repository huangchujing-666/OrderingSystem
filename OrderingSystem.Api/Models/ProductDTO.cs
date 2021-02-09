using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    /// <summary>
    /// 娱乐产品对象
    /// </summary>
    public class ProductDTO
    {
        /// <summary>
        /// 产品id
        /// </summary>
        public int product_id { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string product_name { get; set; }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string business_name { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        public string descript { get; set; }
        /// <summary>
        /// 产品原价格
        /// </summary>
        public string orign_price { get; set; }

        /// <summary>
        /// 实际价格
        /// </summary>
        public string real_price { get; set; }

        /// <summary>
        /// 产品详情
        /// </summary>
        public string content { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string start_date { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string end_date { get; set; }
        
        /// <summary>
        /// 购买须知
        /// </summary>
        public string notice { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 规则
        /// </summary>
        public string rule { get; set; }

        /// <summary>
        /// 产品标签
        /// </summary>
        public string lable { get; set; }
        public List<BaseImageDTO> product_image_list { get; set; }

        /// <summary>
        /// 产品数量
        /// </summary>
        public int count { get; set; }
    }
}