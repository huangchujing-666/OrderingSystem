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
    /// 约饭投诉API
    /// </summary>
    public class RiceDateFeedbackApiController : ApiController
    {
        private readonly IRiceDateService _riceDateService = EngineContext.Current.Resolve<IRiceDateService>();
        private readonly IRiceDateUserService _riceDateUserService = EngineContext.Current.Resolve<IRiceDateUserService>();
        private readonly IUserService _userService = EngineContext.Current.Resolve<IUserService>();
        private readonly IBaseImageService _baseImageService = EngineContext.Current.Resolve<IBaseImageService>();
        private readonly IRiceDateFeedbackService _riceDateFeedbackService = EngineContext.Current.Resolve<IRiceDateFeedbackService>();

        /// <summary>
        /// 提交投诉
        /// </summary>
        /// <param name="riceDateFeedbackDTO">投诉对象</param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<int> PostRiceDateFeedback(RiceDateFeedbackDTO riceDateFeedbackDTO)
        {
            var result = new ResponseModel<int>();
            result.error_code = Result.SUCCESS;
            if (riceDateFeedbackDTO.UserId <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "用户Id无效";
                return result;
            }
            else if (riceDateFeedbackDTO.RiceDateId <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "约饭Id无效";
                return result;
            }
            else
            {
                var riceDate = _riceDateService.GetById(riceDateFeedbackDTO.RiceDateId);
                var riceDateUser = _riceDateUserService.GetByUserIdAndRiceDateId(riceDateFeedbackDTO.UserId, riceDateFeedbackDTO.RiceDateId);
                if (riceDate.BeginDate <= System.DateTime.Now)
                {
                    result.error_code = Result.ERROR;
                    result.message = "约饭未结束无法评价";
                    return result;
                }
                else if (riceDateUser.ApplyStatus == (int)EnumHelp.RiceDateApplyStatus.申请通过)
                {
                    var insertResult = _riceDateFeedbackService.Insert(new RiceDateFeedback()
                    {
                        RiceDateId = riceDateFeedbackDTO.RiceDateId,
                        UserId = riceDateFeedbackDTO.UserId,
                        Content = riceDateFeedbackDTO.Content,
                        CreateTime = System.DateTime.Now,
                        EditTime = System.DateTime.Now
                    });
                    result.data = insertResult.RiceDateFeedbackId;
                }
            }
            return result;
        }
        /// <summary>
        /// 获取报名人的信誉
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="RiceDateId"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<RiceDateUserReputation> GetRiceDateUserFeedback(int UserId)
        {
            var result = new ResponseModel<RiceDateUserReputation>();
            result.error_code = Result.SUCCESS;
            if (UserId <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "用户Id不合法";
            }
            var riceDateFeedback = _riceDateFeedbackService.GetByUserId(UserId);
            var user = _userService.GetById(UserId);
            var data = new RiceDateUserReputation();
            if (riceDateFeedback != null && riceDateFeedback.Count > 0 && user != null)
            {
                int age = 0;
                if (user.BirthDay != null)
                {
                    var BirthDay = DateTime.Parse(user.BirthDay.ToString());
                    age = System.DateTime.Now.Year - BirthDay.Year;
                }
                data.Age = age;
                data.Complain = riceDateFeedback.Count;
                data.NickName = user.NickName;
                data.Path = user.BaseImage == null ? "" : user.BaseImage.Source + user.BaseImage.Path;
                data.Sex = int.Parse(user.Sex.ToString());
                var detailList = new List<ComplainDetails>();
                foreach (var item in riceDateFeedback)
                {







                    detailList.Add(new ComplainDetails()
                    {
                        BusinessName = item.RiceDate.BusinessName,
                        Content = item.Content,
                        datetime = item.CreateTime.ToString("yyyy-MM-dd HH:mm")
                    });
                }
            }
            result.data = data;
            return result;
        }
    }


}
