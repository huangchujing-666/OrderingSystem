using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class TicketRelateTicketBusiness : ITicketRelateTicketBusiness
    {
        private IRepository<TicketRelateTicket> _repoTicketRelateTicket;

        public TicketRelateTicketBusiness(
          IRepository<TicketRelateTicket> repoTicketRelateTicket
          )
        {
            _repoTicketRelateTicket = repoTicketRelateTicket;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TicketRelateTicket GetById(int id)
        {
            return this._repoTicketRelateTicket.GetById(id);
        }

        public TicketRelateTicket Insert(TicketRelateTicket model)
        {
            return this._repoTicketRelateTicket.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(TicketRelateTicket model)
        {
            this._repoTicketRelateTicket.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(TicketRelateTicket model)
        {
            this._repoTicketRelateTicket.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<TicketRelateTicket> GetManagerList(int ticketId, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<TicketRelateTicket>();
             
            where = where.And(p => p.TicketId == ticketId);

            totalCount = this._repoTicketRelateTicket.Table.Where(where).Count();
            return this._repoTicketRelateTicket.Table.Where(where).OrderBy(p => p.TicketRelateTicketId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(int relateTicketId)
        {
            return this._repoTicketRelateTicket.Table.Any(p => p.RelateTicketId == relateTicketId);
        }

        public List<TicketRelateTicket> GetAll()
        {
            return this._repoTicketRelateTicket.Table.ToList();
        }

    }
}


