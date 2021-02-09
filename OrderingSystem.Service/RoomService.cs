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
    public class RoomService: IRoomService
    {
        /// <summary>
        /// The Room biz
        /// </summary>
        private IRoomBusiness _RoomBiz;

        public RoomService(IRoomBusiness RoomBiz)
        {
            _RoomBiz = RoomBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Room GetById(int id)
        {
            return this._RoomBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Room Insert(Room model)
        {
            return this._RoomBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(Room model)
        {
            this._RoomBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(Room model)
        {
            this._RoomBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<Room> GetManagerList(string name,  string businessName,int pageNum, int pageSize, out int totalCount,int businessId)
        {
            return this._RoomBiz.GetManagerList(name, businessName, pageNum, pageSize, out totalCount, businessId);
        }

        public List<Room> GetListByBusinessId(int businessId)
        {
            return this._RoomBiz.GetListByBusinessId(businessId);
        }

        public List<Room> GetAll()
        {
            return this._RoomBiz.GetAll();
        }
    }
}
