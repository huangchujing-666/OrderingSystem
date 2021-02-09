using OrderingSystem.Api.Models;
using OrderingSystem.Cache.Redis;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OrderingSystem.Api.Controllers
{
    public class BusinessGroupApiController : ApiController
    {
        private readonly IBusinessGroupService _businessGroupService = EngineContext.Current.Resolve<IBusinessGroupService>();

        /// <summary>
        /// 乐模块首页获取商家分组
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<List<BusinessGroupDTO>> GetBusinessGroup()
        {
            var result = new ResponseModel<List<BusinessGroupDTO>>();
            result.message = "";
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            List<BusinessGroupDTO> list = new List<BusinessGroupDTO>();
            //查询缓存数据
            string key = "BusinessGroupApi_GetBusinessGroup";
            string key_count = "BusinessGroupApi_GetBusinessGroup_ListCount";
            if (RedisDb._redisHelper_11().KeyExists(key))
            {
                var data = RedisDb._redisHelper_11().StringGet<List<BusinessGroupDTO>>(key);
                result.data = data;
                result.total_count = RedisDb._redisHelper_11().StringGet<int>(key_count);
            }
            else
            {
                int total_count = 0;
                //list.comment_list = new List<CommentDTO>();
                //var model = _businessCommentService.GetPageListByUserId(User_Id, Level_Id, Page_Index, Page_Size, out total_count);
                var getlist = _businessGroupService.GetAll().OrderBy(c => c.Sort).ToList();
                if (getlist != null && getlist.Count > 0)
                {

                    foreach (var item in getlist)
                    {
                        if (item.Name== "电影")
                        {
                            continue;
                        }
                        list.Add(
                            new BusinessGroupDTO()
                            {
                                business_group_id = item.BusinessGroupId,
                                name = item.Name,
                                sort = item.Sort,
                                base_image_id = item.BaseImage == null ? 0 : item.BaseImage.BaseImageId,
                                path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path,
                                BusinessGroupImageList = BusinessGroupImageToDTO(item.BusinessGroupImageList)
                            });
                    }
                    total_count = list.Count;
                }
                result.data = list;
                result.total_count = total_count;
                //设置缓存数据
                RedisDb._redisHelper_11().StringSet(key, list, RedisConfig._defaultExpiry);
                RedisDb._redisHelper_11().StringSet(key_count, total_count, RedisConfig._defaultExpiry);
            }
            return result;
        }

        private List<BusinessGroupImageDTO> BusinessGroupImageToDTO(List<BusinessGroupImage> list)
        {
            List<BusinessGroupImageDTO> result = null;
            if (list != null && list.Count > 0)
            {
                result = new List<BusinessGroupImageDTO>();
                foreach (var item in list.OrderBy(p=>p.Type))
                {
                    result.Add(new BusinessGroupImageDTO()
                    {
                        type = item.Type,
                        path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path
                    });
                }
            }
            return result;
        }

    }
}
