
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{
    /// <summary>
    /// 线路VM
    /// </summary>
    public class BaseLineVM : BaseImgInfoVM
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 基础字典信息
        /// </summary>
        public BaseLine Line { get; set; }
        /// <summary>
        /// 分页
        /// </summary>
        public Paging<BaseLine> Paging { get; set; }
        /// <summary>
        /// 省集合
        /// </summary>
        public List<BaseArea> Provinces { get; set; }
        /// <summary>
        /// 市集合
        /// </summary>
        public List<BaseArea> Citys { get; set; }
        public BaseArea City { get; set; }

        //查询条件
        public string QueryLineName { get; set; }
        public string QueryCityName { get; set; }
    }
}