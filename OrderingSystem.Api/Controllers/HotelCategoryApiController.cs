using OrderingSystem.Api.Models;
using OrderingSystem.Cache.Redis;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderingSystem.Api.Controllers
{
    public class HotelCategoryApiController : ApiController
    {
        private readonly IHotelCategoryService _hotelCategoryService = EngineContext.Current.Resolve<IHotelCategoryService>();
        public ResponseModel<List<HotelCategoryDTO>> GetHotelCategoryList()
        {
            var result = new ResponseModel<List<HotelCategoryDTO>>();
            result.message = "";
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            List<HotelCategoryDTO> list = new List<HotelCategoryDTO>();
            string key = "HotelCategoryApi_GetHotelCategoryList";
            string key_count = "HotelCategoryApi_GetHotelCategoryList_Count";
            if (RedisDb._redisHelper_11().KeyExists(key))
            {
                var data = RedisDb._redisHelper_11().StringGet<List<HotelCategoryDTO>>(key);
                result.data = data;
                result.total_count = RedisDb._redisHelper_11().StringGet<int>(key_count);
            }
            else
            {
                var hotelCategorylist = _hotelCategoryService.GetAll();
                if (hotelCategorylist != null && hotelCategorylist.Count > 0)
                {
                    result.total_count = hotelCategorylist.Count;
                    foreach (var item in hotelCategorylist)
                    {
                        list.Add(new HotelCategoryDTO()
                        {
                            HotelCategoryId = item.HotelCategoryId,
                            Name = item.Name
                        });
                    }
                }
                list = list.OrderBy(p => p.HotelCategoryId).ToList();
                result.data = list;
                //设置缓存数据
                RedisDb._redisHelper_11().StringSet(key, list, RedisConfig._defaultExpiry);
                RedisDb._redisHelper_11().StringSet(key_count, result.total_count, RedisConfig._defaultExpiry);
            }
            return result;
        }
    }
}
