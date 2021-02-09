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

namespace OrderingSystem.Controllers
{
    public class UserController : BaseController
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: User
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="_userVM"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public ActionResult List(UserVM _userVM, int pn = 1)
        {
            int totalCount,
                pageIndex = pn,
                pageSize = PagingConfig.PAGE_SIZE;
            var list = _userService.GetManagerList(_userVM.QueryName, pageIndex, pageSize, out totalCount).Where(c=>c.IsDelete==0).ToList();
            var paging = new Paging<User>()
            {
                Items = list,
                Size = PagingConfig.PAGE_SIZE,
                Total = totalCount,
                Index = pn,
            };
            _userVM.Paging = paging;
            return View(_userVM);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="_userVM"></param>
        /// <returns></returns>
        public ActionResult Edit(UserVM _userVM)
        {
            _userVM.User = _userService.GetById(_userVM.Id) ?? new User();
            _userVM.ImgInfo = _userVM.User.BaseImage ?? new BaseImage();
            return View(_userVM);
        }
        /// <summary>
        /// 添加、修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(User model)
        {
            try
            {
                if (model.UserId > 0)
                {
                    var entity = _userService.GetById(model.UserId);
                    //修改  
                    entity.EditTime = DateTime.Now;
                    entity.NickName = model.NickName;
                    entity.PhoneNo = model.PhoneNo;
                    entity.BaseImageId= model.BaseImageId;
                    _userService.Update(entity);
                }
                else
                {
                    //添加
                    model.EditTime = DateTime.Now;
                    model.CreateTime = DateTime.Now;
                    model.IsDelete = 0;
                    model.Status = 1;
                    _userService.Insert(model);
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
                var entity = _userService.GetById(id);
                entity.IsDelete = (int)IsDeleteEnum.已删除;

                _userService.Update(entity);
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
                var entity = _userService.GetById(id);
                entity.Status = (int)isEnabled;

                _userService.Update(entity);
                return Json(new { Status = Successed.Ok }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Status = Successed.Error }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}