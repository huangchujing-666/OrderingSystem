using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    /// <summary>
    /// 首页轮播图
    /// </summary>
    public class Carousel
    {
        /// <summary>
        /// 轮播图Id
        /// </summary>
        public int carousel_id { get; set; }
        /// <summary>
        /// 轮播图名称
        /// </summary>
        public string carousel_name { get; set; }

        /// <summary>
        /// 商家图片id
        /// </summary>
        public int carousel_img_id { get; set; }

        /// <summary>
        /// 商家图片路径
        /// </summary>
        public string carousel_img_path { get; set; }

        /// <summary>
        /// 跳转商家
        /// </summary>
        public int business_id { get; set; }

        /// <summary>
        /// 图片排序
        /// </summary>
        public int sort_no { get; set; }

    }
}