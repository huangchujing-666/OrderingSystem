
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System.Collections.Generic;

namespace OrderingSystem.Admin.Models
{
    /// <summary>
    /// 文章VM
    /// </summary>
    public class JourneyArticleVM : BaseImgInfoVM
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 基础字典信息
        /// </summary>
        public JourneyArticle JourneyArticle { get; set; }
        /// <summary>
        /// 分页
        /// </summary>
        public Paging<JourneyArticle> Paging { get; set; } 

        /// <summary>
        /// 商家列表
        /// </summary>
        public List<BusinessInfo> BusinessList { get; set; }

        //查询条件
        public string QueryName { get; set; } 
        public string QueryBusinessName { get; set; }
    }
}