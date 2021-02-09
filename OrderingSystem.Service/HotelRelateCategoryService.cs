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
    public class HotelRelateCategoryService: IHotelRelateCategoryService
    {
        /// <summary>
        /// The HotelRelateCategory biz
        /// </summary>
        private IHotelRelateCategoryBusiness _HotelRelateCategoryBiz;

        public HotelRelateCategoryService(IHotelRelateCategoryBusiness HotelRelateCategoryBiz)
        {
            _HotelRelateCategoryBiz = HotelRelateCategoryBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HotelRelateCategory GetById(int id)
        {
            return this._HotelRelateCategoryBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public HotelRelateCategory Insert(HotelRelateCategory model)
        {
            return this._HotelRelateCategoryBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(HotelRelateCategory model)
        {
            this._HotelRelateCategoryBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(HotelRelateCategory model)
        {
            this._HotelRelateCategoryBiz.Delete(model);
        }
        

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<HotelRelateCategory> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._HotelRelateCategoryBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 获取所有
        /// </summary> 
        /// <returns></returns>
        public List<HotelRelateCategory> GetAll()
        {
            return this._HotelRelateCategoryBiz.GetAll();
        }
    }
}
