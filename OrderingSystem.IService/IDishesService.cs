﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.IService
{
    public interface IDishesService
    {
        Dishes GetById(int id);

        Dishes Insert(Dishes model);


        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Update(Dishes model);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        void Delete(Dishes model);

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        List<Dishes> GetManagerList(string name, string businessName, int pageNum, int pageSize, out int totalCount, int businessId);

        /// <summary>
        /// 根据商家ID获取菜品信息列表
        /// </summary>
        /// <param name="BusinessInfoId">商家ID</param>
        /// <returns></returns>
        List<Dishes> GetDishesByBusinessId(int BusinessInfoId);
    }
}
