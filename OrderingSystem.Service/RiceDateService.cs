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
    public class RiceDateService: IRiceDateService
    {
        /// <summary>
        /// The RiceDate biz
        /// </summary>
        private IRiceDateBusiness _RiceDateBiz;

        public RiceDateService(IRiceDateBusiness RiceDateBiz)
        {
            _RiceDateBiz = RiceDateBiz;
        }


        public List<RiceDate> GetList()
        {
            return this._RiceDateBiz.GetList();
        }
        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RiceDate GetById(int id)
        {
            return this._RiceDateBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RiceDate Insert(RiceDate model)
        {
            return this._RiceDateBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(RiceDate model)
        {
            this._RiceDateBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(RiceDate model)
        {
            this._RiceDateBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<RiceDate> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._RiceDateBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 根据发布人 约饭Id获取约饭信息
        /// </summary>
        /// <param name="riceDateId">约饭主键</param>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public RiceDate GetByUserIdAndRiceDateId(int riceDateId, int userId)
        {
            return this._RiceDateBiz.GetByUserIdAndRiceDateId(riceDateId, userId);
        }

        /// <summary>
        /// 多条件查询拼饭内容
        /// </summary>
        /// <param name="date"></param>
        /// <param name="businessName"></param>
        /// <param name="district"></param>
        /// <returns></returns>
        public List<RiceDate> GetRiceDateByCondiction(DateTime? date, string businessName, string district, int Page_Index, int Page_Size,out int totalCount)
        {
            return this._RiceDateBiz.GetRiceDateByCondiction(date, businessName, district,  Page_Index,  Page_Size, out totalCount);
        }

        /// <summary>
        /// 根据用户Id获取发布拼饭列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <returns></returns>
        public List<RiceDate> GetByUserId(int userId, int page_Index, int page_Size,out int totalCount)
        {
            return this._RiceDateBiz.GetByUserId(userId, page_Index, page_Size,out totalCount);
        }
    }
}
