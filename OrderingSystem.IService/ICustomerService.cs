using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface ICustomerService
    {
        Customer GetById(int id);

        Customer Insert(Customer model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(Customer model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(Customer model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<Customer> GetManagerList(string name,int businessInfoId, int pageNum, int pageSize, out int totalCount);

        List<Customer> GetAll();
        Customer GetCustomerByCardNo(int userId, string cardNo,int cardType);
        List<Customer> GetCustomerListByUserId(int userId);
        bool CustomerIdIsAllExist(int[] output);
    }
}
