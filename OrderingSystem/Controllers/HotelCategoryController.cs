
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
    public class HotelCategoryController : BaseController
    {
        // GET: HotelCategory
        private readonly IHotelCategoryService _HotelCategoryService;


        public HotelCategoryController(IHotelCategoryService HotelCategoryService)
        {
            _HotelCategoryService = HotelCategoryService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pn">分页下标</param>
        /// <returns></returns>
        public ActionResult List(HotelCategoryVM vm, int pn = 1)
        {
            int totalCount,
         pageIndex = pn,
         pageSize = PagingConfig.PAGE_SIZE;

            if (Loginer.RoleId == (int)RoleTypeEnum.商家)
            {
                vm.BusinessInfoId = int.Parse(Loginer.BusinessId);
            }

            var list = _HotelCategoryService.GetManagerList(vm.QueryName, vm.BusinessInfoId, pageIndex, pageSize, out totalCount);
            var paging = new Paging<HotelCategory>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            vm.Paging = paging;
            return View(vm);
        }



        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(HotelCategoryVM vm)
        {
            vm.HotelCategory = _HotelCategoryService.GetById(vm.Id) ?? new HotelCategory();
             
            return View(vm);
        }

        /// <summary>
        ///  新增，编辑操作
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Edit(HotelCategory model)
        {
            try
            {
                if (model.HotelCategoryId > 0)
                {
                    var entity = _HotelCategoryService.GetById(model.HotelCategoryId);
                    //修改
                    entity.Name = model.Name;

                    //执行更新
                    _HotelCategoryService.Update(entity);

                }
                else
                {
                    //新增  
                    model.Name = model.Name;
                    model.CreateTime = DateTime.Now;
                    _HotelCategoryService.Insert(model);
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
                var entity = _HotelCategoryService.GetById(id);

                _HotelCategoryService.Delete(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok, Info = Successed.Ok.ToString() }, JsonRequestBehavior.AllowGet);

        }

    }
}