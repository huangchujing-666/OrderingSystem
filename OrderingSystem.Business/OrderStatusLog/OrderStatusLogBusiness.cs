using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class OrderStatusLogBusiness:IOrderStatusLogBusiness
    {
        private IRepository<OrderStatusLog> _repoOrderStatusLog;

        public OrderStatusLogBusiness(
          IRepository<OrderStatusLog> repoOrderStatusLog
          )
        {
            _repoOrderStatusLog = repoOrderStatusLog;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderStatusLog GetById(int id)
        {
            return this._repoOrderStatusLog.GetById(id);
        }

        public OrderStatusLog Insert(OrderStatusLog model)
        {
            return this._repoOrderStatusLog.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(OrderStatusLog model)
        {
            this._repoOrderStatusLog.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(OrderStatusLog model)
        {
            this._repoOrderStatusLog.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<OrderStatusLog> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<OrderStatusLog>();
               

            totalCount = this._repoOrderStatusLog.Table.Where(where).Count();
            return this._repoOrderStatusLog.Table.Where(where).OrderBy(p => p.OrderStatusLogId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }
         
        　
    }
}


