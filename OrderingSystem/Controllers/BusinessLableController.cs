
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
    public class BusinessLableController : BaseController
    {
        // GET: BusinessLable
        private readonly IBusinessLableService _businessLableService;
        private readonly IBusinessInfoService _businessService; 


        public BusinessLableController(IBusinessLableService businessLableService,
            IBusinessInfoService businessService)
        {
            _businessLableService = businessLableService;
            _businessService = businessService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pn">分页下标</param>
        /// <returns></returns>
        public ActionResult List(BusinessLableVM vm, int pn = 1)
        {
            int outTotal = 0;

            //查询
            var data = _businessLableService.GetManagerList(vm.Id,1,100, out outTotal);

            vm.BusinessLableList = data;

            return View(vm);
        }



        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(BusinessLableVM vm)
        {
            vm.BusinessLable = _businessLableService.GetById(vm.Id) ?? new BusinessLable();

            if (vm.Id==0)
            {
                vm.BusinessLable.BusinessInfoId = vm.BusinessInfoId;
            } 
              
            return View(vm);
        }

        /// <summary>
        ///  新增，编辑操作
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Edit(BusinessLable model)
        {
            try
            {
                if (model.BusinessLableId > 0)
                {
                    var entity = _businessLableService.GetById(model.BusinessLableId);
                    //修改
                    entity.Name = model.Name;

                    //执行更新
                    _businessLableService.Update(entity);

                }
                else
                {
                    //新增  
                    model.Name = model.Name;

                    _businessLableService.Insert(model);
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
                var entity = _businessLableService.GetById(id);

                _businessLableService.Delete(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok, Info = Successed.Ok.ToString() }, JsonRequestBehavior.AllowGet);

        }

    }
}