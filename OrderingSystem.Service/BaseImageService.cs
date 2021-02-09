using OrderingSystem.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Business;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Service
{
    public class BaseImageService: IBaseImageService
    {
        /// <summary>
        /// The BaseImage biz
        /// </summary>
        private IBaseImageBusiness _BaseImageBiz;

        public BaseImageService(IBaseImageBusiness BaseImageBiz)
        {
            _BaseImageBiz = BaseImageBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseImage GetById(int id)
        {
            return this._BaseImageBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseImage Insert(BaseImage model)
        {
            return this._BaseImageBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BaseImage model)
        {
            this._BaseImageBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BaseImage model)
        {
            this._BaseImageBiz.Delete(model);
        }
        

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BaseImage> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._BaseImageBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 查询所有
        /// </summary> 
        /// <returns></returns>
        public List<BaseImage> GetAll()
        {
            return this._BaseImageBiz.GetAll();
        }


        /// <summary>
        /// 根据主键id数组查询图片集合
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public List<BaseImage> GetByIds(int[] output)
        {
            return this._BaseImageBiz.GetByIds(output);
        }

    }
}
