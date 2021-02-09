using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IBaseImageService
    {
        BaseImage GetById(int id);

        BaseImage Insert(BaseImage model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(BaseImage model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(BaseImage model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<BaseImage> GetManagerList(string name, int pageNum, int pageSize, out int totalCount);

        /// <summary>
        /// 查询所有
        /// </summary> 
        /// <returns></returns>
        List<BaseImage> GetAll();

        /// <summary>
        /// 根据主键id数组查询图片集合
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        List<BaseImage> GetByIds(int[] output);
    }
}
