using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class BusinessGroupImageBusiness:IBusinessGroupImageBusiness
    {
        private IRepository<BusinessGroupImage> _repoBusinessGroupImage;

        public BusinessGroupImageBusiness(
          IRepository<BusinessGroupImage> repoBusinessGroupImage
          )
        {
            _repoBusinessGroupImage = repoBusinessGroupImage;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessGroupImage GetById(int id)
        {
            return this._repoBusinessGroupImage.GetById(id);
        }

        public BusinessGroupImage Insert(BusinessGroupImage model)
        {
            return this._repoBusinessGroupImage.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessGroupImage model)
        {
            this._repoBusinessGroupImage.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessGroupImage model)
        {
            this._repoBusinessGroupImage.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<BusinessGroupImage> GetManagerList(int groupId, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BusinessGroupImage>();

            if (groupId!=0)
            {
                where = where.And(m => m.BusinessGroupId == groupId);
            }
            totalCount = this._repoBusinessGroupImage.Table.Where(where).Count();
            return this._repoBusinessGroupImage.Table.Where(where).OrderBy(p => p.BusinessGroupImageId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoBusinessGroupImage.Table.Any(p => p.Type == 0);
        }
        public List<BusinessGroupImage> GetAll()
        {
            return this._repoBusinessGroupImage.Table.ToList();
        }
    }
}


