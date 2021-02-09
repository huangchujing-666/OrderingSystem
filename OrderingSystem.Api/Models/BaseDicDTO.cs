using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class BaseDicDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int base_dic_id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int sort_no { get; set; }
    }
}