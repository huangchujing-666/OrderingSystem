
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
    public class RoomController : BaseController
    {
        // GET: Room
        private readonly IRoomService _RoomService;
        private readonly IBusinessInfoService _businessService;
        private readonly IBaseImageService _baseImgInfoService;


        public RoomController(IRoomService RoomService,
            IBusinessInfoService businessService, IBaseImageService baseImgInfoService)
        {
            _RoomService = RoomService;
            _businessService = businessService;
            _baseImgInfoService = baseImgInfoService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pn">分页下标</param>
        /// <returns></returns>
        public ActionResult List(RoomVM vm, int pn = 1)
        {

            int total = 0,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            //分页查询
            var list = _RoomService.GetManagerList(vm.QueryName, vm.QueryBusinessmanName, pageIndex, pageSize, out total, int.Parse(Loginer.BusinessId));
            var paging = new Paging<Room>()
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
        public ActionResult Edit(RoomVM vm)
        {
            vm.Room = _RoomService.GetById(vm.Id) ?? new Room();
            //获取餐厅列表
            int tcount = 0;
            vm.BusinessList = _businessService.GetListByType((int)BusinessTypeEnum.酒店, 1, 1000, out tcount);

            return View(vm);
        }

        /// <summary>
        ///  新增，编辑操作
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Edit(Room model, string Business_Ids)
        {
            try
            {
                if (model.RoomId > 0)
                {
                    var entity = _RoomService.GetById(model.RoomId);
                    //修改
                    entity.Name = model.Name;
                    entity.OrignPrice = model.OrignPrice;
                    entity.RealPrice = model.RealPrice; 
                    entity.BusinessInfoId = model.BusinessInfoId; 
                    entity.Notice = model.Notice;
                    entity.AirConditioner = model.AirConditioner;
                    entity.Area = model.Area;
                    entity.Bathroom = model.Bathroom;
                    entity.Bed = model.Bed;
                    entity.BedType = model.BedType; 
                    entity.Breakfast = model.Breakfast;
                    entity.Floor = model.Floor;
                    entity.Internet = model.Internet;
                    entity.Remain = model.Remain;
                    entity.Rules = model.Rules;
                    entity.Window = model.Window;

                    entity.EditTime = DateTime.Now;
                    entity.CreateTime = DateTime.Now;

                    //执行更新
                    _RoomService.Update(entity);

                }
                else
                {
                    //新增 
                    model.Status = (int)EnabledEnum.有效;
                    model.IsDelete = (int)IsDeleteEnum.有效; 
                    model.CreateTime = DateTime.Now;
                    model.EditTime = DateTime.Now;
                    _RoomService.Insert(model);

                }
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error, Info = Successed.Error.ToString() }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 状态修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateStatus(int id = 0, OrderingSystem.Domain.EnumHelp.EnabledEnum isEnabled = EnumHelp.EnabledEnum.有效)
        {
            try
            {
                var entity = _RoomService.GetById(id);
                entity.Status = (int)isEnabled;

                _RoomService.Update(entity);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
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
                var entity = _RoomService.GetById(id);
                entity.IsDelete = (int)IsDeleteEnum.已删除;

                _RoomService.Update(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok, Info = Successed.Ok.ToString() }, JsonRequestBehavior.AllowGet);

        }
         
    }
}