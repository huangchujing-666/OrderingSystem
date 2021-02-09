using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface ISysRoleMenuService
    {
        SysRoleMenu GetById(int id);

        SysRoleMenu Insert(SysRoleMenu model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(SysRoleMenu model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(SysRoleMenu model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<SysRoleMenu> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 删除实体( roleId )
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        void DeleteByRoleId(int roleId);

        /// <summary>
        /// 获取所有
        /// </summary> 
        /// <returns></returns>
        List<SysRoleMenu> GetAll();
    }
}
