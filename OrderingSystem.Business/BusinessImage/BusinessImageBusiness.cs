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
    public class BusinessImageBusiness : IBusinessImageBusiness
    {
        private IRepository<BusinessImage> _repoBusinessImage;

        public BusinessImageBusiness(
          IRepository<BusinessImage> repoBusinessImage
          )
        {
            _repoBusinessImage = repoBusinessImage;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessImage GetById(int id)
        {
            return this._repoBusinessImage.GetById(id);
        }

        public BusinessImage Insert(BusinessImage model)
        {
            return this._repoBusinessImage.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessImage model)
        {
            this._repoBusinessImage.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessImage model)
        {
            this._repoBusinessImage.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<BusinessImage> GetManagerList(int businessId, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BusinessImage>();
            if (businessId > 0)
            {
                where = where.And(p => p.BusinessInfoId == businessId);
            }

            where = where.And(p => p.IsDelete == (int)IsDeleteEnum.有效);

            totalCount = this._repoBusinessImage.Table.Where(where).Count();
            return this._repoBusinessImage.Table.Where(where).OrderBy(p => p.BusinessImageId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<BusinessImage> GetByBusinessInfoId(int businessInfoId,int type) {
            var where = PredicateBuilder.True<BusinessImage>();
            if (businessInfoId > 0)
            {
                where = where.And(p => p.BusinessInfoId == businessInfoId);
            }
            if (type>0)
            {
                where = where.And(p => p.Type == type);
            }

            where = where.And(p => p.IsDelete == (int)IsDeleteEnum.有效&& p.Status == (int)EnabledEnum.有效);
            return this._repoBusinessImage.Table.Where(where).OrderBy(p=>p.SortNo).ToList();
        }

    }
}


