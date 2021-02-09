using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class OrderCustomerBusiness : IOrderCustomerBusiness
    {
        private IRepository<OrderCustomer> _repoOrderCustomer;

        public OrderCustomerBusiness(
          IRepository<OrderCustomer> repoOrderCustomer
          )
        {
            _repoOrderCustomer = repoOrderCustomer;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderCustomer GetById(int id)
        {
            return this._repoOrderCustomer.GetById(id);
        }

        public OrderCustomer Insert(OrderCustomer model)
        {
            return this._repoOrderCustomer.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(OrderCustomer model)
        {
            this._repoOrderCustomer.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(OrderCustomer model)
        {
            this._repoOrderCustomer.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<OrderCustomer> GetManagerList(string name,int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<OrderCustomer>();

         
            totalCount = this._repoOrderCustomer.Table.Where(where).Count();
            return this._repoOrderCustomer.Table.Where(where).OrderBy(p => p.OrderCustomerId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

      
        public List<OrderCustomer> GetAll()
        {
            return this._repoOrderCustomer.Table.ToList();
        }
    }
}


