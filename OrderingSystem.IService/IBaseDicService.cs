using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IBaseDicService
    {
        BaseDic GetById(int id);

        BaseDic Insert(BaseDic model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(BaseDic model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(BaseDic model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<BaseDic> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 查询所有
        /// </summary> 
        /// <returns></returns>
        List<BaseDic> GetAll();

        /// <summary>
        /// 获取字典
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        List<BaseDic> GetBaseDiscListByType(string Type);
    }
}
