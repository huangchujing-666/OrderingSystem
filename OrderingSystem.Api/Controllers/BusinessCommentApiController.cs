using OrderingSystem.Api.Models;
using OrderingSystem.Cache.Redis;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using static OrderingSystem.Domain.EnumHelp;

namespace OrderingSystem.Api.Controllers
{
    public class BusinessCommentApiController : ApiController
    {
        private readonly IBusinessInfoService _businessInfoService = EngineContext.Current.Resolve<IBusinessInfoService>();
        private readonly IBusinessCommentService _businessCommentService = EngineContext.Current.Resolve<IBusinessCommentService>();

        /// <summary>
        /// 获取评论等级和数量
        /// </summary>
        /// <param name="Bussiness_Id">商家ID</param>
        /// <returns></returns>
        public Object GetCommentLevelDataByBusinessId(int Bussiness_Id)
        {
            return null;
        }

        /// <summary>
        /// 获取商家评论分页列表
        /// </summary>
        /// <param name="Bussiness_Id">商家Id</param>
        /// <param name="Level_Id">评论等级Id</param>
        /// <param name="Page_Index">Page_Index</param>
        /// <returns></returns>
        public ResponseModel<BusinessCommentDTO> GetCommentListByBusinessId(int Bussiness_Id, int Level_Id = 0, int Page_Size = 20, int Page_Index = 1)
        {
            var result = new ResponseModel<Models.BusinessCommentDTO>();
            Models.BusinessCommentDTO list = new Models.BusinessCommentDTO();
            //查询缓存数据
            //string key = "GetCommentListByBusinessId_" + Bussiness_Id + "_" + Level_Id + "_" + Page_Index;
            //string key_count = "GetCommentListCountByBusinessId_" + Bussiness_Id + "_" + Level_Id + "_" + Page_Index;
            //if (RedisDb._redisHelper_11().KeyExists(key))
            //{
            //    var data = RedisDb._redisHelper_11().StringGet<Models.BusinessCommentDTO>(key);
            //    result.data = data;
            //    result.total_count = RedisDb._redisHelper_11().StringGet<int>(key_count);
            //}
            //else
            //{
            int total_count = 0;
            list.comment_list = new List<CommentDTO>();
            var model = _businessCommentService.GetPageList(Bussiness_Id, Level_Id, Page_Index, Page_Size, out total_count);

            foreach (var item in model)
            {
                var data = new Models.CommentDTO()
                {
                    comment_id = item.BusinessCommentId,
                    content = item.Contents,
                    create_date = item.CreateTime.ToString("yyyy-MM-dd"),
                    is_anonymous = item.IsAnonymous,
                    level_id = item.LevelId,
                    level_name = item.Level == null ? "" : item.Level.Name,
                    user_id = item.UserId,
                    user_name = item.User == null ? "" : item.User.NickName,
                    user_img_path = item.User == null ? "" : (item.User.BaseImage == null ? "" : (item.User.BaseImage.Source + item.User.BaseImage.Path)),
                    user_img_id = item.User == null ? 0 : (item.User.BaseImage == null ? 0 : item.User.BaseImage.BaseImageId),
                    imges_list = item.CommentImageList == null ? null : CommentImageListToDTO(item.CommentImageList),
                    order_dishes_list = item.RecommendDishes,
                };
                list.comment_list.Add(data);
            }

            result.data = list;
            result.total_count = total_count;
            //设置缓存数据
            //RedisDb._redisHelper_11().StringSet(key, list, RedisConfig._defaultExpiry);
            //RedisDb._redisHelper_11().StringSet(key_count, total_count, RedisConfig._defaultExpiry);
            //}
            return result;
        }

        /// <summary>
        /// 获取商家评论分页列表
        /// </summary>
        /// <param name="Bussiness_Id">商家Id</param>
        /// <param name="Level_Id">评论等级Id</param>
        /// <param name="Page_Index">Page_Index</param>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<BusinessCommentDTO> GetCommentListByUserId(int User_Id, int Module = 0, int Level_Id = 0, int Page_Size = 20, int Page_Index = 1)
        {
            var result = new ResponseModel<Models.BusinessCommentDTO>();
            Models.BusinessCommentDTO list = new Models.BusinessCommentDTO();
            //查询缓存数据
            //string key = "GetCommentListByBusinessId_" + Bussiness_Id + "_" + Level_Id + "_" + Page_Index;
            //string key_count = "GetCommentListCountByBusinessId_" + Bussiness_Id + "_" + Level_Id + "_" + Page_Index;
            //if (RedisDb._redisHelper_11().KeyExists(key))
            //{
            //    var data = RedisDb._redisHelper_11().StringGet<Models.BusinessCommentDTO>(key);
            //    result.data = data;
            //    result.total_count = RedisDb._redisHelper_11().StringGet<int>(key_count);
            //}
            //else
            //{
            int total_count = 0;
            list.comment_list = new List<CommentDTO>();
            var model = _businessCommentService.GetPageListByUserId(User_Id, Module, Level_Id, Page_Index, Page_Size, out total_count);

            foreach (var item in model)
            {
                var data = new Models.CommentDTO()
                {
                    comment_id = item.BusinessCommentId,
                    content = item.Contents,
                    create_date = item.CreateTime.ToString("yyyy-MM-dd hh:mm:ss"),
                    is_anonymous = item.IsAnonymous,
                    level_id = item.LevelId,
                    level_name = item.Level == null ? "" : item.Level.Name,
                    user_id = item.UserId,
                    user_name = item.User == null ? "" : item.User.NickName,
                    user_img_path = item.User == null ? "" : (item.User.BaseImage == null ? "" : (item.User.BaseImage.Source + item.User.BaseImage.Path)),
                    user_img_id = item.User == null ? 0 : (item.User.BaseImage == null ? 0 : item.User.BaseImage.BaseImageId),
                    imges_list = item.CommentImageList == null ? null : CommentImageListToDTO(item.CommentImageList),
                    order_dishes_list = item.RecommendDishes,
                    business_id = item.BusinessInfo == null ? 0 : item.BusinessInfo.BusinessInfoId,
                    business_name = item.BusinessInfo == null ? "" : item.BusinessInfo.Name,
                };
                list.comment_list.Add(data);
            }

            result.data = list;
            result.total_count = total_count;
            //设置缓存数据
            //RedisDb._redisHelper_11().StringSet(key, list, RedisConfig._defaultExpiry);
            //RedisDb._redisHelper_11().StringSet(key_count, total_count, RedisConfig._defaultExpiry);
            //}
            return result;
        }

