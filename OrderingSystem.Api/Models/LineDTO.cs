using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    /// <summary>
    /// 线路数据返回模型
    /// </summary>
    public class LineDTO
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int line_id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string line_name { get; set; }
        /// <summary>
        /// 线路图片
        /// </summary>
        public string line_image { get; set; } 
        /// <summary>
        /// 站点列表
        /// </summary>
        public List<StationDTO> station_list { get; set; }	

    }
    public class StationDTO
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int station_id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string station_name { get; set; }
    }

}