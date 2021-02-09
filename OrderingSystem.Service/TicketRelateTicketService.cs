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
    public class TicketRelateTicketService: ITicketRelateTicketService
    {
        /// <summary>
        /// The TicketRelateTicket biz
        /// </summary>
        private ITicketRelateTicketBusiness _TicketRelateTicketBiz;

        public TicketRelateTicketService(ITicketRelateTicketBusiness TicketRelateTicketBiz)
        {
            _TicketRelateTicketBiz = TicketRelateTicketBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TicketRelateTicket GetById(int id)
        {
            return this._TicketRelateTicketBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TicketRelateTicket Insert(TicketRelateTicket model)
        {
            return this._TicketRelateTicketBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(TicketRelateTicket model)
        {
            this._TicketRelateTicketBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(TicketRelateTicket model)
        {
            this._TicketRelateTicketBiz.Delete(model);
        }
        
        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<TicketRelateTicket> GetManagerList(int ticketId, int pageNum, int pageSize, out int totalCount)
        {
            return this._TicketRelateTicketBiz.GetManagerList(ticketId, pageNum, pageSize, out totalCount);
        }

        public List<TicketRelateTicket> GetAll()
        {
            return this._TicketRelateTicketBiz.GetAll();
        }
    }
}
