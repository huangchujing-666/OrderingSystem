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
    public class DishesImageBusiness:IDishesImageBusiness
    {
        private IRepository<DishesImage> _repoDishesImage;

        public DishesImageBusiness(
          IRepository<DishesImage> repoDishesImage
          )
        {
            _repoDishesImage = repoDishesImage;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DishesImage GetById(int id)
        {
            return this._repoDishesImage.GetById(id);
        }

        public DishesImage Insert(DishesImage model)
        {
            return this._repoDishesImage.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(DishesImage model)
        {
            this._repoDishesImage.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(DishesImage model)
        {
            this._repoDishesImage.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<DishesImage> GetManagerList(int dishesId, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<DishesImage>();
            if (dishesId > 0)
            {
                where = where.And(p => p.DishesId == dishesId);
            }

            where = where.And(p => p.IsDelete == (int)IsDeleteEnum.有效);

            totalCount = this._repoDishesImage.Table.Where(where).Count();
            return this._repoDishesImage.Table.Where(where).OrderBy(p => p.DishesImageId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

      
    }
}


