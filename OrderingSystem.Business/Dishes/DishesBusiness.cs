using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderingSystem.Domain.EnumHelp;

namespace OrderingSystem.Business
{
    public class DishesBusiness : IDishesBusiness
    {
        private IRepository<Dishes> _repoDishes;

        public DishesBusiness(
          IRepository<Dishes> repoDishes
          )
        {
            _repoDishes = repoDishes;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Dishes GetById(int id)
        {
            return this._repoDishes.GetById(id);
        }

        public Dishes Insert(Dishes model)
        {
            return this._repoDishes.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Dishes model)
        {
            this._repoDishes.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Dishes model)
        {
            this._repoDishes.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<Dishes> GetManagerList(string name,string businessName, int pageNum, int pageSize, out int totalCount, int businessId)
        {
            var where = PredicateBuilder.True<Dishes>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(businessName))
            {
                where = where.And(m => m.BusinessInfo.Name.Contains(businessName));
            } 

            if (businessId != 0)
            {
                where = where.And(m => m.BusinessInfoId == businessId);
            }
            where = where.And(m => m.IsDelete == (int)IsDeleteEnum.有效);

            totalCount = this._repoDishes.Table.Where(where).Count();
            return this._repoDishes.Table.Where(where).OrderBy(p => p.DishesId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 根据商家Id获取菜品信息
        /// </summary>
        /// <param name="BusinessInfoId"></param>
        /// <returns></returns>
        public List<Dishes> GetDishesByBusinessId(int businessInfoId)
        {
            return this._repoDishes.Table.Where(c => c.BusinessInfoId == businessInfoId && c.IsDelete == (int)IsDeleteEnum.有效 && c.Status == (int)EnabledEnum.有效).OrderByDescending(c => c.SellCountPerMonth).ToList();
        }

    }
}


