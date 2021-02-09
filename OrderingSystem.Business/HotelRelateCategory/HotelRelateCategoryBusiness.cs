using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class HotelRelateCategoryBusiness:IHotelRelateCategoryBusiness
    {
        private IRepository<HotelRelateCategory> _repoHotelRelateCategory;

        public HotelRelateCategoryBusiness(
          IRepository<HotelRelateCategory> repoHotelRelateCategory
          )
        {
            _repoHotelRelateCategory = repoHotelRelateCategory;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HotelRelateCategory GetById(int id)
        {
            return this._repoHotelRelateCategory.GetById(id);
        }

        public HotelRelateCategory Insert(HotelRelateCategory model)
        {
            return this._repoHotelRelateCategory.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(HotelRelateCategory model)
        {
            this._repoHotelRelateCategory.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(HotelRelateCategory model)
        {
            this._repoHotelRelateCategory.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<HotelRelateCategory> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<HotelRelateCategory>();
              
             
            totalCount = this._repoHotelRelateCategory.Table.Where(where).Count();
            return this._repoHotelRelateCategory.Table.Where(where).OrderBy(p => p.HotelRelateCategoryId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 获取所有
        /// </summary> 
        /// <returns></returns>
        public List<HotelRelateCategory> GetAll()
        { 
            return this._repoHotelRelateCategory.Table.ToList();
        }

    }
}


