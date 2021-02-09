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
    public class RiceDateUserService : IRiceDateUserService
    {
        /// <summary>
        /// The RiceDateUser biz
        /// </summary>
        private IRiceDateUserBusiness _RiceDateUserBiz;

        public RiceDateUserService(IRiceDateUserBusiness RiceDateUserBiz)
        {
            _RiceDateUserBiz = RiceDateUserBiz;
        }


        public List<RiceDateUser> GetList()
        {
            return this._RiceDateUserBiz.GetList();
        }
        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RiceDateUser GetById(int id)
        {
            return this._RiceDateUserBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RiceDateUser Insert(RiceDateUser model)
        {
            return this._RiceDateUserBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(RiceDateUser model)
        {
            this._RiceDateUserBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(RiceDateUser model)
        {
            this._RiceDateUserBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<RiceDateUser> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._RiceDateUserBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 根据用户Id获取约饭成功集合
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public List<RiceDateUser> GetByUserId(int userId)
        {
            return this._RiceDateUserBiz.GetByUserId(userId);
        }

        /// <summary>
        /// 根据用户Id获取报名约饭集合
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<RiceDateUser> GetByUserId(int UserId, int Page_Index, int Page_Size, out int totalCount)
        {
            return this._RiceDateUserBiz.GetByUserId(UserId, Page_Index, Page_Size, out totalCount);
        }

        /// <summary>
        /// 获取用户是否已经报名
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="riceDateId"></param>
        /// <returns></returns>
        public RiceDateUser GetByUserIdAndRiceDateId(int userId, int riceDateId)
        {
            return this._RiceDateUserBiz.GetByUserIdAndRiceDateId(userId, riceDateId);
        }

        /// <summary>
        /// 根据约饭ID获取报名人数
        /// </summary>
        /// <param name="riceDateId"></param>
        /// <returns></returns>
        public List<RiceDateUser> GetByRiceDateId(int riceDateId)
        {
            return this._RiceDateUserBiz.GetByRiceDateId( riceDateId);
        }

        /// <summary>
        /// 获取报名我约饭的列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<RiceDateUser> GetJoinMyRiceDate(int userId, int page_Index, int page_Size, out int totalCount)
        {
            return this._RiceDateUserBiz.GetJoinMyRiceDate(userId, page_Index, page_Size,out totalCount);
        }
    }
}
