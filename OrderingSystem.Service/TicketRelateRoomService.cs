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
    public class TicketRelateRoomService: ITicketRelateRoomService
    {
        /// <summary>
        /// The TicketRelateRoom biz
        /// </summary>
        private ITicketRelateRoomBusiness _TicketRelateRoomBiz;

        public TicketRelateRoomService(ITicketRelateRoomBusiness TicketRelateRoomBiz)
        {
            _TicketRelateRoomBiz = TicketRelateRoomBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TicketRelateRoom GetById(int id)
        {
            return this._TicketRelateRoomBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public TicketRelateRoom Insert(TicketRelateRoom model)
        {
            return this._TicketRelateRoomBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(TicketRelateRoom model)
        {
            this._TicketRelateRoomBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(TicketRelateRoom model)
        {
            this._TicketRelateRoomBiz.Delete(model);
        }
        
        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<TicketRelateRoom> GetManagerList(int ticketId, int pageNum, int pageSize, out int totalCount)
        {
            return this._TicketRelateRoomBiz.GetManagerList(ticketId, pageNum, pageSize, out totalCount);
        }

        public List<TicketRelateRoom> GetAll()
        {
            return this._TicketRelateRoomBiz.GetAll();
        }
    }
}
