using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class SysRoleMenuBusiness:ISysRoleMenuBusiness
    {
        private IRepository<SysRoleMenu> _repoSysRoleMenu;

        public SysRoleMenuBusiness(
          IRepository<SysRoleMenu> repoSysRoleMenu
          )
        {
            _repoSysRoleMenu = repoSysRoleMenu;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysRoleMenu GetById(int id)
        {
            return this._repoSysRoleMenu.GetById(id);
        }

        public SysRoleMenu Insert(SysRoleMenu model)
        {
            return this._repoSysRoleMenu.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(SysRoleMenu model)
        {
            this._repoSysRoleMenu.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(SysRoleMenu model)
        {
            this._repoSysRoleMenu.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<SysRoleMenu> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<SysRoleMenu>();
        

            totalCount = this._repoSysRoleMenu.Table.Where(where).Count();
            return this._repoSysRoleMenu.Table.Where(where).OrderBy(p => p.SysRoleMenuId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 删除实体( roleId )
        /// </summary> 
        /// <returns></returns>
        public void DeleteByRoleId(int roleId)
        {
            var entities = _repoSysRoleMenu.Table.Where(p => p.SysRoleId == roleId);
            if (entities != null && entities.Count() > 0)
            {
                this._repoSysRoleMenu.Delete(entities);
            }
        }
        /// <summary>
        /// 获取所有
        /// </summary> 
        /// <returns></returns>
        public List<SysRoleMenu> GetAll()
        {
            return this._repoSysRoleMenu.Table.ToList();
        }
    }
}


