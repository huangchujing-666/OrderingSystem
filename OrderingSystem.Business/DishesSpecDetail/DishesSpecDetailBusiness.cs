using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class DishesSpecDetailBusiness:IDishesSpecDetailBusiness
    {
        private IRepository<DishesSpecDetail> _repoDishesSpecDetail;

        public DishesSpecDetailBusiness(
          IRepository<DishesSpecDetail> repoDishesSpecDetail
          )
        {
            _repoDishesSpecDetail = repoDishesSpecDetail;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DishesSpecDetail GetById(int id)
        {
            return this._repoDishesSpecDetail.GetById(id);
        }

        public DishesSpecDetail Insert(DishesSpecDetail model)
        {
            return this._repoDishesSpecDetail.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(DishesSpecDetail model)
        {
            this._repoDishesSpecDetail.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(DishesSpecDetail model)
        {
            this._repoDishesSpecDetail.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<DishesSpecDetail> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<DishesSpecDetail>();
              
            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Descript.Contains(name));
            }

            totalCount = this._repoDishesSpecDetail.Table.Where(where).Count();
            return this._repoDishesSpecDetail.Table.Where(where).OrderBy(p => p.DishesSpecDetailId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 根据id列表获取详情列表
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<DishesSpecDetail> GetListByIds(string ids)// ids="1,2,3"
        {
            var where = PredicateBuilder.True<DishesSpecDetail>();

            // name过滤
            if (!string.IsNullOrWhiteSpace(ids))
            {
                string[] id = ids.Split(',');
                int[] iNums;
                iNums = Array.ConvertAll(id, int.Parse);
                where = where.And(c => iNums.Contains(c.DishesSpecDetailId));
            }
            else
            {
                return null;
            }
            return this._repoDishesSpecDetail.Table.Where(where).ToList();
        }

    }
}


