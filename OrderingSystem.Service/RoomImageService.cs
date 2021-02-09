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
    public class RoomImageService: IRoomImageService
    {
        /// <summary>
        /// The RoomImage biz
        /// </summary>
        private IRoomImageBusiness _RoomImageBiz;

        public RoomImageService(IRoomImageBusiness RoomImageBiz)
        {
            _RoomImageBiz = RoomImageBiz;
        }

        /// <summary>
        /// 根据ID查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoomImage GetById(int id)
        {
            return this._RoomImageBiz.GetById(id);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public RoomImage Insert(RoomImage model)
        {
            return this._RoomImageBiz.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(RoomImage model)
        {
            this._RoomImageBiz.Update(model);
        }
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(RoomImage model)
        {
            this._RoomImageBiz.Delete(model);
        }

        /// <summary>
        /// 添加管理后台菜单列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<RoomImage> GetManagerList(int roomId, int pageNum, int pageSize, out int totalCount)
        {
            return this._RoomImageBiz.GetManagerList(roomId, pageNum, pageSize, out totalCount);
        }
    }
}
