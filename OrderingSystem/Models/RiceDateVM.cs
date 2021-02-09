
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{
    /// <summary>
    /// 拼饭VM
    /// </summary>
    public class RiceDateVM
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 模型
        /// </summary>
        public RiceDate RiceDate { get; set; }
        /// <summary>
        /// 图片信息
        /// </summary>
        public List<BaseImage> BaseImageList { get; set; } 

        /// <summary>
        /// 分页
        /// </summary>
        public Paging<RiceDate> Paging { get; set; }

        //查询条件
        public string QueryName { get; set; } 
    }
}