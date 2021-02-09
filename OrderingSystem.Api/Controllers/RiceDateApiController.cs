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
    /// 约饭接口
    /// </summary>
    public class RiceDateApiController : ApiController
    {
        private readonly IRiceDateService _riceDateService = EngineContext.Current.Resolve<IRiceDateService>();
        private readonly IRiceDateUserService _riceDateUserService = EngineContext.Current.Resolve<IRiceDateUserService>();
        private readonly IUserService _userService = EngineContext.Current.Resolve<IUserService>();
        private readonly IBaseImageService _baseImageService = EngineContext.Current.Resolve<IBaseImageService>();
        private readonly IRiceDateFeedbackService _riceDateFeedbackService = EngineContext.Current.Resolve<IRiceDateFeedbackService>();
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="RiceDateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<int> PublicRiceDate(RiceDateDTO RiceDateDTO)
        {
            var result = new ResponseModel<int>();
            result.error_code = Result.SUCCESS;
            var user = _userService.GetById(RiceDateDTO.UserId);
            if (user != null && user.UserId > 0)
            {
                if (user.IsDelete == (int)EnumHelp.IsDeleteEnum.已删除 || user.Status == (int)EnumHelp.EnabledEnum.无效)
                {
                    result.error_code = Result.ERROR;
                    result.message = "用户Id无效";
                    return result;
                }
                else
                {
                    string[] ids = RiceDateDTO.BaseImageIds.Split(',').ToArray();
                    int[] output = Array.ConvertAll<string, int>(ids, delegate (string s) { return int.Parse(s); });
                    var baseImageList = _baseImageService.GetByIds(output);
                    if (baseImageList.Count != output.Length)
                    {
                        result.error_code = Result.ERROR;
                        result.message = "展示图id有误";
                        return result;
                    }
                    var riceDate = new RiceDate()
                    {
                        Address = RiceDateDTO.Address,
                        Age = RiceDateDTO.Age,
                        BeginDate = RiceDateDTO.BeginDate,
                        BusinessName = RiceDateDTO.BusinessName,
                        CreateTime = System.DateTime.Now,
                        EditTime = System.DateTime.Now,
                        PayWay = RiceDateDTO.PayWay,
                        Remark = RiceDateDTO.Remark,
                        Sex = RiceDateDTO.Sex,
                        Taste = RiceDateDTO.Taste,
                        UserCount = RiceDateDTO.UseCount,
                        Zone = RiceDateDTO.Zone,
                        UserId = RiceDateDTO.UserId,
                        BaseImageIds = RiceDateDTO.BaseImageIds,
                        IsDelete = (int)EnumHelp.IsDeleteEnum.有效,
                        Status = (int)EnumHelp.EnabledEnum.有效
                    };
                    var InsertResult = _riceDateService.Insert(riceDate);
                    result.data = InsertResult.RiceDateId;
                }
            }
            else
            {
                result.data = 0;
                result.error_code = Result.ERROR;
                result.message = "用户不存在";
            }
            return result;
        }

        /// <summary>
        /// 获取约饭信息
        /// </summary>
        /// <param name="UserId">当前用户Id</param>
        /// <param name="RiceDateId">约饭Id</param>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<RiceDateDetailDTO> GetRiceDateDetail([FromUri]int UserId, int RiceDateId)
        {
            var result = new ResponseModel<RiceDateDetailDTO>();
            var data = new RiceDateDetailDTO();
            result.error_code = Result.SUCCESS;
            if (RiceDateId <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "约饭信息ID无效";
                return result;
            }
            var riceDate = _riceDateService.GetById(RiceDateId);
            if (riceDate.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && riceDate.Status == (int)EnumHelp.EnabledEnum.有效)
            {
                data.Address = riceDate.Address;
                data.Age = riceDate.Age;
                data.BeginDate = riceDate.BeginDate;
                data.BusinessName = riceDate.BusinessName;
                data.NickName = riceDate.User != null ? riceDate.User.NickName : "";
                data.PayWay = riceDate.PayWay;
                data.Remark = riceDate.Remark;
                data.RiceDateId = riceDate.RiceDateId;
                data.Sex = riceDate.Sex;
                data.Taste = riceDate.Taste;
                data.UserImage = riceDate.User == null ? "" : (riceDate.User.BaseImage == null ? "" : riceDate.User.BaseImage.Source + riceDate.User.BaseImage.Path);
                data.Zone = riceDate.Zone;
                data.UseCount = riceDate.UserCount;
                var riceDateUserList = _riceDateUserService.GetByUserId(riceDate.UserId);
                data.DateCount = riceDateUserList.Count;
                string[] ids = riceDate.BaseImageIds.Split(',').ToArray();
                int[] output = Array.ConvertAll<string, int>(ids, delegate (string s) { return int.Parse(s); });
                var baseImageList = _baseImageService.GetByIds(output);
                List<string> imageList = new List<string>();
                if (baseImageList != null && baseImageList.Count > 0)
                {
                    foreach (var item in baseImageList)
                    {
                        imageList.Add(item == null ? "" : item.Source + item.Path);
                    }
                }
                data.ImagePath = imageList;
                ///投诉
                var riceDateFeedbacklist = _riceDateFeedbackService.GetByUserId(riceDate.UserId);
                var complainList = new List<ComplainDetail>();
                if (riceDateFeedbacklist != null && riceDateFeedbacklist.Count > 0)
                {
                    data.ComplainCount = riceDateFeedbacklist.Count;
                    foreach (var item in riceDateFeedbacklist)
                    {
                        complainList.Add(new ComplainDetail()
                        {
                            NickName = item.User == null ? "" : item.User.NickName,
                            Content = item.Content
                        });
                    }
                }
                data.ComplainDetailList = complainList;

                //是否已经报名
                var riceDateUser = _riceDateUserService.GetByUserIdAndRiceDateId(UserId, RiceDateId);
                if (riceDateUser != null)
                {
                    data.IsDate = 1;
                }

                if (riceDate.BeginDate >= System.DateTime.Now && riceDateUser.ApplyStatus == (int)EnumHelp.RiceDateApplyStatus.申请通过)
                {
                    data.IsComplain = 1;
                }

                int age = 0;
                if (riceDate.User.BirthDay != null)
                {
                    var BirthDay = DateTime.Parse(riceDate.User.BirthDay.ToString());
                    age = System.DateTime.Now.Year - BirthDay.Year;
                }
                data.Uage = age;
                data.Usex = string.IsNullOrWhiteSpace(riceDate.User.Sex.ToString()) ? 0 : int.Parse(riceDate.User.Sex.ToString());
            }
            else
            {
                result.error_code = Result.ERROR;
                result.message = "约饭信息无效";
            }
            result.data = data;
            return result;
        }

        /// <summary>
        /// 删除发布的约饭
        /// </summary>
        /// <param name="deleteRiceDate"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<int> DeleteRiceDate(RiceDateUserDTO deleteRiceDate)
        {
            var result = new ResponseModel<int>();
            result.error_code = Result.SUCCESS;
            var riceDate = _riceDateService.GetByUserIdAndRiceDateId(deleteRiceDate.RiceDateId, deleteRiceDate.UserId);
            if (riceDate != null)
            {
                riceDate.IsDelete = (int)EnumHelp.IsDeleteEnum.已删除;
                _riceDateService.Update(riceDate);
                result.data = 1;
            }
            else
            {
                result.error_code = Result.ERROR;
                result.data = 0;
            }
            return result;
        }


        /// <summary>
        /// 主页列表搜索页面
        /// </summary>
        /// <param name="Date">约饭日期</param>
        /// <param name="BusinessName">商家名称</param>
        /// <param name="District">区域</param>
        /// <param name="Page_Index"></param>
        /// <param name="Page_Size"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<List<RiceDateDetailDTO>> GetRiceDateByCondiction([FromUri]DateTime? Date, string BusinessName = "", string District = "", int Page_Index = 1, int Page_Size = 20)
        {
            var result = new ResponseModel<List<RiceDateDetailDTO>>();
            result.error_code = Result.SUCCESS;
            int totalCount = 0;
            var data = new List<RiceDateDetailDTO>();
            var searchResult = _riceDateService.GetRiceDateByCondiction(Date, BusinessName, District, Page_Index, Page_Size, out totalCount);
            if (searchResult != null && searchResult.Count > 0)
            {
                foreach (var item in searchResult)
                {
                    string[] ids = item.BaseImageIds.Split(',').ToArray();
                    int[] output = Array.ConvertAll<string, int>(ids, delegate (string s) { return int.Parse(s); });
                    var baseImageList = _baseImageService.GetByIds(output);
                    List<string> imageList = new List<string>();
                    if (baseImageList != null && baseImageList.Count > 0)
                    {
                        foreach (var imgitem in baseImageList)
                        {
                            imageList.Add(imgitem == null ? "" : imgitem.Source + imgitem.Path);
                        }
                    }
                    int age = 0;
                    if (item.User.BirthDay != null)
                    {
                        var BirthDay = DateTime.Parse(item.User.BirthDay.ToString());
                        age = System.DateTime.Now.Year - BirthDay.Year;
                    }
                    data.Add(new RiceDateDetailDTO()
                    {
                        RiceDateId = item.RiceDateId,
                        BeginDate = item.BeginDate,
                        Age = item.Age,
                        Sex = item.Sex,
                        Usex = string.IsNullOrWhiteSpace(item.User.Sex.ToString()) ? 0 : int.Parse(item.User.Sex.ToString()),
                        Uage = age,
                        Address = item.Address,
                        BusinessName = item.BusinessName,
                        NickName = item.User.NickName,
                        PayWay = item.PayWay,
                        UserId = item.UserId,
                        Taste = item.Taste,
                        Remark = item.Remark,
                        Zone = item.Zone,
                        ImagePath = imageList,
                        DateCount = item.UserCount,
                        UserImage = item.User == null ? "" : (item.User.BaseImage == null ? "" : item.User.BaseImage.Source + item.User.BaseImage.Path)
                    });
                }
            }
            result.data = data;
            result.total_count = totalCount;
            return result;
        }

        /// <summary>
        /// 获取我发布的约饭  我的发布
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Page_Index"></param>
        /// <param name="Page_Size"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<List<RiceDateDetailDTO>> GetMyPubilcRiceDate([FromUri]int UserId, int Page_Index = 1, int Page_Size = 20)
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
            var riceDateList = _riceDateService.GetByUserId(UserId, Page_Index, Page_Size, out totalCount);
            if (riceDateList != null && riceDateList.Count > 0)
            {
                foreach (var item in riceDateList)
                {
                    int riceDateStatus = 0;
                    if (item.BeginDate >= System.DateTime.Now)
                    {
                        //int userCount = item.RiceDateUserList != null ? item.RiceDateUserList.Count : 0;
                        int userCount = item.RiceDateUserList != null ? item.RiceDateUserList.Where(c => c.ApplyStatus == (int)EnumHelp.RiceDateApplyStatus.申请通过).ToList().Count : 0;

                        if (userCount == item.UserCount)
                        {
                            riceDateStatus = (int)EnumHelp.RiceDateStatus.组团成功;
                        }
                        else if (userCount < item.UserCount)
                        {
                            riceDateStatus = (int)EnumHelp.RiceDateStatus.组团中;
                        }
                    }
                    else
                    {
                        int userCount = item.RiceDateUserList != null ? item.RiceDateUserList.Where(c => c.ApplyStatus == (int)EnumHelp.RiceDateApplyStatus.申请通过).ToList().Count : 0;
                        if (userCount >= item.UserCount)
                        {
                            riceDateStatus = (int)EnumHelp.RiceDateStatus.组团成功;
                        }
                        else
                        {
                            riceDateStatus = (int)EnumHelp.RiceDateStatus.已过期;
                        }
                    }
                    data.Add(new RiceDateDetailDTO()
                    {
                        BusinessName = item.BusinessName,
                        JoinCount = item.RiceDateUserList == null ? 0 : item.RiceDateUserList.Count,
                        Address = item.Address,
                        BeginDate = item.BeginDate,
                        UseCount = item.UserCount,
                        Zone = item.Zone,
                        Sex = item.Sex,
                        Age = item.Age,
                        Taste = item.Taste,
                        RiceDateStatu = riceDateStatus
                    });
                }
            }
            result.total_count = totalCount;
            result.data = data;
            return result;
        }
    }
}
