using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface IBusinessPayConfigBusiness
    { 
        BusinessPayConfig GetById(int id);

        BusinessPayConfig Insert(BusinessPayConfig model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(BusinessPayConfig model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(BusinessPayConfig model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<BusinessPayConfig> GetManagerList(int businessId, int pageNum, int pageSize, out int totalCount);
         
         
    }
}
