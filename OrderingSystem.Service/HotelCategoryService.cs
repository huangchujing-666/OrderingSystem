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
    public class HotelCategoryService: IHotelCategoryService
    {
        /// <summary>
        /// The HotelCategory biz
        /// </summary>
        private IHotelCategoryBusiness _HotelCategoryBiz;

        public HotelCategoryService(IHotelCategoryBusiness HotelCategoryBiz)
        {
            _HotelCategoryBiz = HotelCategoryBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HotelCategory GetById(int id)
        {
            return this._HotelCategoryBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public HotelCategory Insert(HotelCategory model)
        {
            return this._HotelCategoryBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(HotelCategory model)
        {
            this._HotelCategoryBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(HotelCategory model)
        {
            this._HotelCategoryBiz.Delete(model);
        }
        
        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<HotelCategory> GetManagerList(string name,int businessInfoId, int pageNum, int pageSize, out int totalCount)
        {
            return this._HotelCategoryBiz.GetManagerList(name, businessInfoId, pageNum, pageSize, out totalCount);
        }

        public List<HotelCategory> GetAll()
        {
            return this._HotelCategoryBiz.GetAll();
        }
    }
}
