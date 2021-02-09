using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderingSystem.Domain.EnumHelp;

namespace OrderingSystem.Business
{
    public class SysRoleBusiness:ISysRoleBusiness
    {
        private IRepository<SysRole> _repoSysRole;

        public SysRoleBusiness(
          IRepository<SysRole> repoSysRole
          )
        {
            _repoSysRole = repoSysRole;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SysRole GetById(int id)
        {
            return this._repoSysRole.GetById(id);
        }

        public SysRole Insert(SysRole model)
        {
            return this._repoSysRole.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(SysRole model)
        {
            this._repoSysRole.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(SysRole model)
        {
            this._repoSysRole.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<SysRole> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<SysRole>();
              
            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Name.Contains(name));
            }

            totalCount = this._repoSysRole.Table.Where(where).Count();
            return this._repoSysRole.Table.Where(where).OrderBy(p => p.SysRoleId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<SysRole> GetAll()
        {
            var where = PredicateBuilder.True<SysRole>();

            where = where.And(m => m.IsDelete == (int)IsDeleteEnum.有效 && m.Status == (int)EnabledEnum.有效);


            return this._repoSysRole.Table.Where(where).OrderBy(p => p.SysRoleId).ToList();
        }
    }
}


