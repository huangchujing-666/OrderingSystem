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
    public class BaseStationService: IBaseStationService
    {
        /// <summary>
        /// The BaseStation biz
        /// </summary>
        private IBaseStationBusiness _BaseStationBiz;

        public BaseStationService(IBaseStationBusiness BaseStationBiz)
        {
            _BaseStationBiz = BaseStationBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseStation GetById(int id)
        {
            return this._BaseStationBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseStation Insert(BaseStation model)
        {
            return this._BaseStationBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BaseStation model)
        {
            this._BaseStationBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BaseStation model)
        {
            this._BaseStationBiz.Delete(model);
        }
         
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<BaseStation> GetManagerList(string station, string line, string area, int pageNum, int pageSize, out int totalCount)
        {
            return this._BaseStationBiz.GetManagerList(station,line, area, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<BaseStation> GetAll()
        {
            return this._BaseStationBiz.GetAll();
        }
    }
}
