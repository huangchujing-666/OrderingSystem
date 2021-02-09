using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class GoodsDTO
    {
        /// <summary>
        /// 衣品id
        /// </summary>
        public int goods_id { get; set; }

       /// <summary>
       /// 衣品主图
       /// </summary>
        public int base_image_id { get; set; }

        /// <summary>
        /// 主图路径
        /// </summary>
        public string path { get; set; }

        /// <summary>
        /// 详情图列表
        /// </summary>
        public List<GoodsImageDTO> image_list{ get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string descript { get; set; }
        /// <summary>
        /// 品名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 原价格
        /// </summary>
        public string orign_price { get; set; }

        /// <summary>
        /// 实际价格
        /// </summary>
        public string real_price { get; set; }
    }

    public class GoodsImageDTO
    {
        public int type { get; set; }

        public int base_image_id { get; set; }

        public string path { get; set; }
    }

}