using OrderingSystem.Core.Infrastructure;
using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderingSystem.Api.Models;
using OrderingSystem.Cache.Redis;
using OrderingSystem.Domain.Model;
using System.Text;
using OrderingSystem.Core.Utils;
using static OrderingSystem.Domain.EnumHelp;
using OrderingSystem.Domain;
using OrderingSystem.Api.Common;

namespace OrderingSystem.Api.Controllers
{
    /// <summary>
    /// 商家基本信息接口
    /// </summary>
    public class BusinessInfoApiController : ApiController
    {
        private readonly IBusinessInfoService _businessInfoService = EngineContext.Current.Resolve<IBusinessInfoService>();
        private readonly IActivityMinusService _activityMinusService = EngineContext.Current.Resolve<IActivityMinusService>();
        private readonly IActivityDiscountService _activityDiscountService = EngineContext.Current.Resolve<IActivityDiscountService>();
        private readonly IDishesService _dishesService = EngineContext.Current.Resolve<IDishesService>();
        private readonly IBaseLineService _baseLineService = EngineContext.Current.Resolve<IBaseLineService>();
        //private readonly IDishesRelateLableService _dishesRelateLableService = EngineContext.Current.Resolve<IDishesRelateLableService>();
        //private readonly IDishesRelateSpecService _dishesRelateSpecService = EngineContext.Current.Resolve<IDishesRelateSpecService>();
        private readonly IDishesLableService _dishesLableService = EngineContext.Current.Resolve<IDishesLableService>();
        private readonly IDishesSpecService _dishesSpecService = EngineContext.Current.Resolve<IDishesSpecService>();
        private readonly IBaseImageService _baseImageService = EngineContext.Current.Resolve<IBaseImageService>();
        private readonly IBusinessImageService _businessImageService = EngineContext.Current.Resolve<IBusinessImageService>();
        private readonly IOrderService _orderService = EngineContext.Current.Resolve<IOrderService>();
        private readonly IRoomService _roomService = EngineContext.Current.Resolve<IRoomService>();
        private readonly IBusinessGroupService _businessGroupService = EngineContext.Current.Resolve<IBusinessGroupService>();
        private readonly IBaseDicService _baseDicService = EngineContext.Current.Resolve<IBaseDicService>();

