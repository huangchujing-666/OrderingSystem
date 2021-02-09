using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Business;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Service
{
    public class SysRoleMenuService: ISysRoleMenuService
    {
        /// <summary>
        /// The SysRoleMenu biz
        /// </summary>
        private ISysRoleMenuBusiness _SysRoleMenuBiz;

        public SysRoleMenuService(ISysRoleMenuBusiness SysRoleMenuBiz)
        {
            _SysRoleMenuBiz = SysRoleMenuBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysRoleMenu GetById(int id)
        {
            return this._SysRoleMenuBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SysRoleMenu Insert(SysRoleMenu model)
        {
            return this._SysRoleMenuBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(SysRoleMenu model)
        {
            this._SysRoleMenuBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(SysRoleMenu model)
        {
            this._SysRoleMenuBiz.Delete(model);
        } 
        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<SysRoleMenu> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._SysRoleMenuBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }
        /// <summary>
        /// 删除实体( roleId )
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public void DeleteByRoleId(int roleId)
        {
            this._SysRoleMenuBiz.DeleteByRoleId(roleId);
        }
        /// <summary>
        /// 获取所有
        /// </summary> 
        /// <returns></returns>
        public List<SysRoleMenu> GetAll()
        {
            return this._SysRoleMenuBiz.GetAll();
        }
    }
}
