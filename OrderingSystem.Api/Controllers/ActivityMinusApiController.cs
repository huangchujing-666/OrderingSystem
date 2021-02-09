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
using static OrderingSystem.Domain.EnumHelp;

namespace OrderingSystem.Api.Controllers
{
    public class ActivityMinusApiController : ApiController
    {
        private readonly IActivityMinusService _activityMinusService = EngineContext.Current.Resolve<IActivityMinusService>();
        private readonly IActivityDiscountService _activityDiscountService = EngineContext.Current.Resolve<IActivityDiscountService>();
        private readonly IBusinessInfoService _businessInfoService = EngineContext.Current.Resolve<IBusinessInfoService>();
        /// <summary>
        /// 根据商家id获取折扣信息和满减信息
        /// </summary>
        /// <param name="Business_Id"></param>
        /// <returns></returns>
        public ResponseModel<ActivityDTO> GetDisCountByBusinessId(int Business_Id)
        {
            var result = new ResponseModel<ActivityDTO>();
            result.error_code = Result.SUCCESS;
            string key = "GetDisCountByBusinessId" + Business_Id;
            if (Business_Id <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "参数不合法";
                result.data = null;
                return result;
            }
            else if (RedisDb._redisHelper_11().KeyExists(key))
            {
                var data = RedisDb._redisHelper_11().StringGet<ActivityDTO>(key);
                result.data = data;
                result.total_count = 1;
                return result;
            }
            else
            {
                var businessInfo = _businessInfoService.GetById(Business_Id);
                if (businessInfo != null)
                {
                    var activityDTO = new ActivityDTO();
                    if (businessInfo.ActivityDiscount != null)
                    {
                        var activityDto= new ActivityDiscountDTO()
                        {
                            id = businessInfo.ActivityDiscount.ActivityDiscountId,
                            discount = businessInfo.ActivityDiscount.Discount,
                            business_id = businessInfo.BusinessInfoId
                        };
                        activityDTO.discount= activityDto;
                        
                    }
                    var businessMinusList = businessInfo.ActivityMinusList.Where(c => c.IsDelete == (int)IsDeleteEnum.有效 && c.Status == (int)EnabledEnum.有效).ToList();
                    if (businessMinusList != null)
                    {
                        var minusList = new List<ActivityMinusDTO>();
                        foreach (var item in businessMinusList)
                        {
                            minusList.Add(new ActivityMinusDTO()
                            {
                                achieve_amount =Convert.ToInt32( item.AchiveAmount),
                                activityminusid = item.ActivityMinusId,
                                business_id = item.BusinessInfoId,
                                minus_amount =  Convert.ToInt32(item.MinusAmount)
                            });
                        }
                        activityDTO.minus = minusList;
                    }
                    result.data = activityDTO;
                    RedisDb._redisHelper_11().StringSet(key, result.data, RedisConfig._defaultExpiry);
                }
            }
            return result;

        }
    }
}
