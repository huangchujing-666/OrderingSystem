
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
    public class BusinessGroupController : BaseController
    {
        // GET: BusinessGroup
        private readonly IBusinessGroupService _BusinessGroupService;


        public BusinessGroupController(IBusinessGroupService BusinessGroupService)
        {
            _BusinessGroupService = BusinessGroupService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="pn">分页下标</param>
        /// <returns></returns>
        public ActionResult List(BusinessGroupVM vm, int pn = 1)
        {
            int totalCount,
         pageIndex = pn,
         pageSize = PagingConfig.PAGE_SIZE;

            if (Loginer.RoleId == (int)RoleTypeEnum.商家)
            {
                vm.BusinessInfoId = int.Parse(Loginer.BusinessId);
            }

            var list = _BusinessGroupService.GetManagerList(vm.QueryName,pageIndex, pageSize, out totalCount);
            var paging = new Paging<BusinessGroup>()
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
        public ActionResult Edit(BusinessGroupVM vm)
        {
            vm.BusinessGroup = _BusinessGroupService.GetById(vm.Id) ?? new BusinessGroup();
            if (vm.BusinessGroup.BaseImage ==null)
            {
                vm.BusinessGroup.BaseImage = new BaseImage();
            }
            if (vm.Id== 0)
            {
                //vm.BusinessGroup.BusinessInfoId = int.Parse(Loginer.BusinessId);
            }
            return View(vm);
        }

        /// <summary>
        ///  新增，编辑操作
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        [HttpPost]
        public JsonResult Edit(BusinessGroup model)
        {
            try
            {
                if (model.BusinessGroupId > 0)
                {
                    var entity = _BusinessGroupService.GetById(model.BusinessGroupId);
                    //修改
                    entity.Name = model.Name;
                    entity.BaseImageId = model.BaseImageId;
                    entity.BusinessTypeId = model.BusinessTypeId;
                    entity.Sort = model.Sort; 

                    entity.EditTime = DateTime.Now;
                    //执行更新
                    _BusinessGroupService.Update(entity);

                }
                else
                {
                    //新增   
                    model.CreateTime = DateTime.Now;
                    model.EditTime = DateTime.Now; 
                    _BusinessGroupService.Insert(model);
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
                var entity = _BusinessGroupService.GetById(id);

                _BusinessGroupService.Delete(entity);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { Status = Successed.Ok, Info = Successed.Ok.ToString() }, JsonRequestBehavior.AllowGet);

        }

    }
}