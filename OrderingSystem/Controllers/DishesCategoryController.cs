
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
    public class DishesCategoryController : BaseController
    {
        // GET: DishesCategory
        private readonly IDishesCategoryService _dishesCategoryService;


        public DishesCategoryController(IDishesCategoryService dishesCategoryService)
        {
            _dishesCategoryService = dishesCategoryService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pn">分页下标</param>
        /// <returns></returns>
        public ActionResult List(DishesCategoryVM vm, int pn = 1)
        {
            int totalCount,
         pageIndex = pn,
         pageSize = PagingConfig.PAGE_SIZE;

            if (Loginer.RoleId == (int)RoleTypeEnum.商家)
            {
                vm.BusinessInfoId = int.Parse(Loginer.BusinessId);
            }

            var list = _dishesCategoryService.GetManagerList(vm.QueryName, vm.BusinessInfoId, pageIndex, pageSize, out totalCount);
            var paging = new Paging<DishesCategory>()
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
        public ActionResult Edit(DishesCategoryVM vm)
        {
            vm.DishesCategory = _dishesCategoryService.GetById(vm.Id) ?? new DishesCategory();
            if (vm.Id== 0)
            {
                vm.DishesCategory.BusinessInfoId = int.Parse(Loginer.BusinessId);
            }
            return View(vm);
        }

        /// <summary>
        ///  新增，编辑操作
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Edit(DishesCategory model)
        {
            try
            {
                if (model.DishesCategoryId > 0)
                {
                    var entity = _dishesCategoryService.GetById(model.DishesCategoryId);
                    //修改
                    entity.Name = model.Name;

                    //执行更新
                    _dishesCategoryService.Update(entity);

                }
                else
                {
                    //新增  
                    model.Name = model.Name;
                    model.CreateTime = DateTime.Now;
                    _dishesCategoryService.Insert(model);
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
                var entity = _dishesCategoryService.GetById(id);

                _dishesCategoryService.Delete(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok, Info = Successed.Ok.ToString() }, JsonRequestBehavior.AllowGet);

        }

    }
}