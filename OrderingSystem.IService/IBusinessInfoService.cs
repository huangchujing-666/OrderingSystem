using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IBusinessInfoService
    {
        BusinessInfo GetById(int id);

        BusinessInfo Insert(BusinessInfo model);

        List<BusinessInfo> GetList(int pageIndex, int pageSize, out int totalCount);

        List<BusinessInfo> GetListByGroup(int module,int businessgroupId,int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 根据商家类型获取商家列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<BusinessInfo> GetListByType(int type, int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(BusinessInfo model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(BusinessInfo model);

        /// <summary>
        /// 获取酒店列表
        /// </summary> 
        /// <returns></returns>
        List<BusinessInfo> GetHotelList();

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<BusinessInfo> GetManagerList(string name, int type, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 根据定位查询附近商家
        /// </summary>
        /// <param name="Longitude">经度</param>
        /// <param name="Latitude">纬度</param>
        /// <returns></returns>
        List<BusinessInfo> GetListByLocation(int BusinessTypeId, double Longitude, double Latitude, int Page_Index, int Page_Size, out int total_count, int BusinessGroupId = 0);

        /// <summary>
        /// 根据id列表获取数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        List<BusinessInfo> GetListByIds(List<int> ids);

        /// <summary>
        /// 根据搜索关键字获取商家信息
        /// </summary>
        /// <param name="search_name">关键字</param>
        /// <param name="pageIndex">第几页（默认为1，第一页）</param>
        /// <param name="pageSize">每页显示多少条数据</param>
        /// <param name="totalCount">返回总页数</param>
        /// <returns></returns>
        List<BusinessInfo> GetBusinessInfoBySearch(string search_name, int pageIndex, int pageSize, out int totalCount, int business_type_id = 0, int business_group_id = 0);

        /// <summary>
        /// 根据搜索站点获取商家信息
        /// </summary>
        /// <param name="station_id">站点id</param>
        /// <param name="pageIndex">第几页（默认为1，第一页）</param>
        /// <param name="pageSize">每页显示多少条数据</param>
        /// <param name="totalCount">返回总页数</param>
        /// <returns></returns>
        List<BusinessInfo> GetBusinessInfoByStation(int station_id,int business_type_id,int business_group_id,  int pageIndex, int pageSize, out int totalCount);

        /// <summary>
        /// 酒店首页筛选
        /// </summary>
        /// <param name="module"></param>
        /// <param name="businessGroupId"></param>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <param name="hotelCategoryId"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        //List<BusinessInfo> GetListByManCond(int module, int businessGroupId, decimal minPrice, decimal maxPrice, int hotelCategoryId, int page_Index, int page_Size, out int total_count);

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
        List<BusinessInfo> GetListByManCond(int[] grade, int[] hotelCategoryId, int[] price, int page_Index, int page_Size, out int total_count);

        /// <summary>
        /// 酒店筛选 不根据定位
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
        List<BusinessInfo> GetListByLocationAndCond(int[] grade, int[] hotelCategoryId, int[] price, double longitude, double latitude, int page_Index, int page_Size, out int total_count);
        
        /// <summary>
        /// 主页模糊搜索商家
        /// </summary>
        /// <param name="search_Name"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        List<BusinessInfo> GetIndexBusinessInfoBySearch(string search_Name, int page_Index, int page_Size, out int total_count);

        /// <summary>
        /// 主页每日推荐
        /// </summary>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        List<BusinessInfo> GetIndexActivityBusinessInfo();

        /// <summary>
        /// 主页置顶商家
        /// </summary>
        /// <returns></returns>
        List<BusinessInfo> GetIndexTopBusinessInfo();

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
        List<BusinessInfo> GetIndexListByLocation(string search_Name, double longitude, double latitude, int page_Index, int page_Size, out int total_count);

        /// <summary>
        /// 衣模块获取商家列表
        /// </summary>
        /// <param name="module"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        List<BusinessInfo> GetGoodsBusinessInfoByModule(string search_Name,int module, out int total_count);


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
        List<BusinessInfo> GetGoodsBusinessInfoByGroupIdWithLocation(int businessGroup_Id,string District, double longitude, double latitude, int module, int page_Index, int page_Size, out int total_count);

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
        List<BusinessInfo> GetIndexBusinessInfoIsTop(string search_Name, int module, int page_Index, int page_Size, out int total_count);
    }
}