        #region 娱乐模块主页
        /// <summary>
        /// 主页商家搜索
        /// </summary>
        /// <param name="Search_Name"></param>
        /// <param name="Page_Index"></param>
        /// <param name="Page_Size"></param>
        /// <param name="Longitude"></param>
        /// <param name="Latitude"></param>
        /// <returns></returns>
        public ResponseModel<List<BusinessDTO>> GetIndexBusinessInfoBySearch(string Search_Name, int Page_Index = 1, int Page_Size = 20, double Longitude = 0, double Latitude = 0)
        {
            var result = new ResponseModel<List<Models.BusinessDTO>>();
            List<Models.BusinessDTO> list = new List<Models.BusinessDTO>();
            //查询缓存数据
            string key = "GetIndexBusinessInfoBySearch" + Search_Name + "_" + Page_Index + "_" + Longitude + "_" + Latitude;
            string key_count = "GetIndexBusinessInfoCountBySearch" + Search_Name + "_" + Page_Index + "_" + Longitude + "_" + Latitude;
            if (RedisDb._redisHelper_11().KeyExists(key))
            {
                var data = RedisDb._redisHelper_11().StringGet<List<Models.BusinessDTO>>(key);
                result.data = data;
                result.total_count = RedisDb._redisHelper_11().StringGet<int>(key_count);
            }
            else
            {
                int total_count = 0;
                var getList = _businessInfoService.GetIndexListByLocation(Search_Name, Longitude, Latitude, Page_Index, Page_Size, out total_count);
                //var getList = _businessInfoService.GetIndexBusinessInfoBySearch(Search_Name, Page_Index, Page_Size, out total_count);
                if (getList != null && getList.Count > 0)
                {
                    var searchresult = _businessInfoService.GetListByIds(getList.Select(p => p.BusinessInfoId).ToList());
                    foreach (var item in searchresult)
                    {
                        var activityDiscount = item.ActivityDiscount;
                        var activityMinus = item.ActivityMinusList;
                        //var dishesList = item.DishesList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).Take(3).ToList();
                        int comment_count = 0;
                        if (item.BusinessCommentList != null && item.BusinessCommentList.Count > 0)
                        {
                            var comm_list = item.BusinessCommentList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).ToList();
                            if (comm_list != null && comm_list.Count > 0)
                            {
                                comment_count = comm_list.Count;
                            }
                        }
                        list.Add(new Models.BusinessDTO()
                        {
                            average_pay = item.AveragePay.ToString(),
                            business_id = item.BusinessInfoId,
                            business_img_id = item.BaseImage == null ? 0 : item.BaseImage.BaseImageId,
                            business_name = item.Name,
                            business_img_path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path,
                            business_rank = item.Grade.ToString(),//Math.Round(item.Grade, 1),
                            mobile = item.Mobile,
                            address = item.Address,
                            business_hour = item.BusinessHour,
                            latitude = item.Latitude.ToString(),
                            longitude = item.Longitude.ToString(),
                            introduction = item.Introduction,
                            notic = item.Notic,
                            base_VR_image_list = BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.实景图).ToList()),
                            base_BN_image_list = BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.展示图).ToList()),
                            base_PR_image_list = BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.全景图).ToList()),
                            month_order_count = item.OrderCountPerMonth.ToString(),
                            manjian_info = MinusListToString(activityMinus),
                            activitylist = GetActivityStrList(item.ActivityMinusList, item.ActivityDiscount),
                            discount_info = DiscountToString(activityDiscount),
                            distance = LongitudeToolService.GetDistance(Longitude, Latitude, Convert.ToDouble(item.Longitude.ToString() == "" ? 0 : item.Longitude), Convert.ToDouble(item.Latitude.ToString() == "" ? 0 : item.Latitude)),
                            distance_descript = item.Services,
                            service = item.Services,
                            min_consume = (item.ProductList == null || item.ProductList.Count <= 0) ? "0.00" : item.ProductList.Min(p => p.RealPrice).ToString(),
                            comment_count = comment_count,//(item.BusinessCommentList == null || item.BusinessCommentList.Count <= 0) ? 0 : item.BusinessCommentList.Count,
                            good_comment_rate = GetGoodCommentRate(item.BusinessCommentList),
                            BusinessLable = GetBusinessLableList(item.BusinessLableList),
                            District = item.District,
                            business_type_id = item.BusinessTypeId
                        });
                    }
                }
                result.data = list.OrderBy(c => c.distance).ToList();
                result.total_count = total_count;
                //设置缓存数据
                RedisDb._redisHelper_11().StringSet(key, list, RedisConfig._defaultExpiry);
                RedisDb._redisHelper_11().StringSet(key_count, total_count, RedisConfig._defaultExpiry);
            }
            return result;
        }

        /// <summary>
        /// 每日优惠
        /// </summary>
        /// <param name="Page_Index"></param>
        /// <param name="Page_Size"></param>
        /// <param name="Longitude"></param>
        /// <param name="Latitude"></param>
        /// <returns></returns>
        public ResponseModel<List<BusinessDTO>> GetIndexActivityBusinessInfo(int Page_Index = 1, int Page_Size = 20, double Longitude = 0, double Latitude = 0)
        {
            var result = new ResponseModel<List<BusinessDTO>>();
            result.message = "";
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            List<BusinessDTO> list = new List<BusinessDTO>();
            int total_count = 0;
            string key = "GetIndexActivityBusinessInfo" + Longitude + "_" + Latitude + "_" + Page_Index + "_" + Page_Size;
            string key_count = "GetIndexActivityCountBusinessInfo" + Longitude + "_" + Latitude + "_" + Page_Index + "_" + Page_Size;
            if (false)//RedisDb._redisHelper_11().KeyExists(key)
            {
                //var data = RedisDb._redisHelper_11().StringGet<List<BusinessDTO>>(key);
                //result.data = data;
                //result.total_count = RedisDb._redisHelper_11().StringGet<int>(key_count);
                list = RedisDb._redisHelper_11().StringGet<List<BusinessDTO>>(key);
                total_count = RedisDb._redisHelper_11().StringGet<int>(key_count);
            }
            else
            {
                List<BusinessInfo> getList = new List<BusinessInfo>();
                //1 获取置顶商家
                var topList = _businessInfoService.GetIndexTopBusinessInfo();//.Skip(page_Size * (page_Index - 1)).Take(page_Size)
                if (topList != null && topList.Count > 0)
                {
                    getList = topList.Skip(Page_Size * (Page_Index - 1)).Take(Page_Size).ToList();
                }
                else
                {
                    var activitylist = _businessInfoService.GetIndexActivityBusinessInfo();
                    if (activitylist != null && activitylist.Count > 0)//2.获取优惠商家
                    {
                        getList = activitylist.Skip(Page_Size * (Page_Index - 1)).Take(Page_Size).ToList();
                    }
                    else
                    {
                        //3 获取定位商家
                        var byLocationList = _businessInfoService.GetIndexListByLocation("", Longitude, Latitude, Page_Index, Page_Size, out total_count).ToList();
                        if (byLocationList != null && byLocationList.Count > 0)
                        {
                            getList = _businessInfoService.GetListByIds(byLocationList.Select(p => p.BusinessInfoId).ToList());
                        }
                    }
                }
                if (getList != null)
                {
                    foreach (var item in getList)
                    {
                        var bto = new Models.BusinessDTO()
                        {
                            business_id = item.BusinessInfoId,
                            business_name = item.Name,
                            base_VR_image_list = BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.实景图).ToList()),
                            base_BN_image_list = BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.展示图).ToList()),
                            base_PR_image_list = BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.全景图).ToList()),
                            // min_consume = (item.ProductList == null || item.ProductList.Count <= 0) ? "0.00" : item.ProductList.Min(p => p.RealPrice).ToString(),
                            distance = LongitudeToolService.GetDistance(Longitude, Latitude, Convert.ToDouble(item.Longitude.ToString() == "" ? 0 : item.Longitude), Convert.ToDouble(item.Latitude.ToString() == "" ? 0 : item.Latitude)),
                            District = item.District,
                            business_type_id = item.BusinessTypeId
                        };
                        switch (item.BusinessTypeId)
                        {
                            case (int)EnumHelp.BusinessTypeEnum.乐:
                                bto.min_consume = (item.ProductList == null || item.ProductList.Count <= 0) ? "0.00" : item.ProductList.Min(p => p.RealPrice).ToString();
                                break;
                            case (int)EnumHelp.BusinessTypeEnum.景点:
                                bto.min_consume = (item.BusinessTicketList == null || item.BusinessTicketList.Count <= 0) ? "0.00" : item.BusinessTicketList.Min(p => p.RealPrice).ToString();
                                break;
                            case (int)EnumHelp.BusinessTypeEnum.酒店:
                                bto.min_consume = (item.BusinessRoomList == null || item.BusinessRoomList.Count <= 0) ? "0.00" : item.BusinessRoomList.Min(p => p.RealPrice).ToString();
                                break;
                            default:
                                bto.min_consume = "0.00";
                                break;
                        }
                        list.Add(bto);
                    }
                    total_count = getList.Count;
                }
                result.data = list;
                result.total_count = total_count;
                //设置缓存数据
                RedisDb._redisHelper_11().StringSet(key, list, RedisConfig._defaultExpiry);
                RedisDb._redisHelper_11().StringSet(key_count, total_count, RedisConfig._defaultExpiry);
            }
            return result;
        }

        /// <summary>
        /// 获取周边精选
        /// </summary>
        /// <param name="Page_Index"></param>
        /// <param name="Page_Size"></param>
        /// <param name="Longitude"></param>
        /// <param name="Latitude"></param>
        /// <returns></returns>
        public ResponseModel<List<BusinessDTO>> GetIndexSurroundBusinessInfo(int Page_Index = 1, int Page_Size = 20, double Longitude = 0, double Latitude = 0)
        {
            var result = new ResponseModel<List<Models.BusinessDTO>>();
            List<Models.BusinessDTO> list = new List<Models.BusinessDTO>();
            //查询缓存数据
            string key = "GetIndexBusinessInfoBySearch_" + Page_Index + "_" + Page_Size + "_" + Longitude + "_" + Latitude;
            string key_count = "GetIndexBusinessInfoCountBySearch_" + Page_Index + "_" + Page_Size + "_" + Longitude + "_" + Latitude;
            if (RedisDb._redisHelper_11().KeyExists(key))
            {
                var data = RedisDb._redisHelper_11().StringGet<List<Models.BusinessDTO>>(key);
                result.data = data;
                result.total_count = RedisDb._redisHelper_11().StringGet<int>(key_count);
            }
            else
            {
                int total_count = 0;
                var getList = _businessInfoService.GetIndexListByLocation("", Longitude, Latitude, Page_Index, Page_Size, out total_count);
                //var getList = _businessInfoService.GetIndexBusinessInfoBySearch(Search_Name, Page_Index, Page_Size, out total_count);
                if (getList != null && getList.Count > 0)
                {
                    var searchresult = _businessInfoService.GetListByIds(getList.Select(p => p.BusinessInfoId).ToList());
                    foreach (var item in searchresult)
                    {
                        var activityDiscount = item.ActivityDiscount;
                        var activityMinus = item.ActivityMinusList;
                        //var dishesList = item.DishesList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).Take(3).ToList();
                        int comment_count = 0;
                        if (item.BusinessCommentList != null && item.BusinessCommentList.Count > 0)
                        {
                            var comm_list = item.BusinessCommentList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).ToList();
                            if (comm_list != null && comm_list.Count > 0)
                            {
                                comment_count = comm_list.Count;
                            }
                        }
                        list.Add(new Models.BusinessDTO()
                        {
                            average_pay = item.AveragePay.ToString(),
                            business_id = item.BusinessInfoId,
                            business_img_id = item.BaseImage == null ? 0 : item.BaseImage.BaseImageId,
                            business_name = item.Name,
                            business_img_path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path,
                            business_rank = item.Grade.ToString(),//Math.Round(item.Grade, 1),
                            grade = item.Grade,
                            mobile = item.Mobile,
                            address = item.Address,
                            business_hour = item.BusinessHour,
                            latitude = item.Latitude.ToString(),
                            longitude = item.Longitude.ToString(),
                            introduction = item.Introduction,
                            notic = item.Notic,
                            base_VR_image_list = BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.实景图).ToList()),
                            base_BN_image_list = BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.展示图).ToList()),
                            base_PR_image_list = BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.全景图).ToList()),
                            month_order_count = item.OrderCountPerMonth.ToString(),
                            manjian_info = MinusListToString(activityMinus),
                            activitylist = GetActivityStrList(item.ActivityMinusList, item.ActivityDiscount),
                            discount_info = DiscountToString(activityDiscount),
                            distance = LongitudeToolService.GetDistance(Longitude, Latitude, Convert.ToDouble(item.Longitude.ToString() == "" ? 0 : item.Longitude), Convert.ToDouble(item.Latitude.ToString() == "" ? 0 : item.Latitude)),
                            distance_descript = item.Distance,
                            service = item.Services,
                            min_consume = (item.ProductList == null || item.ProductList.Count <= 0) ? "0.00" : item.ProductList.Min(p => p.RealPrice).ToString(),
                            comment_count = comment_count,//(item.BusinessCommentList == null || item.BusinessCommentList.Count <= 0) ? 0 : item.BusinessCommentList.Count,
                            good_comment_rate = GetGoodCommentRate(item.BusinessCommentList),
                            BusinessLable = GetBusinessLableList(item.BusinessLableList),
                            District = item.District,
                            business_type_id = item.BusinessTypeId
                        });
                    }
                }
                result.data = list.OrderBy(c => c.distance).ThenByDescending(c => c.grade).ToList();
                result.total_count = total_count;
                //设置缓存数据
                RedisDb._redisHelper_11().StringSet(key, result.data, RedisConfig._defaultExpiry);
                RedisDb._redisHelper_11().StringSet(key_count, total_count, RedisConfig._defaultExpiry);
            }
            return result;
        }

        #endregion

        /// <summary>
        ///  首页推荐，附近商家信息数据查询
        /// </summary>
        /// <param name="Longitude">经度</param>
        /// <param name="Latitude">纬度</param>
        /// <param name="Module">模块Id</param>
        /// <param name="BusinessGroupId"></param>
        /// <param name="Page_Index"></param>
        /// <param name="Page_Size"></param>
        /// <returns></returns>
        public ResponseModel<List<BusinessDTO>> GetBusinessInfoNearby(double Longitude = 0, double Latitude = 0, int Module = 0, int BusinessGroupId = 0, int Page_Index = 1, int Page_Size = 20, int PriorityType = 0)
        {
            var result = new ResponseModel<List<BusinessDTO>>();
            result.message = "";
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            List<BusinessDTO> list = new List<BusinessDTO>();
            int total_count = 0;
            string key = "GetBusinessInfoNearby_" + Longitude + "_" + Latitude + "_" + Module + "_" + BusinessGroupId + "_" + Page_Index + "_" + Page_Size + "_" + PriorityType;
            string key_count = "GetBusinessInfoCountNearby_" + Longitude + "_" + Latitude + "_" + Module + "_" + BusinessGroupId + "_" + Page_Index + "_" + Page_Size + "_" + PriorityType;
            if (RedisDb._redisHelper_11().KeyExists(key) && Module == (int)EnumHelp.BusinessTypeEnum.食)//
            {
                //var data = RedisDb._redisHelper_11().StringGet<List<BusinessDTO>>(key);
                //result.data = data;
                //result.total_count = RedisDb._redisHelper_11().StringGet<int>(key_count);
                list = RedisDb._redisHelper_11().StringGet<List<BusinessDTO>>(key);
                total_count = RedisDb._redisHelper_11().StringGet<int>(key_count);
            }
            else
            {
                List<BusinessInfo> getList = new List<BusinessInfo>();
                if (Module > 0)
                {
                    if (Longitude == 0 && Latitude == 0)
                    {
                        //getList = _businessInfoService.GetList(Page_Index, Page_Size, out total_count).ToList();
                        getList = _businessInfoService.GetListByGroup(Module, BusinessGroupId, Page_Index, Page_Size, out total_count).ToList();
                    }
                    else
                    {
                        //获取定位的商家id列表
                        var getBusinessInfoIdList = _businessInfoService.GetListByLocation(Module, Longitude, Latitude, Page_Index, Page_Size, out total_count, BusinessGroupId);
                        getList = _businessInfoService.GetListByIds(getBusinessInfoIdList.Select(p => p.BusinessInfoId).ToList());
                    }
                }
                //绑定数据
                foreach (var item in getList)
                {
                    int comment_count = 0;
                    if (item.BusinessCommentList != null && item.BusinessCommentList.Count > 0)
                    {
                        var comm_list = item.BusinessCommentList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).ToList();
                        if (comm_list != null && comm_list.Count > 0)
                        {
                            comment_count = comm_list.Count;
                        }
                    }
                    list.Add(new Models.BusinessDTO()
                    {
                        average_pay = item.AveragePay.ToString(),
                        business_id = item.BusinessInfoId,
                        business_img_id = item.BaseImage == null ? 0 : item.BaseImage.BaseImageId,
                        business_name = item.Name,
                        business_img_path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path,
                        business_rank = item.Grade.ToString(),
                        mobile = item.Mobile,
                        address = item.Address,
                        business_hour = item.BusinessHour,
                        latitude = item.Latitude.ToString(),
                        longitude = item.Longitude.ToString(),
                        introduction = item.Introduction,
                        notic = item.Notic,
                        base_VR_image_list = BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.实景图).ToList()),
                        base_BN_image_list = BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.展示图).ToList()),
                        base_PR_image_list = BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.全景图).ToList()),
                        //dishes_cate_list = item.DishesList == null ? new List<DishesCategoryDTO>() : DishesListToCateDTO(item.DishesList),
                        month_order_count = item.OrderCountPerMonth.ToString(),
                        manjian_info = MinusListToString(item.ActivityMinusList),
                        discount_info = DiscountToString(item.ActivityDiscount),
                        activitylist = GetActivityStrList(item.ActivityMinusList, item.ActivityDiscount),
                        dishes_list = DishesListToDTO(item.DishesList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).Take(3).ToList()),
                        distance = LongitudeToolService.GetDistance(Longitude, Latitude, Convert.ToDouble(item.Longitude.ToString() == "" ? 0 : item.Longitude), Convert.ToDouble(item.Latitude.ToString() == "" ? 0 : item.Latitude)),
                        distance_descript = item.Services,
                        service = item.Services,
                        min_consume = (item.ProductList == null || item.ProductList.Count <= 0) ? "0.00" : item.ProductList.Min(p => p.RealPrice).ToString(),
                        comment_count = comment_count,//(item.BusinessCommentList == null || item.BusinessCommentList.Count <= 0) ? 0 : .ToList().Count,
                        good_comment_rate = GetGoodCommentRate(item.BusinessCommentList),
                        BusinessLable = GetBusinessLableList(item.BusinessLableList),
                        District = item.District,
                        business_type_id = item.BusinessTypeId
                    });
                }
                //list = list.OrderBy(p => p.distance).ToList();
                //result.data = list;
                result.total_count = total_count;
                //设置缓存数据
                RedisDb._redisHelper_11().StringSet(key, list, RedisConfig._defaultExpiry);
                RedisDb._redisHelper_11().StringSet(key_count, total_count, RedisConfig._defaultExpiry);

            }
            if (list != null && list.Count > 0)
            {
                switch (PriorityType)
                {
                    case (int)EnumHelp.PriorityType.好评优先:
                        result.data = list.OrderByDescending(c => c.good_comment_rate).ToList();
                        break;
                    case (int)EnumHelp.PriorityType.低价优先:
                        result.data = list.OrderBy(c => c.average_pay).ToList();
                        break;
                    case (int)EnumHelp.PriorityType.高价优先:
                        result.data = list.OrderByDescending(c => c.average_pay).ToList();
                        break;
                    default:
                        result.data = list;
                        break;
                }
            }
            else
            {
                result.data = list;
            }
            return result;
        }

        /// <summary>
        /// 首页推荐，附近商家信息模糊查询
        /// </summary>
        /// <param name="Longitude">经度</param>
        /// <param name="Latitude">纬度</param>
        /// <returns></returns>
        public ResponseModel<List<BusinessDTO>> GetBusinessInfoBySearch(string Search_Name, int Module = 0, int BusinessGroupId = 0, int Page_Index = 1, int Page_Size = 20, double Longitude = 0, double Latitude = 0)
        {
            var result = new ResponseModel<List<Models.BusinessDTO>>();
            List<Models.BusinessDTO> list = new List<Models.BusinessDTO>();
            //查询缓存数据
            string key = "GetBusinessInfoBySearch_" + Search_Name + "_" + Module + "_" + BusinessGroupId + "_" + Page_Index + "_" + Longitude + "_" + Latitude;
            string key_count = "GetBusinessInfoCountBySearch_" + Search_Name + "_" + Module + "_" + BusinessGroupId + "_" + Page_Index + "_" + Longitude + "_" + Latitude;
            if (RedisDb._redisHelper_11().KeyExists(key) && Module == (int)EnumHelp.BusinessTypeEnum.食)
            {
                var data = RedisDb._redisHelper_11().StringGet<List<Models.BusinessDTO>>(key);
                result.data = data;
                result.total_count = RedisDb._redisHelper_11().StringGet<int>(key_count);
            }
            else
            {
                int total_count = 0;
                var getList = _businessInfoService.GetBusinessInfoBySearch(Search_Name, Page_Index, Page_Size, out total_count, Module, BusinessGroupId);
                foreach (var item in getList)
                {
                    var activityDiscount = item.ActivityDiscount;
                    var activityMinus = item.ActivityMinusList;
                    var dishesList = item.DishesList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).Take(3).ToList();
                    int comment_count = 0;
                    if (item.BusinessCommentList != null && item.BusinessCommentList.Count > 0)
                    {
                        var comm_list = item.BusinessCommentList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).ToList();
                        if (comm_list != null && comm_list.Count > 0)
                        {
                            comment_count = comm_list.Count;
                        }
                    }
                    list.Add(new Models.BusinessDTO()
                    {
                        average_pay = item.AveragePay.ToString(),
                        business_id = item.BusinessInfoId,
                        business_img_id = item.BaseImage == null ? 0 : item.BaseImage.BaseImageId,
                        business_name = item.Name,
                        business_img_path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path,
                        business_rank = item.Grade.ToString(),//Math.Round(item.Grade, 1),
                        mobile = item.Mobile,
                        address = item.Address,
                        business_hour = item.BusinessHour,
                        latitude = item.Latitude.ToString(),
                        longitude = item.Longitude.ToString(),
                        introduction = item.Introduction,
                        notic = item.Notic,
                        base_VR_image_list = BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.实景图).ToList()),
                        base_BN_image_list = BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.展示图).ToList()),
                        base_PR_image_list = BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.全景图).ToList()),
                        month_order_count = item.OrderCountPerMonth.ToString(),
                        manjian_info = MinusListToString(activityMinus),
                        activitylist = GetActivityStrList(item.ActivityMinusList, item.ActivityDiscount),
                        discount_info = DiscountToString(activityDiscount),
                        dishes_list = DishesListToDTO(dishesList),
                        distance = LongitudeToolService.GetDistance(Longitude, Latitude, Convert.ToDouble(item.Longitude.ToString() == "" ? 0 : item.Longitude), Convert.ToDouble(item.Latitude.ToString() == "" ? 0 : item.Latitude)),
                        distance_descript = item.Services,
                        service = item.Services,
                        min_consume = (item.ProductList == null || item.ProductList.Count <= 0) ? "0.00" : item.ProductList.Min(p => p.RealPrice).ToString(),
                        comment_count = comment_count,//(item.BusinessCommentList == null || item.BusinessCommentList.Count <= 0) ? 0 : item.BusinessCommentList.Count,
                        good_comment_rate = GetGoodCommentRate(item.BusinessCommentList),
                        BusinessLable = GetBusinessLableList(item.BusinessLableList),
                        District = item.District,
                        business_type_id = item.BusinessTypeId
                    });
                }

                result.data = list;
                result.total_count = total_count;
                //设置缓存数据
                RedisDb._redisHelper_11().StringSet(key, list, RedisConfig._defaultExpiry);
                RedisDb._redisHelper_11().StringSet(key_count, total_count, RedisConfig._defaultExpiry);
            }
            return result;
        }


        /// <summary>
        /// 首页推荐，站点附近商家
        /// </summary>
        /// <param name="Longitude">经度</param>
        /// <param name="Latitude">纬度</param>
        /// <returns></returns>
        public ResponseModel<List<BusinessDTO>> GetBusinessInfoByStation(int Station_Id, int Module = 0, int BusinessGroupId = 0, int Page_Index = 1, int Page_Size = 20, double Longitude = 0, double Latitude = 0)
        {
            var result = new ResponseModel<List<Models.BusinessDTO>>();
            List<Models.BusinessDTO> list = new List<Models.BusinessDTO>();
            //查询缓存数据
            string key = "GetBusinessInfoByStation_" + Station_Id + "_" + Module + "_" + BusinessGroupId + "_" + Page_Index + "_" + Longitude + "_" + Latitude;
            string key_count = "GetBusinessInfoCountByStation_" + Station_Id + "_" + Module + "_" + BusinessGroupId + "_" + Page_Index + "_" + Longitude + "_" + Latitude;
            if (RedisDb._redisHelper_11().KeyExists(key) && Module != (int)EnumHelp.BusinessTypeEnum.乐)
            {
                var data = RedisDb._redisHelper_11().StringGet<List<Models.BusinessDTO>>(key);
                result.data = data;
                result.total_count = RedisDb._redisHelper_11().StringGet<int>(key_count);
            }
            else
            {
                int total_count = 0;
                var getList = _businessInfoService.GetBusinessInfoByStation(Station_Id, Module, BusinessGroupId, Page_Index, Page_Size, out total_count);
                foreach (var item in getList)
                {
                    var activityDiscount = item.ActivityDiscount;
                    var activityMinus = item.ActivityMinusList;
                    var dishesList = item.DishesList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).Take(3).ToList();
                    int comment_count = 0;
                    if (item.BusinessCommentList != null && item.BusinessCommentList.Count > 0)
                    {
                        var comm_list = item.BusinessCommentList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).ToList();
                        if (comm_list != null && comm_list.Count > 0)
                        {
                            comment_count = comm_list.Count;
                        }
                    }
                    list.Add(new Models.BusinessDTO()
                    {
                        average_pay = item.AveragePay.ToString(),
                        business_id = item.BusinessInfoId,
                        business_img_id = item.BaseImage == null ? 0 : item.BaseImage.BaseImageId,
                        business_name = item.Name,
                        business_img_path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path,
                        business_rank = item.Grade.ToString(),//.ToString("#0.0"),//Math.Round(item.Grade, 1),
                        mobile = item.Mobile,
                        address = item.Address,
                        business_hour = item.BusinessHour,
                        latitude = item.Latitude.ToString(),
                        longitude = item.Longitude.ToString(),
                        introduction = item.Introduction,
                        notic = item.Notic,
                        base_VR_image_list = BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.实景图).ToList()),
                        base_BN_image_list = BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.展示图).ToList()),
                        base_PR_image_list = BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.全景图).ToList()),
                        month_order_count = item.OrderCountPerMonth.ToString(),
                        manjian_info = MinusListToString(activityMinus),
                        discount_info = DiscountToString(activityDiscount),
                        activitylist = GetActivityStrList(item.ActivityMinusList, item.ActivityDiscount),
                        dishes_list = DishesListToDTO(dishesList),
                        distance = LongitudeToolService.GetDistance(Longitude, Latitude, Convert.ToDouble(item.Longitude.ToString() == "" ? 0 : item.Longitude), Convert.ToDouble(item.Latitude.ToString() == "" ? 0 : item.Latitude)),
                        distance_descript = item.Services,
                        service = item.Services,
                        min_consume = (item.ProductList == null || item.ProductList.Count <= 0) ? "0.00" : item.ProductList.Min(p => p.RealPrice).ToString(),
                        comment_count = comment_count,//(item.BusinessCommentList == null || item.BusinessCommentList.Count <= 0) ? 0 : item.BusinessCommentList.Count,
                        good_comment_rate = GetGoodCommentRate(item.BusinessCommentList),
                        BusinessLable = GetBusinessLableList(item.BusinessLableList),
                        District = item.District,
                        business_type_id = item.BusinessTypeId
                    });
                }

                result.data = list;
                result.total_count = total_count;
                //设置缓存数据
                RedisDb._redisHelper_11().StringSet(key, list, RedisConfig._defaultExpiry);
                RedisDb._redisHelper_11().StringSet(key_count, total_count, RedisConfig._defaultExpiry);
            }
            return result;
        }

        /// <summary>
        /// 首页，获取地铁线路图
        /// </summary> 
        /// <returns></returns>
        public ResponseModel<List<LineDTO>> GetStationData()
        {
            var result = new ResponseModel<List<Models.LineDTO>>();
            List<Models.LineDTO> list = new List<Models.LineDTO>();
            //查询缓存数据
            string key = "GetStationData";
            if (RedisDb._redisHelper_11().KeyExists(key))
            {
                var data = RedisDb._redisHelper_11().StringGet<List<Models.LineDTO>>(key);
                result.data = data;
                result.total_count = data.Count;
            }
            else
            {
                var getList = _baseLineService.GetAll();
                foreach (var item in getList)
                {
                    list.Add(new Models.LineDTO()
                    {
                        line_id = item.BaseLineId,
                        line_name = item.LineName,
                        line_image = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path,
                        station_list = (from p in item.BaseStationList.Where(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效) select new StationDTO { station_id = p.BaseStationId, station_name = p.Name }).ToList(),
                    });
                }

                result.data = list;
                result.total_count = list.Count;
                //设置缓存数据
                RedisDb._redisHelper_11().StringSet(key, list, RedisConfig._defaultExpiry);
            }
            return result;
        }


        /// <summary>
        /// 根据商家id，查询商家信息数据
        /// </summary>
        /// <param name="Bussiness_Id">商家Id</param>
        /// <param name="Longitude">经度</param>
        /// <param name="Latitude">纬度</param>
        /// <returns></returns>
        public ResponseModel<BusinessDTO> GetBusinessInfoById(int Bussiness_Id, double Longitude = 0, double Latitude = 0)
        {
            var result = new ResponseModel<BusinessDTO>();
            BusinessDTO list = new BusinessDTO();
            if (Bussiness_Id <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "参数不合法";
                result.total_count = 0;
                return result;
            }
            var item = _businessInfoService.GetById(Bussiness_Id);
            if (item == null)
            {
                throw new Exception("商家不存在");
            }
            //查询缓存数据
            string key = "GetBusinessInfoById_" + Bussiness_Id;
            if (false)//RedisDb._redisHelper_11().KeyExists(key)
            {
                var data = RedisDb._redisHelper_11().StringGet<BusinessDTO>(key);

                int comment_count = 0;
                if (item.BusinessCommentList != null && item.BusinessCommentList.Count > 0)
                {
                    var comm_list = item.BusinessCommentList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).ToList();
                    if (comm_list != null && comm_list.Count > 0)
                    {
                        comment_count = comm_list.Count;
                    }
                }
                data.comment_count = comment_count;
                if (item.BusinessTypeId == (int)EnumHelp.BusinessTypeEnum.乐)
                {
                    data.product_list = ProductListToDTO(item.ProductList == null ? null : item.ProductList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).ToList());
                }
                if (item.BusinessTypeId == (int)EnumHelp.BusinessTypeEnum.酒店)
                {
                    var roomList = item.BusinessRoomList == null ? null : item.BusinessRoomList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效 && c.Remain > 0).ToList();
                    data.Room_list = RoomListToRoomDTO(item.BusinessRoomList);
                }
                if (item.BusinessTypeId == (int)EnumHelp.BusinessTypeEnum.景点)
                {
                    data.Ticket_list = TicketListToTicketDTO(item.BusinessTicketList == null ? null : item.BusinessTicketList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).ToList());
                }
                if (item.BusinessTypeId == (int)EnumHelp.BusinessTypeEnum.衣)
                {
                    data.goods_list = GoodsListToGoodsDTO(item.BusinessGoodsList == null ? null : item.BusinessGoodsList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).ToList());
                }
                var orderList = _orderService.GetOrderListByBueinessId(Bussiness_Id);
                data.order_count = (orderList == null || orderList.Count == 0) ? 0 : orderList.Count;
                result.data = data;
            }
            else
            {
                //var item = _businessInfoService.GetById(Bussiness_Id);
                var activityDiscount = item.ActivityDiscount == null ? null : item.ActivityDiscount;
                var activityMinus = item.ActivityMinusList == null ? null : item.ActivityMinusList;


                var evaluation = item.BusinessEvaluation;
                var orderList = _orderService.GetOrderListByBueinessId(Bussiness_Id);
                int comment_count = 0;
                if (item.BusinessCommentList != null && item.BusinessCommentList.Count > 0)
                {
                    var comm_list = item.BusinessCommentList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).ToList();
                    if (comm_list != null && comm_list.Count > 0)
                    {
                        comment_count = comm_list.Count;
                    }
                }
                var data = new Models.BusinessDTO();
                //{
                data.average_pay = item.AveragePay.ToString();
                data.business_id = item.BusinessInfoId;
                data.business_img_id = item.BaseImage == null ? 0 : item.BaseImage.BaseImageId;
                data.business_name = item.Name;
                data.business_img_path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path;
                data.business_rank = item.Grade.ToString();//a.ToString()Math.Round(item.Grade, 1),
                data.month_order_count = item.OrderCountPerMonth.ToString();
                data.manjian_info = MinusListToString(activityMinus);
                data.discount_info = DiscountToString(activityDiscount);
                data.activitylist = GetActivityStrList(activityMinus, activityDiscount);
                data.notic = item.Notic;
                data.mobile = item.Mobile;
                data.address = item.Address;
                data.business_hour = item.BusinessHour;
                data.introduction = item.Introduction;
                data.latitude = item.Latitude.ToString();
                data.longitude = item.Longitude.ToString();
                data.base_VR_image_list = item.BusinessImageList == null ? new List<BaseImageDTO>() : BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.实景图).ToList());
                data.base_BN_image_list = item.BusinessImageList == null ? new List<BaseImageDTO>() : BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.展示图).ToList());
                data.base_PR_image_list = item.BusinessImageList == null ? new List<BaseImageDTO>() : BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.全景图).ToList());
                //dishes_list = DishesListToDTO(dishesList), //数据从dishes_cate_list中取
                data.distance = LongitudeToolService.GetDistance(Longitude, Latitude, Convert.ToDouble(item.Longitude.ToString() == "" ? 0 : item.Longitude), Convert.ToDouble(item.Latitude.ToString() == "" ? 0 : item.Latitude));
                data.distance_descript = item.Distance;
                data.service = item.Services;
                data.comment_count = comment_count;// (item.BusinessCommentList == null || item.BusinessCommentList.Count <= 0) ? 0 : item.BusinessCommentList.Count,
                data.good_comment_rate = GetGoodCommentRate(item.BusinessCommentList);
                data.environment_grade = evaluation == null ? "5.0" : evaluation.Environment.ToString();
                data.service_grade = evaluation == null ? "5.0" : evaluation.Service.ToString();
                data.facilities_grade = evaluation == null ? "5.0" : evaluation.Facilities.ToString();
                data.order_count = (orderList == null || orderList.Count == 0) ? 0 : orderList.Count;
                data.District = item.District;
                data.business_type_id = item.BusinessTypeId;
                data.open_date = item.OpenDate;
                data.refresh_date = item.RefreshDate;
                data.total_floors = item.TotalFloors;
                data.total_rooms = item.TotalRooms;
                // };
                //如果是乐模块，需要显示附近商家数据
                switch (item.BusinessTypeId)
                {
                    case (int)BusinessTypeEnum.乐:
                        var productList = item.ProductList == null ? null : item.ProductList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).ToList();
                        data.min_consume = (productList == null || productList.Count <= 0) ? "0.00" : productList.Min(p => p.RealPrice).ToString();
                        data.product_list = ProductListToDTO(productList);
                        int totalCount = 0;
                        var nearbyList = _businessInfoService.GetBusinessInfoByStation(item.BaseStationId, item.BusinessTypeId, 0, 1, 4, out totalCount);
                        var nearBy = (from m in nearbyList
                                      where m.BusinessInfoId != item.BusinessInfoId
                                      select new BusinessDTO
                                      {
                                          business_id = m.BusinessInfoId,
                                          business_name = m.Name,
                                          business_img_path = m.BaseImage == null ? "" : m.BaseImage.Source + m.BaseImage.Path,
                                          business_rank = m.Grade.ToString(),
                                          average_pay = m.AveragePay.ToString(),
                                          manjian_info = MinusListToString(m.ActivityMinusList),
                                          discount_info = DiscountToString(m.ActivityDiscount),
                                          distance_descript = m.Services,
                                          service = m.Services,
                                          min_consume = (m.ProductList == null || m.ProductList.Count <= 0) ? "0.00" : m.ProductList.Min(p => p.RealPrice).ToString(),
                                          comment_count = (m.BusinessCommentList == null || m.BusinessCommentList.Count <= 0) ? 0 : m.BusinessCommentList.Count,
                                          good_comment_rate = GetGoodCommentRate(m.BusinessCommentList),
                                      }).ToList();
                        data.Nearby = (nearBy == null || nearBy.Count <= 0) ? new List<BusinessDTO>() : nearBy;
                        break;
                    case (int)BusinessTypeEnum.酒店:
                        var roomList = item.BusinessRoomList == null ? null : item.BusinessRoomList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效 && c.Remain > 0).ToList();
                        data.Room_list = RoomListToRoomDTO(roomList);
                        data.min_consume = (roomList == null || roomList.Count <= 0) ? "0.00" : roomList.Min(p => p.RealPrice).ToString();
                        data.BusinessLable = GetBusinessLableList(item.BusinessLableList);
                        int HoteltotalCount = 0;
                        var HotelnearbyList = _businessInfoService.GetBusinessInfoByStation(item.BaseStationId, item.BusinessTypeId, 0, 1, 4, out HoteltotalCount);
                        var HotelnearBy = (from m in HotelnearbyList
                                           where m.BusinessInfoId != item.BusinessInfoId
                                           select new BusinessDTO
                                           {
                                               business_id = m.BusinessInfoId,
                                               business_name = m.Name,
                                               business_img_path = m.BaseImage == null ? "" : m.BaseImage.Source + m.BaseImage.Path,
                                               business_rank = m.Grade.ToString(),
                                               average_pay = m.AveragePay.ToString(),
                                               manjian_info = MinusListToString(m.ActivityMinusList),
                                               discount_info = DiscountToString(m.ActivityDiscount),
                                               distance_descript = m.Distance,
                                               service = m.Services,
                                               min_consume = (m.ProductList == null || m.ProductList.Count <= 0) ? "0.00" : m.ProductList.Min(p => p.RealPrice).ToString(),
                                               comment_count = (m.BusinessCommentList == null || m.BusinessCommentList.Count <= 0) ? 0 : m.BusinessCommentList.Count,
                                               good_comment_rate = GetGoodCommentRate(m.BusinessCommentList),
                                               BusinessLable = GetBusinessLableList(m.BusinessLableList)
                                           }).ToList();
                        data.Nearby = (HotelnearBy == null || HotelnearBy.Count <= 0) ? new List<BusinessDTO>() : HotelnearBy;
                        //data.product_list
                        break;
                    case (int)BusinessTypeEnum.食:
                        var dishesList = item.DishesList == null ? null : item.DishesList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).ToList();
                        data.dishes_cate_list = DishesListToCateDTO(dishesList);
                        break;
                    case (int)BusinessTypeEnum.景点:
                        data.Ticket_list = TicketListToTicketDTO(item.BusinessTicketList == null ? null : item.BusinessTicketList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).ToList());
                        data.BusinessLable = GetBusinessLableList(item.BusinessLableList);
                        data.JourneyArticle_list = JourneyArticleToDTO(item.BusinessJourneyArticleList == null ? null : item.BusinessJourneyArticleList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).OrderByDescending(c => c.CreateTime).Take(3).ToList());
                        break;
                    case (int)BusinessTypeEnum.衣:
                        var baseDic = _baseDicService.GetBaseDiscListByType("种类");
                        data.goods_cate_list = EntityToDTO.BaseDicToDTO(baseDic);
                        data.goods_list = GoodsListToGoodsDTO(item.BusinessGoodsList == null ? null : item.BusinessGoodsList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).ToList());
                        break;
                    default:
                        break;
                }
                result.data = data;
                //设置缓存数据
                RedisDb._redisHelper_11().StringSet(key, data, RedisConfig._defaultExpiry);
            }
            return result;
        }

        #region 衣

        /// <summary>
        /// 主页 根据定位获取各个类别下的商家（含模糊搜索）
        /// </summary>
        /// <param name="Search_Name"></param>
        /// <param name="Longitude"></param>
        /// <param name="Latitude"></param>
        /// <param name="Module"></param>
        /// <param name="Page_Index"></param>
        /// <param name="Page_Size"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<List<GoodsBusinessDTO>> GetGoodsIndexBusinessInfo(string Search_Name = "", double Longitude = 0, double Latitude = 0, int Module = 0, int Page_Index = 1, int Page_Size = 20)
        {
            var result = new ResponseModel<List<GoodsBusinessDTO>>();
            result.message = "";
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            var data = new List<GoodsBusinessDTO>();
            int total_count;
            List<BusinessInfo> businesdata;
            businesdata = _businessInfoService.GetGoodsBusinessInfoByModule(Search_Name, Module, out total_count);
            if (businesdata == null || businesdata.Count <= 0)//没有数据
            {
                result.data = data;
                return result;
            }
            var groupbusiness = businesdata.OrderBy(c => c.BusinessGroup.Sort).GroupBy(c => c.BusinessGroupId).ToList();
            foreach (var item in groupbusiness)
            {
                if (item != null && item.ToList().Count > 0)
                {
                    var bi = new GoodsBusinessDTO();
                    var bilist = new List<BusinessDTO>();
                    bi.business_group_id = item.ToList()[0].BusinessGroupId;
                    bi.name = item.ToList()[0].BusinessGroup.Name;
                    foreach (var b in item)
                    {
                        var business = new BusinessDTO();
                        business.business_img_path = b.BaseImage == null ? "" : b.BaseImage.Source + b.BaseImage.Path;
                        business.business_id = b.BusinessInfoId;
                        business.distance = LongitudeToolService.GetDistance(Longitude, Latitude, Convert.ToDouble(b.Longitude.ToString() == "" ? 0 : b.Longitude), Convert.ToDouble(b.Latitude.ToString() == "" ? 0 : b.Latitude));
                        business.business_name = b.Name;
                        business.BusinessLable = GetBusinessLableList(b.BusinessLableList);
                        business.District = b.District;
                        business.address = b.Address;
                        business.base_VR_image_list = BusinessImageToDTO(b.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.实景图).ToList());
                        business.base_BN_image_list = BusinessImageToDTO(b.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.展示图).ToList());
                        business.base_PR_image_list = BusinessImageToDTO(b.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.全景图).ToList());
                        bilist.Add(business);
                    }
                    bi.businesslist = bilist == null ? bilist : bilist.OrderBy(c => c.distance).Skip(Page_Size * (Page_Index - 1)).Take(Page_Size).ToList();
                    data.Add(bi);
                }
            }
            result.data = data;
            result.total_count = total_count;
            return result;
        }

        /// <summary>
        /// 优选名店
        /// </summary>
        /// <param name="Search_Name"></param>
        /// <param name="Longitude"></param>
        /// <param name="Latitude"></param>
        /// <param name="Module"></param>
        /// <param name="Page_Index"></param>
        /// <param name="Page_Size"></param>
        /// <returns></returns>
        public ResponseModel<List<BusinessDTO>> GetIndexBusinessInfoIsTop(string Search_Name = "", double Longitude = 0, double Latitude = 0, int Module = 0, int Page_Index = 1, int Page_Size = 20)
        {
            var result = new ResponseModel<List<BusinessDTO>>();
            result.message = "";
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            var data = new List<BusinessDTO>();
            int total_count = 0;

            var businessList = _businessInfoService.GetIndexBusinessInfoIsTop(Search_Name, Module, Page_Index, Page_Size, out total_count);
            if (businessList != null && businessList.Count > 0)
            {
                foreach (var b in businessList)
                {
                    var business = new BusinessDTO();
                    business.business_img_path = b.BaseImage == null ? "" : b.BaseImage.Source + b.BaseImage.Path;
                    business.business_id = b.BusinessInfoId;
                    business.distance = LongitudeToolService.GetDistance(Longitude, Latitude, Convert.ToDouble(b.Longitude.ToString() == "" ? 0 : b.Longitude), Convert.ToDouble(b.Latitude.ToString() == "" ? 0 : b.Latitude));
                    business.business_name = b.Name;
                    business.BusinessLable = GetBusinessLableList(b.BusinessLableList);
                    business.District = b.District;
                    business.address = b.Address;
                    business.base_VR_image_list = BusinessImageToDTO(b.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.实景图).ToList());
                    business.base_BN_image_list = BusinessImageToDTO(b.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.展示图).ToList());
                    business.base_PR_image_list = BusinessImageToDTO(b.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.全景图).ToList());
                    data.Add(business);
                }
            }
            result.data = data;
            result.total_count = total_count;
            return result;
        }

        /// <summary>
        /// 根据(类别、区域）+定位/获取各个类别下的商家
        /// </summary>
        /// <param name="BusinessGroup_Id"></param>
        /// <param name="Longitude"></param>
        /// <param name="Latitude"></param>
        /// <param name="Module"></param>
        /// <param name="Page_Index"></param>
        /// <param name="Page_Size"></param>
        /// <returns></returns>
        public ResponseModel<List<BusinessDTO>> GetGoodsBusinessInfoByGroupIdWithLocation(int BusinessGroup_Id = 0, string District = "", double Longitude = 0, double Latitude = 0, int Module = 0, int Page_Index = 1, int Page_Size = 20)
        {
            var result = new ResponseModel<List<BusinessDTO>>();
            result.message = "";
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            var data = new List<BusinessDTO>();
            int total_count = 0;
            if (BusinessGroup_Id <= 0 || string.IsNullOrWhiteSpace(District))
            {
                throw new Exception("请根据商家类别分组ID获取区域查询");
            }
            var businessList = _businessInfoService.GetGoodsBusinessInfoByGroupIdWithLocation(BusinessGroup_Id, District, Longitude, Latitude, Module, Page_Index, Page_Size, out total_count);
            var getList = _businessInfoService.GetListByIds(businessList.Select(p => p.BusinessInfoId).ToList());
            if (getList != null && getList.Count > 0)
            {
                foreach (var b in getList)
                {
                    var business = new BusinessDTO();
                    business.business_img_path = b.BaseImage == null ? "" : b.BaseImage.Source + b.BaseImage.Path;
                    business.business_id = b.BusinessInfoId;
                    business.distance = LongitudeToolService.GetDistance(Longitude, Latitude, Convert.ToDouble(b.Longitude.ToString() == "" ? 0 : b.Longitude), Convert.ToDouble(b.Latitude.ToString() == "" ? 0 : b.Latitude));
                    business.business_name = b.Name;
                    business.BusinessLable = GetBusinessLableList(b.BusinessLableList);
                    business.District = b.District;
                    business.address = b.Address;
                    business.base_VR_image_list = BusinessImageToDTO(b.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.实景图).ToList());
                    business.base_BN_image_list = BusinessImageToDTO(b.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.展示图).ToList());
                    business.base_PR_image_list = BusinessImageToDTO(b.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.全景图).ToList());
                    data.Add(business);
                }
            }
            result.data = data == null ? data : data.OrderBy(c => c.distance).ToList();
            result.total_count = total_count;
            return result;
        }



        #endregion

        #region 酒店
        [HttpPost]
        public ResponseModel<List<BusinessDTO>> GetBusinessInfoByManCond(SelectBusinessInfoByManyCondDTO selectBusinessInfoByManyCondDTO)
        {
            var result = new ResponseModel<List<BusinessDTO>>();
            result.message = "";
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            List<BusinessDTO> list = new List<BusinessDTO>();

            int total_count = 0;
            List<BusinessInfo> getList = new List<BusinessInfo>();
            if (selectBusinessInfoByManyCondDTO.Module == (int)EnumHelp.BusinessTypeEnum.酒店)
            {
                if (selectBusinessInfoByManyCondDTO.Longitude == 0 && selectBusinessInfoByManyCondDTO.Latitude == 0)
                {
                    getList = _businessInfoService.GetListByManCond(selectBusinessInfoByManyCondDTO.Grade, selectBusinessInfoByManyCondDTO.HotelCategoryId, selectBusinessInfoByManyCondDTO.Price, selectBusinessInfoByManyCondDTO.Page_Index, selectBusinessInfoByManyCondDTO.Page_Size, out total_count).ToList();
                }
                else
                {
                    //获取定位+多条件筛选酒店列表
                    var searchResult = _businessInfoService.GetListByLocationAndCond(selectBusinessInfoByManyCondDTO.Grade, selectBusinessInfoByManyCondDTO.HotelCategoryId, selectBusinessInfoByManyCondDTO.Price, selectBusinessInfoByManyCondDTO.Longitude, selectBusinessInfoByManyCondDTO.Latitude, selectBusinessInfoByManyCondDTO.Page_Index, selectBusinessInfoByManyCondDTO.Page_Size, out total_count).ToList();
                    getList = _businessInfoService.GetListByIds(searchResult.Select(p => p.BusinessInfoId).ToList());
                }
            }
            else
            {
                result.message = "";
                result.data = list;
                return result;
            }
            //绑定数据
            if (getList != null && getList.Count > 0)
            {
                foreach (var item in getList)
                {
                    int comment_count = 0;
                    if (item.BusinessCommentList != null && item.BusinessCommentList.Count > 0)
                    {
                        var comm_list = item.BusinessCommentList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).ToList();
                        if (comm_list != null && comm_list.Count > 0)
                        {
                            comment_count = comm_list.Count;
                        }
                    }
                    list.Add(new Models.BusinessDTO()
                    {
                        average_pay = item.AveragePay.ToString(),
                        business_id = item.BusinessInfoId,
                        business_img_id = item.BaseImage == null ? 0 : item.BaseImage.BaseImageId,
                        business_name = item.Name,
                        business_img_path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path,
                        business_rank = item.Grade.ToString(),
                        mobile = item.Mobile,
                        address = item.Address,
                        business_hour = item.BusinessHour,
                        latitude = item.Latitude.ToString(),
                        longitude = item.Longitude.ToString(),
                        introduction = item.Introduction,
                        notic = item.Notic,
                        base_VR_image_list = item.BusinessImageList == null ? new List<BaseImageDTO>() : BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.实景图).ToList()),
                        base_BN_image_list = item.BusinessImageList == null ? new List<BaseImageDTO>() : BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.展示图).ToList()),
                        base_PR_image_list = item.BusinessImageList == null ? new List<BaseImageDTO>() : BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.全景图).ToList()),
                        //dishes_cate_list = item.DishesList == null ? new List<DishesCategoryDTO>() : DishesListToCateDTO(item.DishesList),
                        month_order_count = item.OrderCountPerMonth.ToString(),
                        manjian_info = MinusListToString(item.ActivityMinusList),
                        discount_info = DiscountToString(item.ActivityDiscount),
                        activitylist = GetActivityStrList(item.ActivityMinusList, item.ActivityDiscount),
                        //dishes_list = DishesListToDTO(item.DishesList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效).Take(3).ToList()),
                        distance = LongitudeToolService.GetDistance(selectBusinessInfoByManyCondDTO.Longitude, selectBusinessInfoByManyCondDTO.Latitude, Convert.ToDouble(item.Longitude.ToString() == "" ? 0 : item.Longitude), Convert.ToDouble(item.Latitude.ToString() == "" ? 0 : item.Latitude)),
                        distance_descript = item.Services,
                        service = item.Services,
                        min_consume = (item.BusinessRoomList == null || item.BusinessRoomList.Count <= 0) ? "0.00" : item.BusinessRoomList.Min(p => p.RealPrice).ToString(),
                        comment_count = comment_count,//(item.BusinessCommentList == null || item.BusinessCommentList.Count <= 0) ? 0 : .ToList().Count,
                        good_comment_rate = GetGoodCommentRate(item.BusinessCommentList),
                        BusinessLable = GetBusinessLableList(item.BusinessLableList)
                    });
                }
            }
            list = list.OrderBy(p => p.distance).ToList();
            result.data = list;
            result.total_count = total_count;
            return result;
        }
        #endregion

        /// <summary>
        /// 获取可更换房间接口
        /// </summary>
        /// <param name="Bussiness_Id"></param>
        /// <param name="Room_Id"></param>
        /// <returns></returns>
        public ResponseModel<BusinessDTO> GetBusinessUpdRoomById(int Bussiness_Id, int Room_Id)
        {
            var result = new ResponseModel<BusinessDTO>();
            BusinessDTO list = new BusinessDTO();
            if (Bussiness_Id <= 0 || Room_Id <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "参数不合法";
                result.total_count = 0;
                return result;
            }
            var item = _businessInfoService.GetById(Bussiness_Id);
            var room = _roomService.GetById(Room_Id);
            var data = new Models.BusinessDTO();
            if (item != null && room != null)
            {
                var roomList = item.BusinessRoomList == null ? null : item.BusinessRoomList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.EnabledEnum.有效 && c.RoomId != Room_Id && c.Remain > 0).OrderBy(c => c.RealPrice).ToList();
                if (roomList == null)
                {
                    data.is_update = (int)EnumHelp.IsUpdRoomEnum.否;
                    result.data = data;
                    return result;
                }
                data.is_update = (int)EnumHelp.IsUpdRoomEnum.是;
                data.Room_list = GetMinusRoomDTOList(roomList, room.RealPrice);
                data.business_id = item.BusinessInfoId;
                data.base_BN_image_list = item.BusinessImageList == null ? new List<BaseImageDTO>() : BusinessImageToDTO(item.BusinessImageList.Where(c => c.Type == (int)EnumHelp.BusinessImageTypeEnum.展示图).ToList());
                data.business_rank = item.Grade.ToString();
                data.distance_descript = item.Distance;
                data.business_name = item.Name;
                data.business_type_id = item.BusinessTypeId;
            }
            result.data = data;
            return result;
        }

        /// <summary>
        /// 获取酒店可更换房间列表
        /// </summary>
        /// <param name="businessRoomList"></param>
        /// <param name="realPrice"></param>
        /// <returns></returns>
        private List<RoomDTO> GetMinusRoomDTOList(List<Room> businessRoomList, decimal realPrice)
        {
            List<RoomDTO> result = new List<RoomDTO>();
            if (businessRoomList != null && businessRoomList.Count > 0)
            {
                foreach (var item in businessRoomList)
                {
                    result.Add(new RoomDTO()
                    {
                        room_id = item.RoomId,
                        name = item.Name,
                        area = item.Area,
                        bed = item.Bed,
                        breakfast = item.Breakfast,
                        window = item.Window,
                        orignPrice = item.OrignPrice.ToString(),
                        realPrice = item.RealPrice <= realPrice ? "0.00" : (item.RealPrice - realPrice).ToString()
                    });
                }
            }
            return result;
        }

        private List<JourneyArticleDTO> JourneyArticleToDTO(List<JourneyArticle> list)
        {
            List<JourneyArticleDTO> result = new List<JourneyArticleDTO>();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    result.Add(new JourneyArticleDTO()
                    {
                        user_id = item.UserId,
                        journey_article_id = item.JourneyArticleId,
                        name = item.Name,
                        reads = item.Reads,
                        likes = item.Likes,
                        user_name = item.User == null ? "" : item.User.NickName,
                        create_time = item.CreateTime.ToString("yyyy-MM-dd"),
                        path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path
                    });
                }
            }
            return result;
        }

        private List<TicketDTO> TicketListToTicketDTO(List<Ticket> list)
        {
            List<TicketDTO> result = new List<TicketDTO>();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    result.Add(new TicketDTO()
                    {
                        ticket_id = item.TicketId,
                        name = item.Name,
                        notice = item.Notice,
                        orign_price = item.OrignPrice.ToString(),
                        real_price = item.RealPrice.ToString(),
                        remark = item.Remark,
                        rules = item.Rules,
                        special = item.Special
                    });
                }
            }
            return result;
        }

        private List<RoomDTO> RoomListToRoomDTO(List<Room> businessRoomList)
        {
            List<RoomDTO> list = new List<RoomDTO>();
            if (businessRoomList != null && businessRoomList.Count > 0)
            {
                foreach (var item in businessRoomList)
                {
                    list.Add(
                         new RoomDTO()
                         {
                             room_id = item.RoomId,
                             airConditioner = item.AirConditioner,
                             area = item.Area,
                             bathroom = item.Bathroom,
                             bed = item.Bed,
                             breakfast = item.Breakfast,
                             floor = item.Floor,
                             internet = item.Internet,
                             name = item.Name,
                             notice = item.Notice,
                             orignPrice = item.OrignPrice.ToString(),
                             realPrice = item.RealPrice.ToString(),
                             remain = item.Remain.ToString(),
                             rules = item.Rules,
                             window = item.Window,
                             bed_type = item.BedType,
                             room_image_list = RoomImageToDTO(item.RoomImageList)
                         }
                        );
                }
            }
            return list;
        }

        /// <summary>
        /// 衣品对象转衣品实体
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<GoodsDTO> GoodsListToGoodsDTO(List<Goods> businessGoodsList)
        {
            List<GoodsDTO> list = new List<GoodsDTO>();
            if (businessGoodsList != null && businessGoodsList.Count > 0)
            {
                foreach (var item in businessGoodsList)
                {
                    list.Add(
                         new GoodsDTO()
                         {
                             goods_id = item.GoodsId,
                             descript = item.Descript,
                             name = item.Name,
                             orign_price = item.OrignPrice.ToString(),
                             real_price = item.RealPrice.ToString(),
                             path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path,
                             base_image_id = item.BaseImageId,
                             image_list = GoodsImageToDTO(item.GoodsImageList)
                         }
                        );
                }
            }
            return list;
        }

        private List<GoodsImageDTO> GoodsImageToDTO(List<Domain.Model.GoodsImage> goodsImageList)
        {
            List<GoodsImageDTO> result = null;
            if (goodsImageList != null && goodsImageList.Count > 0)
            {
                result = new List<GoodsImageDTO>();
                foreach (var item in goodsImageList)
                {
                    result.Add(new GoodsImageDTO()
                    {
                        base_image_id = item.BaseImageId,
                        path = item.BaseImage == null ? "" : (item.BaseImage.Source + item.BaseImage.Path),
                        type = item.Type
                    });
                }
            }
            return result;
        }

        private List<BaseImageDTO> RoomImageToDTO(List<RoomImage> list)
        {
            List<BaseImageDTO> result = null;
            if (list != null && list.Count > 0)
            {
                result = new List<BaseImageDTO>();
                foreach (var item in list)
                {
                    result.Add(new BaseImageDTO()
                    {
                        img_id = item.BaseImageId,
                        img_path = item.BaseImage == null ? "" : (item.BaseImage.Source + item.BaseImage.Path)
                    });
                }
            }
            return result;
        }
        /// <summary>
        /// 商家标签
        /// </summary>
        /// <param name="businessLableList"></param>
        /// <returns></returns>
        private List<string> GetBusinessLableList(List<BusinessLable> businessLableList)
        {
            List<string> list = new List<string>();
            if (businessLableList == null || businessLableList.Count <= 0)
            {
                return list;
            }
            else
            {
                list = businessLableList.Select(c => c.Name).ToList();
                return list;
            }
        }
        private List<ProductDTO> ProductListToDTO(List<Product> list)
        {
            List<ProductDTO> result = null;
            if (list != null && list.Count > 0)
            {
                result = new List<ProductDTO>();
                foreach (var item in list)
                {
                    result.Add(new ProductDTO()
                    {
                        descript = item.Descript,
                        end_date = item.EndDate.ToString(),
                        notice = item.Notice,
                        orign_price = item.OrignPrice.ToString(),
                        product_id = item.ProductId,
                        product_name = item.Name,
                        real_price = item.RealPrice.ToString(),
                        start_date = item.StartDate.ToString(),
                        remark = item.Remark,
                        rule = item.Rules,
                        content = item.Content,
                        product_image_list = ProductImageToDTO(item.ProductImageList),
                        lable = ProductLableListToStr(item.ProductLableList)
                    });
                }
            }
            return result;
        }

        private string ProductLableListToStr(List<ProductLable> productLableList)
        {
            StringBuilder sb = new StringBuilder(200);
            int i = 0;
            if (productLableList != null && productLableList.Count > 0)
            {
                foreach (var item in productLableList)
                {
                    if (i == 0)
                    {
                        sb.Append(item.Name);
                    }
                    else
                    {
                        sb.Append(",");
                        sb.Append(item.Name);
                    }
                    i++;
                }

            }
            return sb.ToString();
        }

        /// <summary>
        /// 返回菜品集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<DishesDTO> DishesListToDTO(List<Dishes> list)
        {
            List<DishesDTO> result = null;
            if (list != null)
            {
                result = new List<DishesDTO>();
                foreach (var item in list)
                {
                    result.Add(new DishesDTO()
                    {
                        dishes_id = item.DishesId,
                        dishes_img_id = item.BaseImage == null ? "0" : item.BaseImage.BaseImageId.ToString(),//改
                        dishes_img_path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path,//改
                        descript = StringHelper.CleanHtml(item.Descript),
                        dishes_name = item.Name,
                        dishes_orign_price = item.OrignPrice,
                        dishes_real_price = item.RealPrice,
                        month_sale_count = item.SellCountPerMonth,
                    });
                }
            }
            return result;
        }
        /// <summary>
        /// 菜品详情集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<DishesDTO> DishesDetailListToDTO(List<Dishes> list)
        {
            List<DishesDTO> result = null;
            if (list != null)
            {
                var relateLabelAll = _dishesLableService.GetAll();
                var relateSpecAll = _dishesSpecService.GetAll();

                result = new List<DishesDTO>();
                foreach (var item in list)
                {
                    result.Add(new DishesDTO()
                    {
                        dishes_id = item.DishesId,
                        dishes_img_id = item.BaseImageId.ToString(),//改
                        dishes_img_path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path,//改
                        dishes_name = item.Name,
                        dishes_orign_price = item.OrignPrice,
                        dishes_real_price = item.RealPrice,
                        descript = StringHelper.CleanHtml(item.Descript),
                        month_sale_count = item.SellCountPerMonth,
                        //todo:商品标签
                        tags = relateLabelAll.Where(p => p.DishesId == item.DishesId).Select(p => p.Name).ToList(),
                        //todo:规格参数
                        specs = DishesSpecDetailListToDTO(relateSpecAll.Where(p => p.DishesId == item.DishesId).ToList()),
                    });
                }

            }
            return result;
        }

        /// <summary>
        /// 返回菜品规格集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<DishesSpecDTO> DishesSpecDetailListToDTO(List<DishesSpec> list)
        {
            List<DishesSpecDTO> result = null;

            if (list != null)
            {
                result = new List<DishesSpecDTO>();
                foreach (var item in list)
                {
                    var spec = new DishesSpecDTO();

                    if (item != null)
                    {
                        spec.spec_id = item.DishesSpecId;
                        if (item.DishesSpecDetailList != null)
                        {
                            spec.spec_name = item.Name;
                            spec.detail_list.AddRange(from p in item.DishesSpecDetailList
                                                      select new DishesSpecDetailDTO
                                                      {
                                                          descript = p.Descript,
                                                          dishes_orign_price = p.OrignPrice,
                                                          dishes_real_price = p.RealPrice,
                                                          specdetail_id = p.DishesSpecDetailId,
                                                          spec_id = p.DishesSpecId
                                                      });
                        }
                    }
                    result.Add(spec);
                }
            }
            return result;
        }
        /// <summary>
        /// 返回菜品分类集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<DishesCategoryDTO> DishesListToCateDTO(List<Dishes> list)
        {
            List<DishesCategoryDTO> cate_result = new List<DishesCategoryDTO>();
            if (list != null && list.Count > 0)
            {
                var dishes = list.Where(p => p.Status == 1 && p.IsDelete == 0);
                foreach (var item in dishes)
                {
                    if (cate_result.Where(p => p.category_id == item.DishesCategoryId).Count() == 0)
                    {
                        if (item.DishesCategory != null)
                        {
                            cate_result.Add(new DishesCategoryDTO()
                            {
                                category_id = item.DishesCategory.DishesCategoryId,
                                category_name = item.DishesCategory.Name,
                            });
                        }
                    }
                }

                foreach (var item in cate_result)
                {
                    item.dishes_list = DishesDetailListToDTO(list.Where(p => p.DishesCategoryId == item.category_id).ToList());
                }
            }
            return cate_result;
        }
        private string DiscountToString(ActivityDiscount obj)
        {
            StringBuilder sb = new StringBuilder();
            if (obj == null)
            {
                return "";
            }
            else
            {
                sb.Append("全场商品");
                sb.Append(obj.Discount * 10);
                sb.Append("折");
            }
            return sb.ToString();

        }
        private string MinusListToString(List<ActivityMinus> list)
        {
            StringBuilder sb = new StringBuilder(200);
            if (list == null || list.Count <= 0)
            {
                return "";
            }
            int i = 0;
            foreach (var item in list)
            {
                if (i == 0)
                {
                    sb.Append("满");
                    sb.Append(item.AchiveAmount);
                    sb.Append("减");
                    sb.Append(item.MinusAmount);
                }
                else
                {
                    sb.Append("，");
                    sb.Append("满");
                    sb.Append(item.AchiveAmount);
                    sb.Append("减");
                    sb.Append(item.MinusAmount);
                }
                i++;
            }
            return sb.ToString();
        }
        private List<BaseImageDTO> BusinessImageToDTO(List<BusinessImage> list)
        {
            List<BaseImageDTO> result = new List<BaseImageDTO>();
            if (list != null && list.Count > 0)
            {
                result = new List<BaseImageDTO>();
                foreach (var item in list)
                {
                    result.Add(new BaseImageDTO()
                    {
                        img_id = item.BaseImageId,
                        img_path = item.BaseImage == null ? "" : (item.BaseImage.Source + item.BaseImage.Path)
                    });
                }
            }
            return result;
        }
        private List<BaseImageDTO> ProductImageToDTO(List<ProductImage> list)
        {
            List<BaseImageDTO> result = new List<BaseImageDTO>();
            if (list != null && list.Count > 0)
            {
                result = new List<BaseImageDTO>();
                foreach (var item in list)
                {
                    result.Add(new BaseImageDTO()
                    {
                        img_id = item.BaseImageId,
                        img_path = item.BaseImage == null ? "" : (item.BaseImage.Source + item.BaseImage.Path)
                    });
                }
            }
            return result;
        }
        private int GetGoodCommentRate(List<BusinessComment> businessCommentList)
        {
            if (businessCommentList == null || businessCommentList.Count <= 0)
            {
                return 100;
            }
            else
            {
                decimal totalcount = businessCommentList.Count;
                decimal goodcount = businessCommentList.Where(c => c.LevelId <= (int)EnumHelp.Level.一般).ToList().Count;
                return (int)((goodcount / totalcount) * 100);
            }
        }
        private List<ActivityStrDTO> GetActivityStrList(List<ActivityMinus> activityMinusList, ActivityDiscount activityDiscount)
        {
            List<ActivityStrDTO> list = new List<ActivityStrDTO>();
            if (activityDiscount != null)
            {
                list.Add(new ActivityStrDTO()
                {
                    id = activityDiscount.ActivityDiscountId,
                    type = (int)EnumHelp.DiscountType.折扣,
                    activity_descript = "全场商品" + activityDiscount.Discount * 10 + "折"
                });
            }
            if (activityMinusList != null && activityMinusList.Count > 0)
            {
                foreach (var item in activityMinusList)
                {
                    list.Add(new ActivityStrDTO()
                    {
                        id = item.ActivityMinusId,
                        type = (int)EnumHelp.DiscountType.满减,
                        activity_descript = "满" + item.AchiveAmount + "减" + item.MinusAmount
                    });
                }

            }
            return list;
        }
    }
}
