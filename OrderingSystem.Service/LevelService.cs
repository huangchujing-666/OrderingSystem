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
    public class LevelService: ILevelService
    {
        /// <summary>
        /// The Level biz
        /// </summary>
        private ILevelBusiness _LevelBiz;

        public LevelService(ILevelBusiness LevelBiz)
        {
            _LevelBiz = LevelBiz;
        }


        public List<Level> GetList()
        {
            return this._LevelBiz.GetList();
        }
        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Level GetById(int id)
        {
            return this._LevelBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Level Insert(Level model)
        {
            return this._LevelBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Level model)
        {
            this._LevelBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Level model)
        {
            this._LevelBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<Level> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._LevelBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }
    }
}
