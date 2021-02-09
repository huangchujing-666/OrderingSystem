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
    public class DishesService: IDishesService
    {
        /// <summary>
        /// The Dishes biz
        /// </summary>
        private IDishesBusiness _DishesBiz;

        public DishesService(IDishesBusiness DishesBiz)
        {
            _DishesBiz = DishesBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dishes GetById(int id)
        {
            return this._DishesBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Dishes Insert(Dishes model)
        {
            return this._DishesBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Dishes model)
        {
            this._DishesBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Dishes model)
        {
            this._DishesBiz.Delete(model);
        }
        

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<Dishes> GetManagerList(string name, string businessName, int pageNum, int pageSize, out int totalCount, int businessId)
        {
            return this._DishesBiz.GetManagerList(name, businessName, pageNum, pageSize, out totalCount, businessId);
        }

        public List<Dishes> GetDishesByBusinessId(int BusinessInfoId) {
            return this._DishesBiz.GetDishesByBusinessId(BusinessInfoId);
        }
    }
}
