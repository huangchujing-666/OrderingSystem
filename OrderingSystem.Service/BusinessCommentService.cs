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
    public class BusinessCommentService: IBusinessCommentService
    {
        /// <summary>
        /// The BusinessComment biz
        /// </summary>
        private IBusinessCommentBusiness _BusinessCommentBiz;

        public BusinessCommentService(IBusinessCommentBusiness BusinessCommentBiz)
        {
            _BusinessCommentBiz = BusinessCommentBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessComment GetById(int id)
        {
            return this._BusinessCommentBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public BusinessComment Insert(BusinessComment model)
        {
            return this._BusinessCommentBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessComment model)
        {
            this._BusinessCommentBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessComment model)
        {
            this._BusinessCommentBiz.Delete(model);
        }

        /// <summary>
        /// 判断用户是否已经对订单评论了
        /// </summary>
        /// <param name="businesssId"></param>
        /// <param name="userInfoId"></param>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public bool IsExist(int businesssId, int userInfoId, string orderNo)
        {
            return this._BusinessCommentBiz.IsExist(businesssId, userInfoId, orderNo);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<BusinessComment> GetManagerList(string name, int pageNum, int pageSize, out int totalCount)
        {
            return this._BusinessCommentBiz.GetManagerList(name, pageNum, pageSize, out totalCount);
        }

        /// <summary>
        /// 获取评论分页列表
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="levelId"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<BusinessComment> GetPageList(int businessId, int levelId, int pageNum, int pageSize, out int totalCount)
        {
            return this._BusinessCommentBiz.GetPageList(businessId, levelId,pageNum, pageSize, out totalCount);
        }


        /// <summary>
        /// 获取评论分页列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="levelId"></param>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<BusinessComment> GetPageListByUserId(int userId,int module, int levelId, int pageNum, int pageSize, out int totalCount) {
            return this._BusinessCommentBiz.GetPageListByUserId(userId,module, levelId, pageNum, pageSize, out totalCount);
        }
    }
}
