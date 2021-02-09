
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{
    /// <summary>
    /// 站点VM
    /// </summary>
    public class BaseStationVM
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 站点信息
        /// </summary>
        public BaseStation Station { get; set; }
  
        public Paging<BaseStation> Paging { get; set; }
        /// <summary>
        /// 省集合
        /// </summary>
        public List<BaseArea> Provinces { get; set; }
        /// <summary>
        /// 市集合
        /// </summary>
        public List<BaseArea> Citys { get; set; }
        public BaseArea City { get; set; }
        /// <summary>
        /// 线路集合
        /// </summary>
        public List<BaseLine> Lines { get; set; }

        //查询条件
        public string QueryStationName { get; set; }
        public string QueryLineName { get; set; }
        public string QueryCityName { get; set; }
    }
}