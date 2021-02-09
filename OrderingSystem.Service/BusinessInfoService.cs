using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Business;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Service
{
    public class BusinessInfoService : IBusinessInfoService
    {
        /// <summary>
        /// The BusinessInfo biz
        /// </summary>
        private IBusinessInfoBusiness _BusinessInfoBiz;

        public BusinessInfoService(IBusinessInfoBusiness BusinessInfoBiz)
        {
            _BusinessInfoBiz = BusinessInfoBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessInfo GetById(int id)
        {
            return this._BusinessInfoBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BusinessInfo Insert(BusinessInfo model)
        {
            return this._BusinessInfoBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessInfo model)
        {
            this._BusinessInfoBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessInfo model)
        {
            this._BusinessInfoBiz.Delete(model);
        }

        /// <summary>
        /// 获取酒店列表
        /// </summary> 
        /// <returns></returns>
        public List<BusinessInfo> GetHotelList()
        {
           return  this._BusinessInfoBiz.GetHotelList();
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetManagerList(string name, int type, int pageNum, int pageSize, out int totalCount)
        {
            return this._BusinessInfoBiz.GetManagerList(name, type, pageNum, pageSize, out totalCount);
        }

        public List<BusinessInfo> GetList(int pageIndex, int pageSize, out int totalCount)
        {
            return this._BusinessInfoBiz.GetList(pageIndex, pageSize, out totalCount);
        }

        public List<BusinessInfo> GetListByGroup(int module, int businessgroupId, int pageIndex, int pageSize, out int totalCount)
        {
            return this._BusinessInfoBiz.GetListByGroup(module, businessgroupId, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 根据商家类型获取商家列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetListByType(int type, int pageIndex, int pageSize, out int totalCount)
        {
            return this._BusinessInfoBiz.GetListByType(type, pageIndex, pageSize, out totalCount);
        }

        /// <summary>
        /// 根据经纬度获取附近商家信息
        /// </summary>
        /// <param name="Longitude"></param>
        /// <param name="Latitude"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetListByLocation(int BusinessTypeId, double Longitude, double Latitude, int Page_Index, int Page_Size, out int total_count, int BusinessGroupId = 0)
        {
            return this._BusinessInfoBiz.GetListByLocation(BusinessTypeId, Longitude, Latitude, Page_Index, Page_Size, out total_count, BusinessGroupId);
        }

        /// <summary>
        /// 根据id列表获取数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetListByIds(List<int> ids)
        {
            return this._BusinessInfoBiz.GetListByIds(ids);
        }

        /// <summary>
        /// 根据搜索关键字获取商家信息
        /// </summary>
        /// <param name="search_name">关键字</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页显示多少条数据</param>
        /// <param name="totalCount">返回总页数</param>
        /// <returns></returns>
        public List<BusinessInfo> GetBusinessInfoBySearch(string search_name, int pageIndex, int pageSize, out int totalCount, int business_type_id = 0, int business_group_id = 0)
        {
            return this._BusinessInfoBiz.GetBusinessInfoBySearch(search_name, pageIndex, pageSize, out totalCount, business_type_id, business_group_id);
        }

        /// <summary>
        /// 主页模糊搜索商家
        /// </summary>
        /// <param name="search_Name"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetIndexBusinessInfoBySearch(string search_Name, int page_Index, int page_Size, out int total_count) {
            return this._BusinessInfoBiz.GetIndexBusinessInfoBySearch(search_Name, page_Index, page_Size, out total_count);
        }

        /// <summary>
        /// 根据搜索站点获取商家信息
        /// </summary>
        /// <param name="station_id">站点id</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页显示多少条数据</param>
        /// <param name="totalCount">返回总页数</param>
        /// <returns></returns>
        public List<BusinessInfo> GetBusinessInfoByStation(int station_id, int business_type_id, int business_group_id, int pageIndex, int pageSize, out int totalCount)
        {
            return this._BusinessInfoBiz.GetBusinessInfoByStation(station_id, business_type_id, business_group_id, pageIndex, pageSize, out totalCount);
        }

        //public List<BusinessInfo> GetListByManCond(int module, int businessGroupId, decimal minPrice, decimal maxPrice, int hotelCategoryId, int page_Index, int page_Size, out int total_count)
        //{
        //    return this._BusinessInfoBiz.GetListByManCond(module, businessGroupId, minPrice, maxPrice, hotelCategoryId, page_Index, page_Size, out total_count);
        //}

        /// <summary>
        /// 酒店首页筛选
        /// </summary>
        /// <param name="grade">等级</param>
        /// <param name="hotelCategoryId">酒店类别</param>
        /// <param name="price">价格</param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
       public  List<BusinessInfo> GetListByManCond(int[] grade, int[] hotelCategoryId, int[] price, int page_Index, int page_Size, out int total_count)
        {
            return this._BusinessInfoBiz.GetListByManCond(grade, hotelCategoryId, price, page_Index, page_Size, out total_count);
        }

        /// <summary>
        /// 酒店筛选 根据定位排序
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="hotelCategoryId"></param>
        /// <param name="price"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetListByLocationAndCond(int[] grade, int[] hotelCategoryId, int[] price, double longitude, double latitude, int page_Index, int page_Size, out int total_count)
        {
            return this._BusinessInfoBiz.GetListByLocationAndCond(grade, hotelCategoryId, price, longitude, latitude,page_Index, page_Size, out total_count);
        }


        /// <summary>
        /// 主页优惠商家
        /// </summary>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetIndexActivityBusinessInfo()
        {
            return this._BusinessInfoBiz.GetIndexActivityBusinessInfo();
        }
        /// <summary>
        /// 置顶商家
        /// </summary>
        /// <returns></returns>
        public List<BusinessInfo> GetIndexTopBusinessInfo()
        {
            return this._BusinessInfoBiz.GetIndexTopBusinessInfo();
        }

        /// <summary>
        /// 娱乐主页模糊搜索，获取附近商家
        /// </summary>
        /// <param name="search_Name"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetIndexListByLocation(string search_Name, double longitude, double latitude, int page_Index, int page_Size, out int total_count)
        {
            return this._BusinessInfoBiz.GetIndexListByLocation(search_Name, longitude, latitude,  page_Index, page_Size, out total_count);
        }


        /// <summary>
        /// 衣模块获取商家列表
        /// </summary>
        /// <param name="module"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetGoodsBusinessInfoByModule(string search_Name,int module, out int total_count)
        {
            return this._BusinessInfoBiz.GetGoodsBusinessInfoByModule(search_Name,module,out total_count);
        }


        /// <summary>
        /// 根据商家分组，定位查询
        /// </summary>
        /// <param name="businessGroup_Id"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="module"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetGoodsBusinessInfoByGroupIdWithLocation(int businessGroup_Id,string District, double longitude, double latitude, int module, int page_Index, int page_Size, out int total_count)
        {
            return this._BusinessInfoBiz.GetGoodsBusinessInfoByGroupIdWithLocation(businessGroup_Id, District, longitude, latitude,module, page_Index, page_Size, out total_count);
        }

        /// <summary>
        /// 获取主页优选商家/置顶商家
        /// </summary>
        /// <param name="search_Name"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="module"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetIndexBusinessInfoIsTop(string search_Name, int module, int page_Index, int page_Size, out int total_count)
        {
            return this._BusinessInfoBiz.GetIndexBusinessInfoIsTop(search_Name, module, page_Index, page_Size, out total_count);
        }
    }
}
