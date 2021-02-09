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
    public class SysMenuService: ISysMenuService
    {
        /// <summary>
        /// The SysMenu biz
        /// </summary>
        private ISysMenuBusiness _SysMenuBiz;

        public SysMenuService(ISysMenuBusiness SysMenuBiz)
        {
            _SysMenuBiz = SysMenuBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysMenu GetById(int id)
        {
            return this._SysMenuBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SysMenu Insert(SysMenu model)
        {
            return this._SysMenuBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(SysMenu model)
        {
            this._SysMenuBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(SysMenu model)
        {
            this._SysMenuBiz.Delete(model);
        } 

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<SysMenu> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._SysMenuBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 获取所有
        /// </summary> 
        /// <returns></returns>
        public List<SysMenu> GetAll()
        {
            return this._SysMenuBiz.GetAll();
        }
    }
}
