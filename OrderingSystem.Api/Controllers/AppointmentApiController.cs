using OrderingSystem.Api.Models;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
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
    /// <summary>
    /// 用户预约到店
    /// </summary>
    public class AppointmentApiController : ApiController
    {
        private readonly IAppointmentService _appointmentService = EngineContext.Current.Resolve<IAppointmentService>();
        private readonly IUserService _userService = EngineContext.Current.Resolve<IUserService>();
        private readonly IBusinessInfoService _businessInfoService = EngineContext.Current.Resolve<IBusinessInfoService>();

        /// <summary>
        /// 提交预约信息
        /// </summary>
        /// <param name="postAppointment"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<int> PostAppointment(PostAppointment postAppointment)
        {
            var result = new ResponseModel<int>();
            result.error_code = Result.SUCCESS;
            if (string.IsNullOrWhiteSpace(postAppointment.value_ids))
            {
                throw new Exception("衣品id不能为空");
            }
            if (postAppointment.module != (int)EnumHelp.BusinessTypeEnum.食)
            {
                throw new Exception("模块id有误");
            }
            var user = _userService.GetById(postAppointment.user_id);
            if (user == null || user.IsDelete == (int)EnumHelp.IsDeleteEnum.已删除 || user.Status == (int)EnumHelp.EnabledEnum.无效)
            {
                throw new Exception("用户id有误,或用户已删除");
            }
            var business = _businessInfoService.GetById(postAppointment.business_id);
            if (business == null || business.IsDelete == (int)EnumHelp.IsDeleteEnum.已删除 || business.Status == (int)EnumHelp.EnabledEnum.无效)
            {
                throw new Exception("商家id有误,或商家已删除");
            }
            DateTime appointTime;
            if (postAppointment.appointment_time == null)
            {
                throw new Exception("预约时间不能为空");
            }
            else if (!DateTime.TryParse(postAppointment.ToString(), out appointTime))
            {
                throw new Exception("预约时间有误");
            }
            if (string.IsNullOrWhiteSpace(postAppointment.user_name))
            {
                throw new Exception("用户名不能为空");
            }

            if (!CheckInputHelper.RegexPhone(postAppointment.phone_no))
            {
                throw new Exception("手机号码有误");
            }
            DateTime createTime = System.DateTime.Now;
            var goodsIds = postAppointment.value_ids.Split(',').ToArray();
            int[] output = Array.ConvertAll<string, int>(goodsIds, delegate (string s) { return int.Parse(s); });
            var goods = business.BusinessGoodsList.Where(c => c.IsDelete == (int)EnumHelp.IsDeleteEnum.有效 && c.Status == (int)EnumHelp.IsDeleteEnum.有效).ToList();
            if (goods != null && goods.Count > 0)
            {
                var searchGoods = goods.Where(c => output.Contains(c.GoodsId)).ToList();
                if (searchGoods != null && searchGoods.Count == output.Length)
                {
                    var insertResult = _appointmentService.Insert(new Appointment()
                    {
                        AppointmentTime = appointTime,
                        BusinessInfoId = postAppointment.business_id,
                        UserId = postAppointment.user_id,
                        CreateTime = createTime,
                        DenyReason = "",
                        EditTime = createTime,
                        ModuleId = postAppointment.module,
                        Status = (int)EnumHelp.IsDeleteEnum.有效,
                        IsDelete = (int)EnumHelp.IsDeleteEnum.有效,
                        Remark = postAppointment.remark,
                        ValueIds = postAppointment.value_ids,
                        UserName = postAppointment.user_name,
                        Phone = postAppointment.phone_no
                    });
                    if (insertResult.AppointmentId <= 0)
                    {
                        throw new Exception("预约失败");
                    }
                    result.data = insertResult.AppointmentId;
                }
                else
                {
                    throw new Exception("衣品id有误");
                }
            }
            else
            {
                throw new Exception("商家无衣品可以预约");
            }
            return result;
        }

        /// <summary>
        /// 获取预约到店详情
        /// </summary>
        /// <param name="Appointment_Id"></param>
        /// <returns></returns>
        public ResponseModel<AppointmentDTO> GetAppointmentById(int Appointment_Id)
        {
            var result = new ResponseModel<AppointmentDTO>();
            result.error_code = Result.SUCCESS;
            var appointment = _appointmentService.GetById(Appointment_Id);
            if (appointment == null)
            {
                throw new Exception("该预约Id不存在");
            }
            var goodsIds = appointment.ValueIds.Split(',').ToArray();
            int[] output = Array.ConvertAll<string, int>(goodsIds, delegate (string s) { return int.Parse(s); });
            var goodsList = appointment.BusinessInfo.BusinessGoodsList.Where(c => output.Contains(c.GoodsId)).ToList();
            List<GoodsDTO> goodsListResult = new List<GoodsDTO>(); ;
            if (goodsList != null & goodsList.Count > 0)
            {
                foreach (var item in goodsList)
                {
                    goodsListResult.Add(
                           new GoodsDTO()
                           {
                               base_image_id = item.BaseImageId,
                               descript = item.Descript,
                               goods_id = item.GoodsId,
                               name = item.Name,
                               orign_price = item.OrignPrice.ToString(),
                               real_price = item.RealPrice.ToString(),
                               path = item.BaseImage == null ? "" : item.BaseImage.Source + item.BaseImage.Path,
                               image_list = GoodsImageToDTO(item.GoodsImageList)
                           }
                        );
                }
            }
            var data = new AppointmentDTO()
            {
                appointment_id = appointment.AppointmentId,
                business_name = appointment.BusinessInfo.Name,
                business_id = appointment.BusinessInfoId,
                deny_reason = appointment.DenyReason,
                phone_no = appointment.Phone,
                remark = appointment.Remark,
                module = appointment.ModuleId,
                user_id = appointment.UserId,
                user_name = appointment.UserName,
                appointment_time = appointment.AppointmentTime.ToString("yyyy-MM-dd"),
                GoodsList = goodsListResult
            };
            result.data = data;
            return result;
        }

        /// <summary>
        /// 获取指定状态下/全部 的用户预约
        /// </summary>
        /// <param name="User_Id"></param>
        /// <param name="StatusId"></param>
        /// <returns></returns>
        public ResponseModel<List<AppointmentDTO>> GetAppointmentListByUserId(int User_Id, int StatusId = 0)
        {
            var result = new ResponseModel<List<AppointmentDTO>>();
            result.error_code = Result.SUCCESS;
            var appointmentList = _appointmentService.GetAppointmentListByUserId(User_Id, StatusId);
            var data = new List<AppointmentDTO>();
            if (appointmentList != null && appointmentList.Count > 0)
            {
                foreach (var item in appointmentList)
                {
                    RiceDateApplyStatus status;
                    Enum.TryParse<RiceDateApplyStatus>(item.Status.ToString(), out status);
                    data.Add(new AppointmentDTO()
                    {
                        business_name = item.BusinessInfo.Name,
                        business_id = item.BusinessInfoId,
                        appointment_time = item.AppointmentTime.ToString("yyyy-MM-dd"),
                        apply_status = status.ToString()
                    });
                }
            }
            result.data = data;
            return result;
        }

        /// <summary>
        /// 删除预约
        /// </summary>
        /// <param name="postDeleteAppointment"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel DeleteAppointment(PostDeleteAppointment postDeleteAppointment)
        {
            var result = new ResponseModel();
            result.error_code = Result.SUCCESS;
            var appoinemtnt = _appointmentService.GetById(postDeleteAppointment.AppointmentId);
            if (appoinemtnt == null)
            {
                throw new Exception("该预约不存在");
            }
            if (appoinemtnt.UserId != postDeleteAppointment.UserId)
            {
                throw new Exception("该用户无权限删除预约");
            }
            appoinemtnt.IsDelete = (int)EnumHelp.IsDeleteEnum.已删除;
            appoinemtnt.EditTime = System.DateTime.Now;
            _appointmentService.Update(appoinemtnt);
            return result;
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
    }
}
