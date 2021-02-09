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
    public class BaseAreaService: IBaseAreaService
    {
        /// <summary>
        /// The BaseArea biz
        /// </summary>
        private IBaseAreaBusiness _BaseAreaBiz;

        public BaseAreaService(IBaseAreaBusiness BaseAreaBiz)
        {
            _BaseAreaBiz = BaseAreaBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseArea GetById(int id)
        {
            return this._BaseAreaBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BaseArea Insert(BaseArea model)
        {
            return this._BaseAreaBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BaseArea model)
        {
            this._BaseAreaBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BaseArea model)
        {
            this._BaseAreaBiz.Delete(model);
        }
        

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BaseArea> GetManagerList(string name, int pid, int pageNum, int pageSize, out int totalCount)
        {
            return this._BaseAreaBiz.GetManagerList(name,pid, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<BaseArea> GetAll()
        {
            return this._BaseAreaBiz.GetAll();
        }


        /// <summary>
        /// 获取城市列表
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public List<BaseArea> GetCityList(int pid)
        {
            return this._BaseAreaBiz.GetCityList(pid);
        }
    }
}
