using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class CustomerBusiness : ICustomerBusiness
    {
        private IRepository<Customer> _repoCustomer;

        public CustomerBusiness(
          IRepository<Customer> repoCustomer
          )
        {
            _repoCustomer = repoCustomer;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetById(int id)
        {
            return this._repoCustomer.GetById(id);
        }

        public Customer Insert(Customer model)
        {
            return this._repoCustomer.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Customer model)
        {
            this._repoCustomer.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Customer model)
        {
            this._repoCustomer.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<Customer> GetManagerList(string name, int businessInfoId, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<Customer>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Name.Contains(name));
            }
 

            totalCount = this._repoCustomer.Table.Where(where).Count();
            return this._repoCustomer.Table.Where(where).OrderBy(p => p.CustomerId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoCustomer.Table.Any(p => p.Name == name);
        }

        public List<Customer> GetAll()
        {
            return this._repoCustomer.Table.ToList();
        }
        public Customer GetCustomerByCardNo(int userId, string cardNo,int cardType)
        {
            var where = PredicateBuilder.True<Customer>();
            if (userId>0&&!string.IsNullOrWhiteSpace(cardNo))
            {
                where = where.And(c=>c.UserId== userId && c.CardNo.Equals(cardNo)&&c.CardType== cardType);
                return this._repoCustomer.Table.Where(where).FirstOrDefault();
            }
            return null;
        }

        public List<Customer> GetCustomerListByUserId(int userId)
        {
            var where = PredicateBuilder.True<Customer>();
            if (userId > 0)
            {
                where = where.And(c => c.UserId== userId);
                return this._repoCustomer.Table.Where(where).OrderBy(c=>c.CustomerId).ToList();
            }
            return null;
        }

        public bool CustomerIdIsAllExist(int[] output)
        {
            bool result = false;
            var where = PredicateBuilder.True<Customer>();
            if (output!=null)
            {
                where = where.And(c => output.Contains(c.CustomerId));
                var list=this._repoCustomer.Table.Where(where).ToList();
                if (list!=null&& list.Count== output.Length)
                {
                    result = true;
                }
            }
            return result;
        }

    }
}


