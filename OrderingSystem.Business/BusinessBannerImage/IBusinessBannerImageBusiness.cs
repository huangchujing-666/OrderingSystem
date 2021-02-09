using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface IBusinessBannerImageBusiness
    {


        BusinessBannerImage GetById(int id);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        List<BusinessBannerImage> GetList();

        List<BusinessBannerImage> GetListByModule(int module);
        BusinessBannerImage Insert(BusinessBannerImage model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(BusinessBannerImage model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(BusinessBannerImage model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<BusinessBannerImage> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);


    }
}
