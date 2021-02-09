using OrderingSystem.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderingSystem.IService;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.Cache.Redis;

namespace OrderingSystem.Api.Controllers
{
    /// <summary>
    /// 首页轮播图接口
    /// </summary>
    public class BusinessBannerImageApiController : ApiController
    {
        private readonly IBusinessBannerImageService _businessBannerImageService = EngineContext.Current.Resolve<IBusinessBannerImageService>();
        private readonly IBaseImageService _baseImageService = EngineContext.Current.Resolve<IBaseImageService>();
        /// <summary>
        /// 首页获取轮播图数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<List<Carousel>> GetLunboData(int Module)
        {
            var result = new ResponseModel<List<Carousel>>();
            result.message = "";
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            List<Carousel> list = new List<Carousel>();
            if (Module<=0)
            {
                result.error_code= Result.ERROR;
                result.message = "参数有误";
                result.data = list;
                return result;
            }
            string _key = "GetLunboData_"+ Module;
            if (RedisDb._redisHelper_11().KeyExists(_key))
            {
                var data = RedisDb._redisHelper_11().StringGet<List<Carousel>>(_key);
                result.data = data;
                result.total_count = data.Count;
            }
            else
            {
                var getList = _businessBannerImageService.GetListByModule(Module);
                if (getList!=null&& getList.Count>0)
                {
                    foreach (var item in getList)
                    {
                        list.Add(new Carousel()
                        {
                            business_id = item.BusinessInfoId,
                            carousel_id = item.BusinessBannerImageId,
                            carousel_img_id = item.BaseImageId,
                            carousel_img_path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path,//需要添加一个默认图片的路径
                            carousel_name = item.Descript,
                            sort_no = item.SortNo
                        });
                    }
                }
                result.data = list;
                result.total_count = list.Count;
                RedisDb._redisHelper_11().StringSet(_key, list, RedisConfig._defaultExpiry);
            }
            return result;
        }
    }
}
