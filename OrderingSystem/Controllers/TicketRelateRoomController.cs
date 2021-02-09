
using Exam.Common;
using OrderingSystem.Admin.Common;
using OrderingSystem.Admin.Models;
using OrderingSystem.Controllers;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System;
using System.Linq;
using System.Web.Mvc;
using static OrderingSystem.Domain.EnumHelp;

namespace OrderingSystem.Admin.Controllers
{
    public class TicketRelateRoomController : BaseController
    {
        // GET: TicketRelateRoom
        private readonly ITicketRelateRoomService _TicketRelateRoomService;
        private readonly IBusinessInfoService _businessService;
        private readonly IRoomService _roomService;


        public TicketRelateRoomController(ITicketRelateRoomService TicketRelateRoomService,
            IBusinessInfoService businessService, IRoomService roomService)
        {
            _TicketRelateRoomService = TicketRelateRoomService;
            _businessService = businessService;
            _roomService = roomService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pn">分页下标</param>
        /// <returns></returns>
        public ActionResult List(TicketRelateRoomVM vm, int pn = 1)
        {

            int total = 0,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            //分页查询
            var list = _TicketRelateRoomService.GetManagerList(vm.Id, pageIndex, pageSize, out total);
            var paging = new Paging<TicketRelateRoom>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = total,
                Index = pn,
            };
            vm.Paging = paging;

            return View(vm);
        }



        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(TicketRelateRoomVM vm)
        {
            vm.TicketRelateRoom = _TicketRelateRoomService.GetById(vm.Id) ?? new TicketRelateRoom();

            if (vm.TicketRelateRoom.TicketRelateRoomId == 0)
            {
                vm.TicketRelateRoom.TicketId = vm.TicketId;
            }

            vm.BusinessInfoList = _businessService.GetHotelList();

            if (vm.TicketRelateRoom.BusinessInfoId > 0)
            { 
                vm.RoomList = _roomService.GetListByBusinessId(vm.TicketRelateRoom.BusinessInfoId);
            }

            return View(vm);
        }

        /// <summary>
        ///  新增，编辑操作
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Edit(TicketRelateRoom model, string Business_Ids)
        {
            try
            {

                if (model.TicketRelateRoomId > 0)
                {
                    var entity = _TicketRelateRoomService.GetById(model.TicketRelateRoomId);
                    //修改
                    entity.RoomId = model.RoomId;
                    entity.BusinessInfoId = model.BusinessInfoId;
                    entity.Count = model.Count;

                    //执行更新
                    _TicketRelateRoomService.Update(entity);

                }
                else
                {
                    //新增   
                    _TicketRelateRoomService.Insert(model);

                }
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error, Info = Successed.Error.ToString() }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(int id = 0)
        {
            try
            {
                var entity = _TicketRelateRoomService.GetById(id);

                _TicketRelateRoomService.Delete(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok, Info = Successed.Ok.ToString() }, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 根据商家ID获取房间列表
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetRoomList(int bid = 0)
        {
            var list = _roomService.GetListByBusinessId(bid);
            list = (from p in list
                    select new Room
                    {
                        RoomId = p.RoomId,
                        Name = p.Name
                    }).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}