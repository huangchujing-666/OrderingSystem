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
    public class TicketService: ITicketService
    {
        /// <summary>
        /// The Ticket biz
        /// </summary>
        private ITicketBusiness _TicketBiz;

        public TicketService(ITicketBusiness TicketBiz)
        {
            _TicketBiz = TicketBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Ticket GetById(int id)
        {
            return this._TicketBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Ticket Insert(Ticket model)
        {
            return this._TicketBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Ticket model)
        {
            this._TicketBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Ticket model)
        {
            this._TicketBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<Ticket> GetManagerList(string name, string businessName, int pageNum, int pageSize, out int totalCount, int businessId)
        {
            return this._TicketBiz.GetManagerList(name, businessName, pageNum, pageSize, out totalCount, businessId);
        }

        public List<Ticket> GetAll()
        {
            return this._TicketBiz.GetAll();
        }
    }
}
