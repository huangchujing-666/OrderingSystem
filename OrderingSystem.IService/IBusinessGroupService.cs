using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IBusinessGroupService
    {
        BusinessGroup GetById(int id);

        BusinessGroup Insert(BusinessGroup model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(BusinessGroup model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(BusinessGroup model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<BusinessGroup> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);

        List<BusinessGroup> GetAll();

        /// <summary>
        /// 根据模块id获取分组
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        List<BusinessGroup> GetByBusinessTypeId(int module);
    }
}
