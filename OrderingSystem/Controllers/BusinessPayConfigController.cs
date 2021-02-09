
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
    public class BusinessPayConfigController : BaseController
    {
        // GET: BusinessPayConfig
        private readonly IBusinessPayConfigService _BusinessPayConfigService;
        private readonly IBusinessInfoService _businessService; 


        public BusinessPayConfigController(IBusinessPayConfigService BusinessPayConfigService,
            IBusinessInfoService businessService)
        {
            _BusinessPayConfigService = BusinessPayConfigService;
            _businessService = businessService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pn">分页下标</param>
        /// <returns></returns>
        public ActionResult List(BusinessPayConfigVM vm, int pn = 1)
        {
            int outTotal = 0;

            //查询
            var data = _BusinessPayConfigService.GetManagerList(vm.Id,1,100, out outTotal);

            vm.BusinessPayConfigList = data;

            return View(vm);
        }



        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(BusinessPayConfigVM vm)
        {
            vm.BusinessPayConfig = _BusinessPayConfigService.GetById(vm.Id) ?? new BusinessPayConfig();

            if (vm.Id==0)
            {
                vm.BusinessPayConfig.BusinessInfoId = int.Parse(Loginer.BusinessId);
            } 
              
            return View(vm);
        }

        /// <summary>
        ///  新增，编辑操作
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Edit(BusinessPayConfig model)
        {
            try
            {
                if (model.BusinessPayConfigId > 0)
                {
                    var entity = _BusinessPayConfigService.GetById(model.BusinessPayConfigId);
                    //修改
                    entity.Name = model.Name;

                    //执行更新
                    _BusinessPayConfigService.Update(entity);

                }
                else
                {
                    //新增  
                    model.CreateTime = DateTime.Now;

                    _BusinessPayConfigService.Insert(model);
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
                var entity = _BusinessPayConfigService.GetById(id);

                _BusinessPayConfigService.Delete(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok, Info = Successed.Ok.ToString() }, JsonRequestBehavior.AllowGet);

        }

    }
}