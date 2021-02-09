using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface ITicketRelateTicketService
    {
        TicketRelateTicket GetById(int id);

        TicketRelateTicket Insert(TicketRelateTicket model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(TicketRelateTicket model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(TicketRelateTicket model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<TicketRelateTicket> GetManagerList(int ticketId, int pageNum, int pageSize, out int totalCount);

        List<TicketRelateTicket> GetAll();
    }
}
