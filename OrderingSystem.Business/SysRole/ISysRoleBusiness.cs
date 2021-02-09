using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface ISysRoleBusiness
    {


        SysRole GetById(int id);

        SysRole Insert(SysRole model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(SysRole model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(SysRole model);
        
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<SysRole> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<SysRole> GetAll();


    }
}
