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
    public class BaseDicService: IBaseDicService
    {
        /// <summary>
        /// The BaseDic biz
        /// </summary>
        private IBaseDicBusiness _BaseDicBiz;

        public BaseDicService(IBaseDicBusiness BaseDicBiz)
        {
            _BaseDicBiz = BaseDicBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseDic GetById(int id)
        {
            return this._BaseDicBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseDic Insert(BaseDic model)
        {
            return this._BaseDicBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BaseDic model)
        {
            this._BaseDicBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BaseDic model)
        {
            this._BaseDicBiz.Delete(model);
        }
        

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BaseDic> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._BaseDicBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 查询所有
        /// </summary> 
        /// <returns></returns>
        public List<BaseDic> GetAll()
        {
            return this._BaseDicBiz.GetAll();
        }


        /// <summary>
        /// 获取字典
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public List<BaseDic> GetBaseDiscListByType(string Type)
        {
            return this._BaseDicBiz.GetBaseDiscListByType(Type);
        }
    }
}
