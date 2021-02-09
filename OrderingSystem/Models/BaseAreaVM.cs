
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{
    /// <summary>
    /// 地区VM
    /// </summary>
    public class BaseAreaVM
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 地区信息
        /// </summary>
        public BaseArea Area { get; set; }
        /// <summary>
        /// 省信息
        /// </summary>
        public List<BaseArea> Provinces { get; set; }
        /// <summary>
        /// 分页
        /// </summary>
        public Paging<BaseArea> Paging { get; set; }

        //查询条件
        public string QueryName { get; set; }
        public int QueryProvinceId { get; set; }
    }
}