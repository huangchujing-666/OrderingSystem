using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class HotelCategoryBusiness : IHotelCategoryBusiness
    {
        private IRepository<HotelCategory> _repoHotelCategory;

        public HotelCategoryBusiness(
          IRepository<HotelCategory> repoHotelCategory
          )
        {
            _repoHotelCategory = repoHotelCategory;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HotelCategory GetById(int id)
        {
            return this._repoHotelCategory.GetById(id);
        }

        public HotelCategory Insert(HotelCategory model)
        {
            return this._repoHotelCategory.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(HotelCategory model)
        {
            this._repoHotelCategory.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(HotelCategory model)
        {
            this._repoHotelCategory.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<HotelCategory> GetManagerList(string name, int businessInfoId, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<HotelCategory>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Name.Contains(name));
            }
         
            totalCount = this._repoHotelCategory.Table.Where(where).Count();
            return this._repoHotelCategory.Table.Where(where).OrderBy(p => p.HotelCategoryId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoHotelCategory.Table.Any(p => p.Name == name);
        }

        public List<HotelCategory> GetAll()
        {
            return this._repoHotelCategory.Table.ToList();
        }

    }
}


