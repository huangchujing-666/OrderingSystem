using OrderingSystem.Api.Models;
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
    public class RoomApiController : ApiController
    {
        private readonly IRoomService _businessInfoService = EngineContext.Current.Resolve<IRoomService>();

        /// <summary>
        /// 根据房间Id获取房间信息
        /// </summary>
        /// <param name="Room_Id"></param>
        /// <param name="Business_Id"></param>
        /// <returns></returns>
        public ResponseModel<RoomDTO> GetRoomInfoById(int Room_Id, int Business_Id)
        {
            var result = new ResponseModel<RoomDTO>();
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            if (Room_Id <= 0 || Business_Id <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "参数有误";
                return result;
            }
            var roomResult = _businessInfoService.GetById(Room_Id);
            RoomDTO room = new RoomDTO();
            if (roomResult!=null&& roomResult.BusinessInfoId== Business_Id)
            {
                room.room_id = roomResult.RoomId;
                room.airConditioner = roomResult.AirConditioner;
                room.area = roomResult.Area;
                room.bathroom = roomResult.Bathroom;
                room.bed = roomResult.Bed;
                room.floor = roomResult.Floor;
                room.name = roomResult.Name;
                room.notice = roomResult.Notice;
                room.orignPrice = roomResult.OrignPrice.ToString();
                room.realPrice = roomResult.RealPrice.ToString();
                room.window = roomResult.Window;
                room.rules = roomResult.Rules;
                room.remain = roomResult.Remain.ToString();
                room.internet = roomResult.Internet;
                room.breakfast = roomResult.Breakfast;
                room.room_image_list = RoomImageToDTO(roomResult.RoomImageList);
                room.bed_type = roomResult.BedType;
            }
            result.data = room;
            return result;
        }

        private List<BaseImageDTO> RoomImageToDTO(List<RoomImage> list)
        {
            List<BaseImageDTO> result = null;
            if (list != null&& list.Count>0)
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
    }
}

