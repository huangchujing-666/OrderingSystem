using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IActivityDiscountService
    {
        ActivityDiscount GetById(int id);

        ActivityDiscount Insert(ActivityDiscount model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(ActivityDiscount model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(ActivityDiscount model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<ActivityDiscount> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);
        /// <summary>
        /// 根据商家ID获取折扣信息
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
         //ActivityDiscount GetByBusinessId(int businessId);
    }
}
