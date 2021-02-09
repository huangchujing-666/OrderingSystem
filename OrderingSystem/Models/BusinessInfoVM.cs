 
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Admin.Models
{
    public class BusinessInfoVM : BaseImgInfoVM
    {
        public int V { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 菜单信息
        /// </summary>
        public BusinessInfo BusinessInfo { get; set; }
        /// <summary>
        /// 菜单集合
        /// </summary>
        public List<BusinessInfo> BusinessInfos { get; set; }

        public List<SysAccount> SysAccountList { get; set; }
        public List<BusinessGroup> BusinessGroupList { get; set; }
        public List<HotelRelateCategory> HotelRelateCategoryList { get; set; }
        public List<HotelCategory> HotelCategoryList { get; set; }

        /// <summary>
        /// 分页
        /// </summary>
        public Paging<BusinessInfo> Paging { get; set; }

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
        /// <summary>
        /// 站点集合
        /// </summary>
        public List<BaseStation> Stations { get; set; }

        //查询条件
        public string QueryName { get; set; }
        public int QueryType { get; set; }

        /// <summary>
        /// 获取当前用户角色
        /// </summary>
        public int RoleId { get; set; }

    }

    public class AroundVM {
        public string status { get; set; }
        public string count { get; set; }
        public string info { get; set; }
        public string infocode { get; set; }
        public List<PointVM> pois { get; set; } 
    }

    public class PointVM {
        public string id { get; set; }
        public string name { get; set; }
        public string distance { get; set; } 
    }

}