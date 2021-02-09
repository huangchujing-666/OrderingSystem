using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Exam.Common;
using OrderingSystem.Admin.Models;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using static OrderingSystem.Domain.EnumHelp;
using OrderingSystem.Admin.Common;

namespace OrderingSystem.Controllers
{
    public class SysAccountController : Controller
    {

        private readonly ISysAccountService _SysAccountService;
        private readonly ISysRoleService _SysRoleService;

        public SysAccountController(ISysAccountService SysAccountService, ISysRoleService SysRoleService)
        {
            _SysAccountService = SysAccountService;
            _SysRoleService = SysRoleService;
        }

        // GET: SysAccount
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="_SysAccountVM"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(SysAccountVM _SysAccountVM, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _SysAccountService.GetManagerList(_SysAccountVM.QueryName, pageIndex, pageSize, out totalCount).ToList();
            var paging = new Paging<SysAccount>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            _SysAccountVM.Paging = paging;
            return View(_SysAccountVM);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="_SysAccountVM"></param>
        /// <returns></returns>
        public ActionResult Edit(SysAccountVM _SysAccountVM)
        {
            _SysAccountVM.SysAccount = _SysAccountService.GetById(_SysAccountVM.Id) ?? new SysAccount();
            _SysAccountVM.ImgInfo = _SysAccountVM.SysAccount.BaseImage ?? new BaseImage();

            _SysAccountVM.SysRoles = _SysRoleService.GetAll();
            return View(_SysAccountVM);
        }
        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(SysAccount model)
        {
            try
            {
                if (model.SysAccountId > 0)
                {
                    var entity = _SysAccountService.GetById(model.SysAccountId);
                    //修改  
                    entity.EditTime = DateTime.Now;
                    entity.NickName = model.NickName;
                    entity.MobilePhone = model.MobilePhone;
                    entity.BaseImageId = model.BaseImageId;
                    entity.Remarks = model.Remarks;
                    _SysAccountService.Update(entity);
                }
                else
                {
                    //添加
                    model.PassWord = MD5Util.GetMD5_32(model.PassWord);
                    model.EditTime = DateTime.Now;
                    model.CreateTime = DateTime.Now;
                    model.IsDelete = (int)IsDeleteEnum.有效;
                    model.Status = (int)EnabledEnum.有效;
                    _SysAccountService.Insert(model);
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
                var entity = _SysAccountService.GetById(id);
                entity.IsDelete = (int)IsDeleteEnum.已删除;

                _SysAccountService.Update(entity);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
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
                var entity = _SysAccountService.GetById(id);
                entity.Status = (int)isEnabled;

                _SysAccountService.Update(entity);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult EditPwd()
        {
            return View();
        }

        /// <summary>
        /// 密码修改
        /// </summary>
        /// <param name="accountId">账号id</param>
        /// <param name="account">账号</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EditPwd(int accountId, string account, string oldPassword, string newPassword)
        {
            try
            { 
                //获取用户
                var _user = _SysAccountService.Login(account, MD5Util.GetMD5_32(oldPassword));
                if (_user == null)
                {
                    //获取用户失败
                    return Json(new { Status = Successed.Empty }, JsonRequestBehavior.AllowGet);
                }
                _user.PassWord = MD5Util.GetMD5_32(newPassword);
                //修改密码
                _SysAccountService.Update(_user);

                //修改密码成功，清除缓存
                Loginer.DelAccountCache();
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}