using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class OrderStatusBusiness:IOrderStatusBusiness
    {
        private IRepository<OrderStatus> _repoOrderStatus;

        public OrderStatusBusiness(
          IRepository<OrderStatus> repoOrderStatus
          )
        {
            _repoOrderStatus = repoOrderStatus;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderStatus GetById(int id)
        {
            return this._repoOrderStatus.GetById(id);
        }

        public OrderStatus Insert(OrderStatus model)
        {
            return this._repoOrderStatus.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(OrderStatus model)
        {
            this._repoOrderStatus.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(OrderStatus model)
        {
            this._repoOrderStatus.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<OrderStatus> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<OrderStatus>();
              
            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Name.Contains(name));
            }

            totalCount = this._repoOrderStatus.Table.Where(where).Count();
            return this._repoOrderStatus.Table.Where(where).OrderBy(p => p.OrderStatusId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoOrderStatus.Table.Any(p => p.Name == name);
        }
        　
    }
}


