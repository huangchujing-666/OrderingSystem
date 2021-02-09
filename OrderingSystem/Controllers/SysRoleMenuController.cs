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
    /// 角色菜单
    /// </summary>
    public class SysRoleMenuController : BaseController
    {
        // GET: SysRoleMenu
        private readonly ISysRoleMenuService _SysRoleMenuService;
        private readonly ISysMenuService _sysMenuService;
        public SysRoleMenuController(ISysRoleMenuService SysRoleMenuService, ISysMenuService sysMenuService)
        {
            _SysRoleMenuService = SysRoleMenuService;
            _sysMenuService = sysMenuService;
        }

        public ActionResult Edit(SysRoleMenuVM _sysRoleMenuVM)
        {
            _sysRoleMenuVM.SysMenus = _sysMenuService.GetAll().Where(p => p.Status == (int)EnumHelp.EnabledEnum.有效).ToList();
            return View(_sysRoleMenuVM);
        }
        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(int roleId = 0, int[] menuId = null)
        {
            try
            { 
                _SysRoleMenuService.DeleteByRoleId(roleId);
                for (int i = 0; i < menuId.Length; i++)
                {
                    var model = new SysRoleMenu()
                    {
                        SysRoleId = roleId,
                        SysMenuId = menuId[i],
                        CreateTime = DateTime.Now,
                        EditTime = DateTime.Now,
                    };
                    _SysRoleMenuService.Insert(model);
                }
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 获取分组相关菜单
        /// </summary>
        /// <param name="groupId">分组Id</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetRoleMenus(int roleId = 0)
        {
            var list = _SysRoleMenuService.GetAll().Where(p=>p.SysRoleId == roleId).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}