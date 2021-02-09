using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class BaseImageBusiness : IBaseImageBusiness
    {
        private IRepository<BaseImage> _repoBaseImage;

        public BaseImageBusiness(
          IRepository<BaseImage> repoBaseImage
          )
        {
            _repoBaseImage = repoBaseImage;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BaseImage GetById(int id)
        {
            return this._repoBaseImage.GetById(id);
        }

        public BaseImage Insert(BaseImage model)
        {
            return this._repoBaseImage.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BaseImage model)
        {
            this._repoBaseImage.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BaseImage model)
        {
            this._repoBaseImage.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<BaseImage> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BaseImage>();


            totalCount = this._repoBaseImage.Table.Where(where).Count();
            return this._repoBaseImage.Table.Where(where).OrderBy(p => p.BaseImageId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }
        /// <summary>
        /// 查询所有
        /// </summary> 
        /// <returns></returns>
        public List<BaseImage> GetAll()
        {
            var where = PredicateBuilder.True<BaseImage>();

            return this._repoBaseImage.Table.ToList();
        }

        /// <summary>
        /// 根据图片数组查询图片集合
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public List<BaseImage> GetByIds(int[] output)
        {
            var where = PredicateBuilder.True<BaseImage>();

            return this._repoBaseImage.Table.Where(c=> output.Contains(c.BaseImageId)).ToList();
        }
    }
}