        /// <summary>
        /// 根据评论Id获取用户评论信息
        /// </summary>
        /// <param name="Comment_Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<CommentDTO> GetCommentByCommentId(int Comment_Id)
        {
            var result = new ResponseModel<CommentDTO>();
            result.message = "";
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            var obj = new CommentDTO();
            if (Comment_Id > 0)
            {
                var businessComment = _businessCommentService.GetById(Comment_Id);
                if (businessComment != null)
                {
                    obj.business_id = businessComment.BusinessInfoId;
                    obj.business_name = businessComment.BusinessInfo == null ? "" : businessComment.BusinessInfo.Name;
                    obj.business_image_path = businessComment.BusinessInfo == null ? "" : (businessComment.BusinessInfo.BaseImage == null ? "" : (businessComment.BusinessInfo.BaseImage.Source + businessComment.BusinessInfo.BaseImage.Path));
                    obj.comment_id = businessComment.BusinessCommentId;
                    obj.content = businessComment.Contents;
                    obj.create_date = businessComment.CreateTime.ToString("yyyy-MM-dd hh:mm:ss");
                    obj.imges_list = businessComment.CommentImageList == null ? null : CommentImageListToDTO(businessComment.CommentImageList);
                    obj.is_anonymous = businessComment.IsAnonymous;
                    obj.level_id = businessComment.LevelId;
                    obj.level_name = businessComment.Level == null ? "" : businessComment.Level.Name;
                    obj.order_dishes_list = businessComment.RecommendDishes;
                    obj.user_id = businessComment.UserId;
                    obj.user_name = businessComment.User == null ? "" : businessComment.User.NickName;
                    obj.user_img_id = businessComment.User == null ? 0 : (businessComment.User.BaseImage == null ? 0 : businessComment.User.BaseImage.BaseImageId);
                    obj.user_img_path = businessComment.User == null ? "" : (businessComment.User.BaseImage == null ? "" : (businessComment.User.BaseImage.Source + businessComment.User.BaseImage.Path));
                }
                result.data = obj;
            }
            else
            {
                result.message = "参数不合法";
                result.error_code = Result.ERROR;
            }
            return result;
        }
        [HttpPost]
        public ResponseModel<DelBusinessCommentDTO> DeleteCommentByUserId(DelBusinessCommentDTO delBusinessCommentDTO)
        {
            var result = new ResponseModel<DelBusinessCommentDTO>();
            result.message = "";
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            result.data = delBusinessCommentDTO;
            if (delBusinessCommentDTO.User_Id > 0 && delBusinessCommentDTO.Business_Comment_Id > 0)
            {
                var getresult = _businessCommentService.GetById(delBusinessCommentDTO.Business_Comment_Id);
                if (getresult == null)
                {
                    result.error_code = Result.ERROR;
                    result.message = "该用户评价不存在";
                }
                else if (getresult.UserId == delBusinessCommentDTO.User_Id)
                {
                    getresult.IsDelete = (int)IsDeleteEnum.已删除;
                    _businessCommentService.Update(getresult);
                }
                else
                {
                    result.error_code = Result.ERROR;
                    result.message = "无法删除此评论";
                }
            }
            else
            {
                result.error_code = Result.ERROR;
                result.message = "参数不合法";
            }
            return result;
        }

        /// <summary>
        /// 返回评论图片集合
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<BaseImageDTO> CommentImageListToDTO(List<CommentImage> list)
        {
            List<BaseImageDTO> result = null;
            if (list != null)
            {
                result = new List<BaseImageDTO>();
                foreach (var item in list)
                {
                    result.Add(new BaseImageDTO()
                    {
                        img_id = item.BaseImageId,
                        img_path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path,
                    });
                }
            }
            return result;
        }


    }
}
