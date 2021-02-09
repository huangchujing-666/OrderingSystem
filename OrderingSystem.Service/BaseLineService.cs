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
    public class BaseLineService: IBaseLineService
    {
        /// <summary>
        /// The BaseLine biz
        /// </summary>
        private IBaseLineBusiness _BaseLineBiz;

        public BaseLineService(IBaseLineBusiness BaseLineBiz)
        {
            _BaseLineBiz = BaseLineBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseLine GetById(int id)
        {
            return this._BaseLineBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseLine Insert(BaseLine model)
        {
            return this._BaseLineBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BaseLine model)
        {
            this._BaseLineBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BaseLine model)
        {
            this._BaseLineBiz.Delete(model);
        }


        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BaseLine> GetManagerList(string lineName, string areaName, int pageNum, int pageSize, out int totalCount)
        {
            return this._BaseLineBiz.GetManagerList(lineName, areaName, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<BaseLine> GetAll()
        {
            return this._BaseLineBiz.GetAll();
        }
    }
}
