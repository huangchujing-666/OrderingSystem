using OrderingSystem.Core.Data;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Business
{
    public class TicketBusiness : ITicketBusiness
    {
        private IRepository<Ticket> _repoTicket;

        public TicketBusiness(
          IRepository<Ticket> repoTicket
          )
        {
            _repoTicket = repoTicket;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Ticket GetById(int id)
        {
            return this._repoTicket.GetById(id);
        }

        public Ticket Insert(Ticket model)
        {
            return this._repoTicket.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Ticket model)
        {
            this._repoTicket.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Ticket model)
        {
            this._repoTicket.Delete(model);
        }
        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<Ticket> GetManagerList(string name, string businessName, int pageNum, int pageSize, out int totalCount, int businessId)
        {
            var where = PredicateBuilder.True<Ticket>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(businessName))
            {
                where = where.And(m => m.BusinessInfo.Name.Contains(businessName));
            }
            if (businessId != 0)
            {
                where = where.And(m => m.BusinessInfoId == businessId);
            }

            totalCount = this._repoTicket.Table.Where(where).Count();
            return this._repoTicket.Table.Where(where).OrderBy(p => p.TicketId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoTicket.Table.Any(p => p.Name == name);
        }
        public List<Ticket> GetAll()
        {
            return this._repoTicket.Table.ToList();
        }
    }
}


