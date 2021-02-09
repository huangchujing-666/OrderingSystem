using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public interface IBaseDicBusiness
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
        /// 根据type获取字典集合
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<BaseDic> GetBaseDiscListByType(string type);
    }
}
