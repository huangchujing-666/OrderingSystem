using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IBusinessBannerImageService
    {
        BusinessBannerImage GetById(int id);

        BusinessBannerImage Insert(BusinessBannerImage model);

        List<BusinessBannerImage> GetList();

        List<BusinessBannerImage> GetListByModule(int module);

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
