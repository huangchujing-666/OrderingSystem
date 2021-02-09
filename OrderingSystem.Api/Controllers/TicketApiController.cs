using OrderingSystem.Api.Models;
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
    public class TicketApiController : ApiController
    {
        private readonly ITicketService _ticketService = EngineContext.Current.Resolve<ITicketService>();
        //public TicketApiController(ITicketService ticketService)
        //{
        //    _ticketService = ticketService;
        //}

        /// <summary>
        /// 根据Id获取门票信息
        /// </summary>
        /// <param name="TicketId"></param>
        /// <param name="Business_Id"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<TicketDTO> GetTicketDetailById(int TicketId, int Business_Id)
        {
            var result = new ResponseModel<TicketDTO>();
            result.error_code = Result.SUCCESS;
            result.total_count = 0;
            if (TicketId <= 0 || Business_Id <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "参数有误";
                return result;
            }
            var ticketResult = _ticketService.GetById(TicketId);
            TicketDTO ticket = new TicketDTO();
            if (ticketResult != null && ticketResult.BusinessInfoId == Business_Id)
            {
                ticket.ticket_id = ticketResult.TicketId;
                ticket.name = ticketResult.Name;
                ticket.notice = ticketResult.Notice;
                ticket.orign_price = ticketResult.OrignPrice.ToString();
                ticket.real_price = ticketResult.RealPrice.ToString();
                ticket.remark = ticketResult.Remark;
                ticket.rules = ticketResult.Rules;
                ticket.special = ticketResult.Special;
                ticket.use_count = ticketResult.UseCount;
                ticket.bind_card = ticketResult.BindCard;
                 var relate_room_list = new List<RelateRoomDTO>();
                if (ticketResult.TicketRelateRoom != null && ticketResult.TicketRelateRoom.Count > 0)
                {
                    foreach (var item in ticketResult.TicketRelateRoom)
                    {
                        var room = item.Room;
                        if (room != null)
                        {
                            relate_room_list.Add(new RelateRoomDTO()
                            {
                                count = item.Count,
                                name = room.Name,
                                orign_price = room.OrignPrice.ToString(),
                                real_price = room.RealPrice.ToString(),
                                room_id = room.RoomId,
                                room_area = room.Area,
                                room_bed = room.Bed,
                                room_breakfast = room.Breakfast,
                                room_window = room.Window,
                                room_bed_type = room.BedType
                            });

                        }
                        if (room.BusinessInfo != null)
                        {
                            var business = room.BusinessInfo;
                            ticket.hotel_id = business.BusinessInfoId;
                            ticket.hotel_distance = business.Distance;
                            ticket.hotel_name = business.Name;
                            ticket.hotel_rank = business.Grade.ToString();
                            ticket.hotel_img_path = business.BaseImage == null ? "" : business.BaseImage.Source + business.BaseImage.Path;
                        }
                    }
                }
                ticket.relate_room_list = relate_room_list;
                var relate_ticket_list = new List<RelateTicketDTO>();
                if (ticketResult.TicketRelateTicket != null && ticketResult.TicketRelateTicket.Count > 0)
                {
                    foreach (var item in ticketResult.TicketRelateTicket)
                    {
                        var relateticket = _ticketService.GetById(item.RelateTicketId);
                        if (relateticket != null)
                        {
                            relate_ticket_list.Add(new RelateTicketDTO()
                            {
                                count = item.Count,
                                name = relateticket.Name,
                                orign_price = relateticket.OrignPrice.ToString(),
                                real_price = relateticket.RealPrice.ToString(),
                                ticket_id = relateticket.TicketId
                            });
                        }
                    }
                }
                ticket.relate_ticket_list = relate_ticket_list;
            }
            result.data = ticket;
            return result;
        }
    }
}
