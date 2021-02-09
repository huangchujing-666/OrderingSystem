using OrderingSystem.Api.Models;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.Domain;
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
    /// <summary>
    /// 约饭报名接口
    /// </summary>
    public class RiceDateUserApiController : ApiController
    {
        private readonly IRiceDateService _riceDateService = EngineContext.Current.Resolve<IRiceDateService>();
        private readonly IRiceDateUserService _riceDateUserService = EngineContext.Current.Resolve<IRiceDateUserService>();
        private readonly IUserService _userService = EngineContext.Current.Resolve<IUserService>();
        //private readonly IBaseImageService _baseImageService = EngineContext.Current.Resolve<IBaseImageService>();
        private readonly IRiceDateFeedbackService _riceDateFeedbackService = EngineContext.Current.Resolve<IRiceDateFeedbackService>();

        /// <summary>
        /// 约饭报名
        /// </summary>
        /// <param name="RiceDateUserDTO">报名实体对象</param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<int> JoinRiceDate(RiceDateUserDTO riceDateUserDTO)
        {
            var result = new ResponseModel<int>();
            result.error_code = Result.SUCCESS;
            if (riceDateUserDTO.RiceDateId <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "约饭Id有误";
            }
            else if (riceDateUserDTO.UserId <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "用户Id有误";
            }
            else
            {
                var riceDate = _riceDateService.GetById(riceDateUserDTO.RiceDateId);
                var user = _userService.GetById(riceDateUserDTO.UserId);
                var riceDateUser = _riceDateUserService.GetByUserIdAndRiceDateId(riceDateUserDTO.UserId, riceDateUserDTO.RiceDateId);
                var riceDateUserList = _riceDateUserService.GetByRiceDateId(riceDateUserDTO.RiceDateId);
                if (riceDate == null)
                {
                    result.error_code = Result.ERROR;
                    result.message = "约饭信息不存在";
                }
                else if (riceDate.Status == (int)EnumHelp.EnabledEnum.无效 || riceDate.IsDelete == (int)EnumHelp.IsDeleteEnum.已删除)
                {
                    result.error_code = Result.ERROR;
                    result.message = "该约饭信息无效";
                }
                else if (user == null)
                {
                    result.error_code = Result.ERROR;
                    result.message = "用户不存在";
                }
                else if (user.Status == (int)EnumHelp.EnabledEnum.无效 || user.IsDelete == (int)EnumHelp.IsDeleteEnum.已删除)
                {
                    result.error_code = Result.ERROR;
                    result.message = "用户无效";
                }
                else if (riceDateUser != null)
                {
                    result.error_code = Result.ERROR;
                    result.message = "该用户已报名过此约饭";
                }
                else if (riceDateUserList != null && riceDateUserList.Count >= riceDate.UserCount)
                {
                    result.error_code = Result.ERROR;
                    result.message = "报名人数已满";
                }
                else
                {
                    var insertResult = _riceDateUserService.Insert(new RiceDateUser()
                    {
                        CreateTime = System.DateTime.Now,
                        EditTime = System.DateTime.Now,
                        RiceDateId = riceDateUserDTO.RiceDateId,
                        UserId = riceDateUserDTO.UserId,
                        ApplyStatus = (int)EnumHelp.RiceDateApplyStatus.申请中
                    });
                    result.data = insertResult.RiceDateUserId;
                }
            }
            return result;
        }

        /// <summary>
        /// 取消约饭报名
        /// </summary>
        /// <param name="riceDateUserDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<int> CancleRiceDate(RiceDateUserDTO riceDateUserDTO)
        {
            var result = new ResponseModel<int>();
            result.error_code = Result.SUCCESS;
            if (riceDateUserDTO.RiceDateId <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "约饭Id有误";
            }
            else if (riceDateUserDTO.UserId <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "用户Id有误";
            }
            else
            {
                var riceDateUser = _riceDateUserService.GetByUserIdAndRiceDateId(riceDateUserDTO.UserId, riceDateUserDTO.RiceDateId);
                if (riceDateUser != null && riceDateUser.RiceDateUserId > 0)
                {
                    _riceDateUserService.Delete(riceDateUser);
                    result.data = 1;
                }
                else
                {
                    result.error_code = Result.ERROR;
                    result.message = "没有此报名信息";
                }
            }
            return result;
        }

        /// <summary>
        /// 获取我参加的约饭  我的报名
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Page_Index"></param>
        /// <param name="Page_Size"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<List<RiceDateDetailDTO>> GetMyJoinRiceDate([FromUri]int UserId, int Page_Index = 1, int Page_Size = 20)
        {
            var result = new ResponseModel<List<RiceDateDetailDTO>>();
            result.error_code = Result.SUCCESS;
            int totalCount = 0;
            var data = new List<RiceDateDetailDTO>();
            if (UserId <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "用户Id不合法";
            }
            var riceDateUserList = _riceDateUserService.GetByUserId(UserId, Page_Index, Page_Size, out totalCount);
            if (riceDateUserList != null && riceDateUserList.Count > 0)
            {
                foreach (var item in riceDateUserList)
                {
                    int riceDateStatus = 0;
                    var riceDateList = _riceDateUserService.GetByRiceDateId(item.RiceDateId);
                    var feedBack = _riceDateFeedbackService.GetByUserIdAndRiceDateId(UserId, item.RiceDateId);
                    if (riceDateList != null && riceDateList.Count > 0)
                    {
                        int count = riceDateList.Where(c => c.ApplyStatus == (int)EnumHelp.RiceDateApplyStatus.申请通过).ToList().Count;
                        if (feedBack != null)
                        {
                            riceDateStatus = (int)EnumHelp.RiceDateStatus.被投诉;
                        }
                        else if (item.ApplyStatus == (int)EnumHelp.RiceDateApplyStatus.申请中 && item.RiceDate.BeginDate >= System.DateTime.Now)
                        {
                            riceDateStatus = (int)EnumHelp.RiceDateStatus.等待确认;
                        }
                        else if (count >= item.RiceDate.UserCount)
                        {
                            riceDateStatus = (int)EnumHelp.RiceDateStatus.愉快用餐;
                        }

                    }
                    int age = 0;
                    if (item.RiceDate.User.BirthDay != null)
                    {
                        var BirthDay = DateTime.Parse(item.RiceDate.User.BirthDay.ToString());
                        age = System.DateTime.Now.Year - BirthDay.Year;
                    }
                    data.Add(new RiceDateDetailDTO()
                    {
                        BusinessName = item.RiceDate.BusinessName,
                        Address = item.RiceDate.Address,
                        BeginDate = item.RiceDate.BeginDate,
                        UseCount = item.RiceDate.UserCount,
                        Zone = item.RiceDate.Zone,
                        Sex = item.RiceDate.Sex,
                        Age = item.RiceDate.Age,
                        Taste = item.RiceDate.Taste,
                        UserImage = item.RiceDate.User == null ? "" : (item.RiceDate.User.BaseImage.Source + item.RiceDate.User.BaseImage.Path),
                        PayWay = item.RiceDate.PayWay,
                        Usex = item.RiceDate.User.Sex == null ? 0 : int.Parse(item.RiceDate.User.Sex.ToString()),
                        Uage = age,
                        RiceDateStatu = riceDateStatus
                    });
                }
            }
            result.total_count = totalCount;
            result.data = data;
            return result;
        }

        /// <summary>
        /// 获取报名我发布约饭的用户  报名申请
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Page_Index"></param>
        /// <param name="Page_Size"></param>
        /// <returns></returns>
        public ResponseModel<List<RiceDateDetailDTO>> GetJoinMyRiceDate([FromUri]int UserId, int Page_Index = 1, int Page_Size = 20)
        {
            var result = new ResponseModel<List<RiceDateDetailDTO>>();
            result.error_code = Result.SUCCESS;
            int totalCount = 0;
            var data = new List<RiceDateDetailDTO>();
            if (UserId <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "用户Id不合法";
            }
            var riceDateUserList = _riceDateUserService.GetJoinMyRiceDate(UserId, Page_Index, Page_Size, out totalCount);
            if (riceDateUserList != null && riceDateUserList.Count > 0)
            {
                foreach (var item in riceDateUserList)
                {
                    data.Add(new RiceDateDetailDTO()
                    {
                        RiceDateId = item.RiceDateId,
                        UserImage = item.User.BaseImage == null ? "" : (item.User.BaseImage.Source + item.User.BaseImage.Path),
                        BusinessName = item.RiceDate.BusinessName,
                        BeginDate = item.RiceDate.BeginDate,
                        JoinTime = item.CreateTime,
                        RiceDateStatu = item.ApplyStatus,
                        UserId = item.UserId,
                        RiceDateUserId = item.RiceDateUserId
                    });
                }

            }
            result.total_count = totalCount;
            result.data = data;
            return result;
        }

        [HttpPost]
        /// <summary>
        /// 确认约饭/驳回约饭
        /// </summary>
        /// <param name="riceDateUserConfirmDTO"></param>
        /// <returns></returns>
        public ResponseModel<int> RiceDateUserConfirm(RiceDateUserConfirmDTO riceDateUserConfirmDTO)
        {
            var result = new ResponseModel<int>();
            result.error_code = Result.SUCCESS;
            EnumHelp.RiceDateApplyStatus status;
            if (riceDateUserConfirmDTO.ApplyStatus <= 0 || !Enum.TryParse<EnumHelp.RiceDateApplyStatus>(riceDateUserConfirmDTO.ApplyStatus.ToString(), out status))
            {
                result.error_code = Result.ERROR;
                result.message = "约饭状态有误";
            }
            else if (riceDateUserConfirmDTO.RiceDateUserId <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "报名Id有误";
            }
            else
            {
                var riceDateUser = _riceDateUserService.GetById(riceDateUserConfirmDTO.RiceDateUserId);
                riceDateUser.ApplyStatus = riceDateUserConfirmDTO.ApplyStatus;
                _riceDateUserService.Update(riceDateUser);
                result.data = 1;
            }
            return result;
        }
    }
}
