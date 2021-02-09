using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class SystemLogBusiness:ISystemLogBusiness
    {
        private IRepository<SystemLog> _repoSystemLog;

        public SystemLogBusiness(
          IRepository<SystemLog> repoSystemLog
          )
        {
            _repoSystemLog = repoSystemLog;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SystemLog GetById(int id)
        {
            return this._repoSystemLog.GetById(id);
        }

        public SystemLog Insert(SystemLog model)
        {
            return this._repoSystemLog.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(SystemLog model)
        {
            this._repoSystemLog.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(SystemLog model)
        {
            this._repoSystemLog.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<SystemLog> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<SystemLog>();
             
            totalCount = this._repoSystemLog.Table.Where(where).Count();
            return this._repoSystemLog.Table.Where(where).OrderBy(p => p.SystemLogId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }
         

    }
}


