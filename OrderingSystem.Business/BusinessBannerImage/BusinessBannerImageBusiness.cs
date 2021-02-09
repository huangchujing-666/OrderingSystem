using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class BusinessBannerImageBusiness : IBusinessBannerImageBusiness
    {
        private IRepository<BusinessBannerImage> _repoBusinessBannerImage;

        public BusinessBannerImageBusiness(
          IRepository<BusinessBannerImage> repoBusinessBannerImage
          )
        {
            _repoBusinessBannerImage = repoBusinessBannerImage;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessBannerImage GetById(int id)
        {
            return this._repoBusinessBannerImage.GetById(id);
        }

        public List<BusinessBannerImage> GetList()
        {
            return this._repoBusinessBannerImage.Table.OrderBy(c=>c.SortNo).ToList();
        }
        public List<BusinessBannerImage> GetListByModule(int module)
        {
            return this._repoBusinessBannerImage.Table.Where(c=>c.Module==module).OrderBy(c => c.SortNo).ToList();
        }
        public BusinessBannerImage Insert(BusinessBannerImage model)
        {
            return this._repoBusinessBannerImage.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessBannerImage model)
        {
            this._repoBusinessBannerImage.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessBannerImage model)
        {
            this._repoBusinessBannerImage.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<BusinessBannerImage> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BusinessBannerImage>();

            if (!string.IsNullOrWhiteSpace(name))
            {
                where = where.And(p=>p.BusinessInfo.Name.Contains(name));
            }
            totalCount = this._repoBusinessBannerImage.Table.Where(where).Count();
            return this._repoBusinessBannerImage.Table.Where(where).OrderBy(p => p.BusinessBannerImageId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }


    }
}


