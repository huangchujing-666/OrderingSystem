using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface IBusinessImageBusiness
    {


        BusinessImage GetById(int id);

        BusinessImage Insert(BusinessImage model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(BusinessImage model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(BusinessImage model);
        
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<BusinessImage> GetManagerList(int businessId, int pageNum, int pageSize, out int totalCount);
        List<BusinessImage> GetByBusinessInfoId(int businessInfoId, int type);
    }
}
