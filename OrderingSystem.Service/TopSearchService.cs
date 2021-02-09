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
    public class TopSearchService: ITopSearchService
    {
        /// <summary>
        /// The TopSearch biz
        /// </summary>
        private ITopSearchBusiness _TopSearchBiz;

        public TopSearchService(ITopSearchBusiness TopSearchBiz)
        {
            _TopSearchBiz = TopSearchBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TopSearch GetById(int id)
        {
            return this._TopSearchBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TopSearch Insert(TopSearch model)
        {
            return this._TopSearchBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(TopSearch model)
        {
            this._TopSearchBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(TopSearch model)
        {
            this._TopSearchBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<TopSearch> GetManagerList(string name,int typeid, int pageNum, int pageSize, out int totalCount)
        {
            return this._TopSearchBiz.GetManagerList(name, typeid, pageNum, pageSize, out totalCount);
        }
    }
}
