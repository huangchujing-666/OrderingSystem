using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    /// <summary>
    /// 乐模块商家分组对象
    /// </summary>
    public class BusinessGroupDTO
    {
        /// <summary>
        /// 商家分组id
        /// </summary>
        public int business_group_id { get; set; }
        /// <summary>
        /// 商家分组名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 图片id
        /// </summary>
        public int base_image_id { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int sort { get; set; }

        public List<BusinessGroupImageDTO> BusinessGroupImageList { get; set; }
    }

    public class BusinessGroupImageDTO
    {
        public int type { get; set; }

        public string path { get; set; }
    }
}