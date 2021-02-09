using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IProductRelateLableService
    {
        ProductRelateLable GetById(int id);

        ProductRelateLable Insert(ProductRelateLable model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(ProductRelateLable model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(ProductRelateLable model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<ProductRelateLable> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 获取所有
        /// </summary> 
        /// <returns></returns>
        List<ProductRelateLable> GetAll();
    }
}
