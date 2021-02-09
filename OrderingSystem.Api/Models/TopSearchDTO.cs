using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class TopSearchDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int TopSearchId { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
        public int SortNo { get; set; }
    }
}