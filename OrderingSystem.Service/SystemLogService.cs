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
    public class SystemLogService: ISystemLogService
    {
        /// <summary>
        /// The SystemLog biz
        /// </summary>
        private ISystemLogBusiness _SystemLogBiz;

        public SystemLogService(ISystemLogBusiness SystemLogBiz)
        {
            _SystemLogBiz = SystemLogBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SystemLog GetById(int id)
        {
            return this._SystemLogBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public SystemLog Insert(SystemLog model)
        {
            return this._SystemLogBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(SystemLog model)
        {
            this._SystemLogBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(SystemLog model)
        {
            this._SystemLogBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<SystemLog> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._SystemLogBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }
    }
}
