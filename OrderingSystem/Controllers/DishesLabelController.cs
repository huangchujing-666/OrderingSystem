
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
    public class DishesLabelController : BaseController
    {
        // GET: DishesLabel
        private readonly IDishesService _dishesService;
        private readonly IBusinessInfoService _businessService;
        private readonly IBaseImageService _baseImgInfoService;
        private readonly IDishesLableService _dishesLabelService;
        private readonly IDishesRelateLableService _dishesRelateLabelService;


        public DishesLabelController(IDishesService dishesService,
            IBusinessInfoService businessService, IBaseImageService baseImgInfoService,
            IDishesLableService dishesLabelService,IDishesRelateLableService dishesRelateLabelService)
        {
            _dishesService = dishesService;
            _businessService = businessService;
            _baseImgInfoService = baseImgInfoService;
            _dishesLabelService = dishesLabelService;
            _dishesRelateLabelService = dishesRelateLabelService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pn">分页下标</param>
        /// <returns></returns>
        public ActionResult List(DishesLabelVM vm, int pn = 1)
        {
            //查询
            var data = _dishesService.GetById(vm.Id);

            vm.BusinessInfoId = data.BusinessInfoId;

            vm.DishesLabelList = data.DishesLableList;

            return View(vm);
        }



        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(DishesLabelVM vm)
        {
            vm.DishesLabel = _dishesLabelService.GetById(vm.Id) ?? new DishesLable();

            if (vm.Id==0)
            {
                var data = _dishesService.GetById(vm.DishesId);

                vm.BusinessInfoId = data.BusinessInfoId;
            }
            else
            {
                vm.DishesId = vm.DishesLabel.DishesId;
                vm.BusinessInfoId = vm.DishesLabel.BusinessInfoId;
            }
             
            //获取默认可选标签列表
            vm.DishesLabelList = _dishesLabelService.GetAll().Where(p => p.BusinessInfoId == 0 || p.BusinessInfoId == vm.BusinessInfoId).Distinct().ToList();

            return View(vm);
        }

        /// <summary>
        ///  新增，编辑操作
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Edit(DishesLable model)
        {
            try
            {
                if (model.DishesLableId > 0)
                {
                    var entity = _dishesLabelService.GetById(model.DishesLableId);
                    //修改
                    entity.Name = model.Name;

                    //执行更新
                    _dishesLabelService.Update(entity);

                }
                else
                {
                    //新增  
                    model.Name = model.Name;

                    _dishesLabelService.Insert(model);
                }
            }
            catch (Exception)
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
                var entity = _dishesLabelService.GetById(id);
                
                _dishesLabelService.Delete(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok, Info = Successed.Ok.ToString() }, JsonRequestBehavior.AllowGet);

        }

    }
}