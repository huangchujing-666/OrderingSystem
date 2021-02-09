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
    public class SysRoleService: ISysRoleService
    {
        /// <summary>
        /// The SysRole biz
        /// </summary>
        private ISysRoleBusiness _SysRoleBiz;

        public SysRoleService(ISysRoleBusiness SysRoleBiz)
        {
            _SysRoleBiz = SysRoleBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysRole GetById(int id)
        {
            return this._SysRoleBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SysRole Insert(SysRole model)
        {
            return this._SysRoleBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(SysRole model)
        {
            this._SysRoleBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(SysRole model)
        {
            this._SysRoleBiz.Delete(model);
        } 
        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<SysRole> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._SysRoleBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public List<SysRole> GetAll()
        {
            return this._SysRoleBiz.GetAll();
        }
    }
}
