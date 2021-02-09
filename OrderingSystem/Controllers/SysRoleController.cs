using Exam.Common;
using OrderingSystem.Admin.Models;
using OrderingSystem.Controllers;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using System;
using System.Linq;
using System.Web.Mvc;

namespace OrderingSystem.Admin.Controllers
{
    /// <summary>
    /// 用户分组
    /// </summary>
    public class SysRoleController : BaseController
    {
        // GET: SysRole
        private readonly ISysRoleService _SysRoleService;
        public SysRoleController(ISysRoleService SysRoleService)
        {
            _SysRoleService = SysRoleService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="_SysRoleVM"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(SysRoleVM _SysRoleVM, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _SysRoleService.GetManagerList(_SysRoleVM.QueryName, pageIndex, pageSize, out totalCount);
            var paging = new Paging<SysRole>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            _SysRoleVM.Paging = paging;
            return View(_SysRoleVM);
        }
      
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="_SysRoleVM"></param>
        /// <returns></returns>
        public ActionResult Edit(SysRoleVM _SysRoleVM)
        {
            _SysRoleVM.SysRole = _SysRoleService.GetById(_SysRoleVM.Id) ?? new SysRole(); 
            return View(_SysRoleVM);
        }
        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(SysRole model)
        {
            try
            {
                if (model.SysRoleId > 0)
                {
                    var entity = _SysRoleService.GetById(model.SysRoleId);
                    //修改  
                    entity.EditTime = DateTime.Now;
                    entity.Name = model.Name; 
                    _SysRoleService.Update(entity);
                }
                else
                {
                    //if (_SysRoleService.IsExistName(model.Name))
                    //    return Json(new { Status = Successed.Repeat }, JsonRequestBehavior.AllowGet);
                    //添加
                    model.Status = (int)EnumHelp.EnabledEnum.有效;
                    model.CreateTime = DateTime.Now;
                    model.EditTime = DateTime.Now;

                    _SysRoleService.Insert(model);
                }
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
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
                var entity = _SysRoleService.GetById(id);

                if (entity == null)
                {
                    return Json(new { Status = Successed.Empty }, JsonRequestBehavior.AllowGet);
                }

                _SysRoleService.Delete(entity);

                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
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
                var entity = _SysRoleService.GetById(id);

                if (entity == null)
                {
                    return Json(new { Status = Successed.Empty }, JsonRequestBehavior.AllowGet);
                }
                entity.Status = (int)isEnabled;
                _SysRoleService.Update(entity);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}