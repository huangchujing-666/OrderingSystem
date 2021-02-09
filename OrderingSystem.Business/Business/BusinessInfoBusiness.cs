using OrderingSystem.Core.Data;
using OrderingSystem.Data;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using static OrderingSystem.Domain.EnumHelp;
using System.Data.Entity;

namespace OrderingSystem.Business
{
    public class BusinessInfoBusiness : IBusinessInfoBusiness
    {

        private IRepository<BusinessInfo> _repoBusinessInfo;

        public BusinessInfoBusiness(
          IRepository<BusinessInfo> repoBusinessInfo
          )
        {
            _repoBusinessInfo = repoBusinessInfo;
        }
        /// <summary>
        /// 根据ID查找用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BusinessInfo GetById(int id)
        {
            return this._repoBusinessInfo.GetById(id);
        }

        public BusinessInfo Insert(BusinessInfo model)
        {
            return this._repoBusinessInfo.Insert(model);
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(BusinessInfo model)
        {
            this._repoBusinessInfo.Update(model);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Delete(BusinessInfo model)
        {
            this._repoBusinessInfo.Delete(model);
        }
        /// <summary>
        /// 获取酒店列表
        /// </summary> 
        /// <returns></returns>
        public List<BusinessInfo> GetHotelList()
        {
            var where = PredicateBuilder.True<BusinessInfo>();

            where = where.And(p => p.BusinessTypeId == (int)BusinessTypeEnum.酒店);
            where = where.And(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效);

            return this._repoBusinessInfo.Table.Where(where).ToList();
        }

        /// <summary>
        /// 管理后台用户列表
        /// </summary> 
        /// <returns></returns>
        public List<BusinessInfo> GetManagerList(string name, int type, int pageNum, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BusinessInfo>();

            // name过滤
            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(m => m.Name.Contains(name));
            }
            //商家模块类型（食衣乐）
            if (type != 0)
            {
                where = where.And(m => m.BusinessTypeId == type);
            }

            where = where.And(m => m.Status == (int)EnabledEnum.有效 && m.IsDelete == (int)IsDeleteEnum.有效);

            totalCount = this._repoBusinessInfo.Table.Where(where).Count();
            return this._repoBusinessInfo.Table.Where(where).OrderBy(p => p.BusinessInfoId).Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 判断是否名称存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsExistName(string name)
        {
            return this._repoBusinessInfo.Table.Any(p => p.Name == name);
        }
        /// <summary>
        /// 获取商家列表
        /// </summary>
        /// <returns></returns>

        public List<BusinessInfo> GetList(int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = _repoBusinessInfo.Table.Where(p => p.IsDelete == (int)IsDeleteEnum.有效 && p.Status == (int)EnabledEnum.有效 && p.BusinessTypeId == (int)BusinessTypeEnum.食).ToList().Count;
            return this._repoBusinessInfo.Table.Where(p => p.IsDelete == (int)IsDeleteEnum.有效 && p.Status == (int)EnabledEnum.有效 && p.BusinessTypeId == (int)BusinessTypeEnum.食).OrderBy(c => c.BusinessInfoId).Take(pageSize * pageIndex).Skip((pageIndex - 1) * pageSize).ToList();
        }

        /// <summary>
        /// 根据商家类型获取商家列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetListByType(int type, int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = _repoBusinessInfo.Table.Where(p => p.IsDelete == (int)IsDeleteEnum.有效 && p.Status == (int)EnabledEnum.有效 && p.BusinessTypeId == type).ToList().Count;
            return this._repoBusinessInfo.Table.Where(p => p.IsDelete == (int)IsDeleteEnum.有效 && p.Status == (int)EnabledEnum.有效 && p.BusinessTypeId == type).OrderBy(c => c.BusinessInfoId).Take(pageSize * pageIndex).Skip((pageIndex - 1) * pageSize).ToList();
        }

        /// <summary>
        /// 获取商家列表
        /// </summary>
        /// <param name="module"></param>
        /// <param name="businessgroupId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetListByGroup(int module, int businessgroupId, int pageIndex, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BusinessInfo>();
            //商家模块类型（食衣乐）

            if (module > 0)
            {
                where = where.And(m => m.BusinessTypeId == module);
            }
            // 商家分组
            if (businessgroupId > 0)
            {
                where = where.And(m => m.BusinessGroupId == businessgroupId);
            }
            where = where.And(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效);
            totalCount = _repoBusinessInfo.Table.Where(where).ToList().Count;
            return this._repoBusinessInfo.Table.Where(where).OrderBy(c => c.BusinessInfoId).Take(pageSize * pageIndex).Skip((pageIndex - 1) * pageSize).ToList();
        }

        /// <summary>
        /// 根据商家定位查询商家
        /// </summary>
        /// <param name="Longitude">经度</param>
        /// <param name="Latitude">纬度</param>
        /// <returns></returns>
        public List<BusinessInfo> GetListByLocation(int BusinessTypeId, double Longitude, double Latitude, int Page_Index, int Page_Size, out int total_count, int BusinessGroupId = 0)
        {
            //定义sql
            string sqlStr = @"select *,Round(
                            CAST(CAST(
                            sqrt(
                                (
                                 ((#longitude-Longitude)*PI()*12656*cos(((22.568207+Latitude)/2)*PI()/180)/180)  
                                 *  
                                 ((#longitude-Longitude)*PI()*12656*cos (((22.568207+Latitude)/2)*PI()/180)/180)  
                                )
                                +  
                                (  
                                 ((#latitude-Latitude)*PI()*12656/180)  
                                 *  
                                 ((#latitude-Latitude)*PI()*12656/180)  
                                ) 
                            )   
                            AS DECIMAL(20,7)) AS VARCHAR(20))
                            ,3)
                            as dis from BusinessInfo b where b.Status=1 and b.IsDelete =0 and  b.BusinessTypeId =#BusinessTypeId and b.BusinessGroupId=#BusinessGroupId  
                            order by dis";
            sqlStr = sqlStr.Replace("#longitude", Longitude.ToString()).Replace("#latitude", Latitude.ToString()).Replace("#BusinessTypeId", BusinessTypeId.ToString()).Replace("#BusinessGroupId", BusinessGroupId.ToString());
            total_count = this._repoBusinessInfo.SqlQuery(sqlStr, new String[] { }).ToList().Count;
            return this._repoBusinessInfo.SqlQuery(sqlStr, new String[] { }).Skip(Page_Size * (Page_Index - 1)).Take(Page_Size).ToList();
        }


        public List<BusinessInfo> GetListByIds(List<int> ids)
        {
            var where = PredicateBuilder.True<BusinessInfo>();
            if (ids != null)
            {
                where = where.And(p => ids.Contains(p.BusinessInfoId));
            }
            where = where.And(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效);

            return this._repoBusinessInfo.Table.Where(where).ToList();
        }

        /// <summary>
        /// 根据搜索关键字获取商家信息
        /// </summary>
        /// <param name="search_name">关键字</param>
        /// <param name="pageIndex">第几页（默认为1，第一页）</param>
        /// <param name="pageSize">每页显示多少条数据（默认为20，显示20条）</param>
        /// <returns></returns>
        public List<BusinessInfo> GetBusinessInfoBySearch(string search_name, int pageIndex, int pageSize, out int totalCount, int business_type_id = 0, int business_group_id = 0)
        {
            var where = PredicateBuilder.True<BusinessInfo>();
            if (business_type_id > 0)
            {
                where = where.And(c => c.BusinessTypeId == business_type_id);
            }
            if (business_group_id > 0)
            {
                where = where.And(c => c.BusinessGroupId == business_group_id);
            }
            if (!string.IsNullOrWhiteSpace(search_name))
            {
                where = where.And(p => p.Name.Contains(search_name));
            }
            where = where.And(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效);

            totalCount = this._repoBusinessInfo.Table.Where(where).Count();
            return this._repoBusinessInfo.Table.Where(where).OrderBy(c => c.BusinessInfoId).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 模糊搜索
        /// </summary>
        /// <param name="search_Name"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetIndexBusinessInfoBySearch(string search_Name, int page_Index, int page_Size, out int total_count)
        {
            var where = PredicateBuilder.True<BusinessInfo>();
            if (!string.IsNullOrWhiteSpace(search_Name))
            {
                where = where.And(p => p.Name.Contains(search_Name));
            }
            where = where.And(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效);

            total_count = this._repoBusinessInfo.Table.Where(where).Count();
            return this._repoBusinessInfo.Table.Where(where).OrderBy(c => c.BusinessInfoId).Skip((page_Index - 1) * page_Size).Take(page_Size).ToList();
        }
        /// <summary>
        /// 根据搜索站点获取商家信息
        /// </summary>
        /// <param name="station_id">站点id</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页显示多少条数据</param>
        /// <param name="totalCount">返回总页数</param>
        /// <returns></returns>
        public List<BusinessInfo> GetBusinessInfoByStation(int station_id, int business_type_id, int business_group_id, int pageIndex, int pageSize, out int totalCount)
        {
            var where = PredicateBuilder.True<BusinessInfo>();
            if (business_type_id > 0)
            {
                where = where.And(c => c.BusinessTypeId == business_type_id);
            }
            if (business_group_id > 0)
            {
                where = where.And(c => c.BusinessGroupId == business_group_id);
            }
            if (station_id > 0)
            {
                where = where.And(p => p.BaseStationId == station_id);
            }

            where = where.And(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效);

            totalCount = this._repoBusinessInfo.Table.Where(where).Count();
            return this._repoBusinessInfo.Table.Where(where).OrderByDescending(p => p.OrderCountPerMonth).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 酒店筛选 多条件 非定位
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="hotelCategoryId"></param>
        /// <param name="price"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetListByManCond(int[] grade, int[] hotelCategoryId, int[] price, int page_Index, int page_Size, out int total_count)
        {
            StringBuilder sb = new StringBuilder(200);
            string sqlStr = "select y.* from BusinessInfo y inner join (select MAX(d.Grade) Grade,MAX(d.RoomId) RoomId,MAX(d.RealPrice) RealPrice,MAX(d.BusinessInfoId) BusinessInfoId,Max(h.HotelCategoryId) HotelCategoryId from (select b.BusinessInfoId, b.Grade, r.RoomId, r.RealPrice from BusinessInfo b right join Room r on b.BusinessInfoId = r.BusinessInfoId where b.BusinessTypeId = 4  and b.[Status]=1 and b.IsDelete=0) d inner join HotelRelateCategory h on d.BusinessInfoId = h.BusinessInfoId where 1 = 1 ";
            sb.Append(sqlStr);
            if (price != null && price.Length > 0)
            {
                for (int i = 0; i < price.Length; i++)
                {
                    var str = EnumHelp.HotelPrice[price[i]];
                    if (!string.IsNullOrWhiteSpace(str))//只有一个元素
                    {
                        var aa = str.Split('-').ToArray();
                        decimal[] output = Array.ConvertAll<string, decimal>(aa, delegate (string s) { return decimal.Parse(s); });
                        if (price.Length == 1)
                        {
                            sb.Append(" and (RealPrice>=");
                            sb.Append(output[0]);
                            sb.Append(" and RealPrice<");
                            sb.Append(output[1]);
                            sb.Append(") ");
                            continue;
                        }
                        else if (i == 0)//第一个
                        {
                            sb.Append(" and ((RealPrice>=");
                            sb.Append(output[0]);
                            sb.Append(" and RealPrice<");
                            sb.Append(output[1]);
                            sb.Append(") ");
                        }
                        else if (i == (price.Length - 1))//最后一个
                        {
                            sb.Append(" or (RealPrice>=");
                            sb.Append(output[0]);
                            sb.Append(" and RealPrice<");
                            sb.Append(output[1]);
                            sb.Append(")) ");
                        }
                        else
                        {
                            sb.Append(" or (RealPrice>=");
                            sb.Append(output[0]);
                            sb.Append(" and RealPrice<");
                            sb.Append(output[1]);
                            sb.Append(") ");
                        }
                    }
                }
            }
            if (grade != null && grade.Length > 0)
            {
                // and ((Grade>=1 and Grade<2) or (Grade>=4 and Grade<5)) 
                for (int i = 0; i < grade.Length; i++)
                {
                    var str = EnumHelp.HotelGrade[grade[i]];
                    if (!string.IsNullOrWhiteSpace(str))//只有一个元素
                    {
                        var aa = str.Split('-').ToArray();
                        int[] output = Array.ConvertAll<string, int>(aa, delegate (string s) { return int.Parse(s); });
                        if (grade.Length == 1)
                        {
                            sb.Append(" and (Grade>=");
                            sb.Append(output[0]);
                            sb.Append(" and Grade<");
                            sb.Append(output[1]);
                            sb.Append(") ");
                            continue;
                        }
                        else if (i == 0)//第一个
                        {
                            sb.Append(" and ((Grade>=");
                            sb.Append(output[0]);
                            sb.Append(" and Grade<");
                            sb.Append(output[1]);
                            sb.Append(") ");
                        }
                        else if (i == (grade.Length - 1))//最后一个
                        {
                            sb.Append(" or (Grade>=");
                            sb.Append(output[0]);
                            sb.Append(" and Grade<");
                            sb.Append(output[1]);
                            sb.Append(")) ");
                        }
                        else
                        {
                            sb.Append(" or (Grade>=");
                            sb.Append(output[0]);
                            sb.Append(" and Grade<");
                            sb.Append(output[1]);
                            sb.Append(") ");
                        }
                    }
                }
            }
            if (hotelCategoryId != null && hotelCategoryId.Length > 0)
            {
                bool isExit = true;
                string ids = string.Empty;
                for (int i = 0; i < hotelCategoryId.Length; i++)
                {
                    if (!EnumHelp.hotelCategoryId.Contains(hotelCategoryId[i]))
                    {
                        isExit = false;
                    }
                    else
                    {
                        ids += (i == 0 ? hotelCategoryId[i].ToString() : ("," + hotelCategoryId[i].ToString()));
                    }
                }
                if (isExit)
                {
                    sb.Append(" and HotelCategoryId in (");
                    sb.Append(ids);
                    sb.Append(")");
                }
            }
            sb.Append(" group by d.BusinessInfoId) z on y.BusinessInfoId=z.BusinessInfoId");

            total_count = this._repoBusinessInfo.SqlQuery(sb.ToString(), new String[] { }).ToList().Count;
            return this._repoBusinessInfo.SqlQuery(sb.ToString(), new String[] { }).Skip(page_Size * (page_Index - 1)).Take(page_Size).ToList();
            #region Test
            //IQueryable<BusinessInfo> iqb = _repoBusinessInfo.Table;
            //iqb = iqb.Where(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效 && p.BusinessTypeId == 4);

            ////IQueryable<BusinessInfo> iqb = DbContext;
            //var where = PredicateBuilder.True<BusinessInfo>();
            ////
            //IQueryable<BusinessInfo> iqb1 = _repoBusinessInfo.Table;
            //if (grade != null && grade.Length > 0)
            //{
            //    var where1 = PredicateBuilder.True<BusinessInfo>();
            //    for (int i = 0; i < grade.Length; i++)
            //    {
            //        var str = EnumHelp.HotelGrade[grade[i]];
            //        if (!string.IsNullOrWhiteSpace(str))
            //        {
            //            var aa = str.Split('-').ToArray();
            //            int[] output = Array.ConvertAll<string, int>(aa, delegate (string s) { return int.Parse(s); });
            //            //if (i == 0)
            //            // {
            //            iqb1 = iqb1.Where(c => c.Grade >= output[0] && c.Grade < output[1]);
            //            // }
            //            //else
            //            //{
            //            //    iqb1= iqb1.Where()
            //            //    where1 = where1.Or(c => c.Grade >= output[0] && c.Grade < output[1]);
            //            //}
            //        }
            //    }
            //    iqb1 = iqb1.Where(where1);
            //    iqb = iqb.Union(iqb1);
            //}
            //var list = iqb.ToList();
            /////酒店类别
            //if (hotelCategoryId != null && hotelCategoryId.Length > 0)
            //{
            //    where = where.And(c => hotelCategoryId.Contains(c.HotelCategoryId));
            //}
            //if (price != null && price.Length > 0)
            //{
            //    var where3 = PredicateBuilder.True<BusinessInfo>();
            //    for (int i = 0; i < price.Length; i++)
            //    {
            //        var str = EnumHelp.HotelPrice[price[i]];
            //        if (!string.IsNullOrWhiteSpace(str))
            //        {
            //            var aa = str.Split('-').ToArray();
            //            decimal[] output = Array.ConvertAll<string, decimal>(aa, delegate (string s) { return decimal.Parse(s); });
            //            if (i == 0)
            //            {
            //                where3 = where3.And(c => c.BusinessRoomList.All(m => m.RealPrice >= output[0] && m.RealPrice < output[1]));
            //            }
            //            else
            //            {
            //                where3 = where3.Or(c => c.BusinessRoomList.All(m => m.RealPrice >= output[0] && m.RealPrice < output[1]));
            //            }
            //        }
            //    }
            //    where = where.And(where3);
            //}
            //jun

            //var where = PredicateBuilder.True<BusinessInfo>();
            //var where1 = PredicateBuilder.True<BusinessInfo>();
            //var where2 = PredicateBuilder.True<BusinessInfo>();
            //var where3 = PredicateBuilder.True<BusinessInfo>();

            ////商家等级
            //foreach (var item in grade)
            //{
            //    if (item == 1)
            //    {
            //        where1 = where1.Or(p => p.Grade < 3);
            //    }
            //    else
            //    {
            //        where1 = where1.Or(p => p.Grade >= (item + 1) && p.Grade < (item + 2));
            //    }
            //}
            //where = where.And(where1);

            ////酒店分类
            //foreach (var item in hotelCategoryId)
            //{
            //    where2 = where2.Or(p => p.HotelCategoryList.Select(h => h.HotelCategoryId).Contains(item));
            //}
            //where = where.And(where2);

            ////房间价格
            //foreach (var item in price)
            //{
            //    if (item == 1)
            //    {
            //        where3 = where3.Or(p => p.BusinessRoomList.Any(r => r.RealPrice >= 0 && r.RealPrice < 100));
            //    }
            //    else if (item == 2)
            //    {
            //        where3 = where3.Or(p => p.BusinessRoomList.Any(r => r.RealPrice >= 100 && r.RealPrice < 300));
            //    }
            //    else if (item == 3)
            //    {
            //        where3 = where3.Or(p => p.BusinessRoomList.Any(r => r.RealPrice >= 300 && r.RealPrice < 500));
            //    }
            //    else if (item == 4)
            //    {
            //        where3 = where3.Or(p => p.BusinessRoomList.Any(r => r.RealPrice >= 500));
            //    }
            //}
            //where = where.And(where3);

            //where = where.And(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效 && p.BusinessTypeId == 4);
            //total_count = _repoBusinessInfo.Table.Where(where).ToList().Count;
            //return this._repoBusinessInfo.Table.Where(where).OrderBy(c => c.BusinessInfoId).Take(page_Size * page_Index).Skip((page_Index - 1) * page_Size).ToList(); 
            #endregion
        }

        /// <summary>
        /// 酒店筛选 多条件 定位
        /// </summary>
        /// <param name="grade"></param>
        /// <param name="hotelCategoryId"></param>
        /// <param name="price"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetListByLocationAndCond(int[] grade, int[] hotelCategoryId, int[] price, double longitude, double latitude, int page_Index, int page_Size, out int total_count)
        {
            StringBuilder sb = new StringBuilder(200);
            string sqlStr = @"select *,Round( CAST(CAST(sqrt((((#longitude-Longitude)*PI()*12656*cos(((22.568207+Latitude)/2)*PI()/180)/180) * ((#longitude-Longitude)*PI()*12656*cos (((22.568207+Latitude)/2)*PI()/180)/180)) + ( ((#latitude-Latitude)*PI()*12656/180) * ((#latitude-Latitude)*PI()*12656/180) ) ) AS DECIMAL(20,7)) AS VARCHAR(20)),3) as dis from (select y.* from BusinessInfo y inner join (select MAX(d.Grade) Grade,MAX(d.RoomId) RoomId,MAX(d.RealPrice) RealPrice,MAX(d.BusinessInfoId) BusinessInfoId,Max(h.HotelCategoryId) HotelCategoryId from (select b.BusinessInfoId, b.Grade, r.RoomId, r.RealPrice from BusinessInfo b right join Room r on b.BusinessInfoId = r.BusinessInfoId where b.BusinessTypeId = 4  and b.[Status]=1 and b.IsDelete=0) d inner join HotelRelateCategory h on d.BusinessInfoId = h.BusinessInfoId where 1 = 1 ";
            sb.Append(sqlStr);
            if (price != null && price.Length > 0)
            {
                for (int i = 0; i < price.Length; i++)
                {
                    var str = EnumHelp.HotelPrice[price[i]];
                    if (!string.IsNullOrWhiteSpace(str))//只有一个元素
                    {
                        var aa = str.Split('-').ToArray();
                        decimal[] output = Array.ConvertAll<string, decimal>(aa, delegate (string s) { return decimal.Parse(s); });
                        if (price.Length == 1)
                        {
                            sb.Append(" and (RealPrice>=");
                            sb.Append(output[0]);
                            sb.Append(" and RealPrice<");
                            sb.Append(output[1]);
                            sb.Append(") ");
                            continue;
                        }
                        else if (i == 0)//第一个
                        {
                            sb.Append(" and ((RealPrice>=");
                            sb.Append(output[0]);
                            sb.Append(" and RealPrice<");
                            sb.Append(output[1]);
                            sb.Append(") ");
                        }
                        else if (i == (price.Length - 1))//最后一个
                        {
                            sb.Append(" or (RealPrice>=");
                            sb.Append(output[0]);
                            sb.Append(" and RealPrice<");
                            sb.Append(output[1]);
                            sb.Append(")) ");
                        }
                        else
                        {
                            sb.Append(" or (RealPrice>=");
                            sb.Append(output[0]);
                            sb.Append(" and RealPrice<");
                            sb.Append(output[1]);
                            sb.Append(") ");
                        }
                    }
                }
            }
            if (grade != null && grade.Length > 0)
            {
                // and ((Grade>=1 and Grade<2) or (Grade>=4 and Grade<5)) 
                for (int i = 0; i < grade.Length; i++)
                {
                    var str = EnumHelp.HotelGrade[grade[i]];
                    if (!string.IsNullOrWhiteSpace(str))//只有一个元素
                    {
                        var aa = str.Split('-').ToArray();
                        int[] output = Array.ConvertAll<string, int>(aa, delegate (string s) { return int.Parse(s); });
                        if (grade.Length == 1)
                        {
                            sb.Append(" and (Grade>=");
                            sb.Append(output[0]);
                            sb.Append(" and Grade<");
                            sb.Append(output[1]);
                            sb.Append(") ");
                            continue;
                        }
                        else if (i == 0)//第一个
                        {
                            sb.Append(" and ((Grade>=");
                            sb.Append(output[0]);
                            sb.Append(" and Grade<");
                            sb.Append(output[1]);
                            sb.Append(") ");
                        }
                        else if (i == (grade.Length - 1))//最后一个
                        {
                            sb.Append(" or (Grade>=");
                            sb.Append(output[0]);
                            sb.Append(" and Grade<");
                            sb.Append(output[1]);
                            sb.Append(")) ");
                        }
                        else
                        {
                            sb.Append(" or (Grade>=");
                            sb.Append(output[0]);
                            sb.Append(" and Grade<");
                            sb.Append(output[1]);
                            sb.Append(") ");
                        }
                    }
                }
            }
            if (hotelCategoryId != null && hotelCategoryId.Length > 0)
            {
                bool isExit = true;
                string ids = string.Empty;
                for (int i = 0; i < hotelCategoryId.Length; i++)
                {
                    if (!EnumHelp.hotelCategoryId.Contains(hotelCategoryId[i]))
                    {
                        isExit = false;
                    }
                    else
                    {
                        ids += (i == 0 ? hotelCategoryId[i].ToString() : ("," + hotelCategoryId[i].ToString()));
                    }
                }
                if (isExit)
                {
                    sb.Append(" and HotelCategoryId in (");
                    sb.Append(ids);
                    sb.Append(")");
                }
            }
            sb.Append(" group by d.BusinessInfoId) z on y.BusinessInfoId=z.BusinessInfoId) b order by dis");
            string sql = sb.ToString().Replace("#longitude", longitude.ToString()).Replace("#latitude", latitude.ToString());
            total_count = this._repoBusinessInfo.SqlQuery(sql, new String[] { }).ToList().Count;
            return this._repoBusinessInfo.SqlQuery(sql, new String[] { }).Skip(page_Size * (page_Index - 1)).Take(page_Size).ToList();
        }

        #region MyRegion
        //public List<BusinessInfo> GetListByManCond(int module, int businessGroupId, decimal minPrice, decimal maxPrice, int hotelCategoryId, int page_Index, int page_Size, out int total_count)
        //{
        //    var where = PredicateBuilder.True<BusinessInfo>();
        //    //商家模块类型（食衣乐）

        //    if (module > 0)
        //    {
        //        where = where.And(m => m.BusinessTypeId == module);
        //    }
        //    // 商家分组
        //    if (businessGroupId > 0)
        //    {
        //        where = where.And(m => m.BusinessGroupId == businessGroupId);
        //    }
        //    if (minPrice>0)
        //    {
        //        where = where.And(m => m.AveragePay>= minPrice);
        //    }
        //    if (maxPrice>0)
        //    {
        //        where = where.And(m => m.AveragePay<= maxPrice);
        //    }
        //    if (hotelCategoryId>0)
        //    {
        //        where = where.And(m => m.HotelCategoryId == businessGroupId);
        //    }
        //    where = where.And(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效);
        //    total_count = _repoBusinessInfo.Table.Where(where).ToList().Count;
        //    return this._repoBusinessInfo.Table.Where(where).OrderBy(c => c.BusinessInfoId).Take(page_Size * page_Index).Skip((page_Index - 1) * page_Size).ToList();
        //}

        //select *,Round( CAST(CAST(sqrt((((114.11000-Longitude)*PI()*12656*cos(((22.568207+Latitude)/2)*PI()/180)/180) * ((114.11000-Longitude)*PI()*12656*cos (((22.568207+Latitude)/2)*PI()/180)/180)) + ( ((22.53000-Latitude)*PI()*12656/180) * ((22.53000-Latitude)*PI()*12656/180) ) ) AS DECIMAL(20,7)) AS VARCHAR(20)),3) as dis from (select y.* from BusinessInfo y inner join (select MAX(d.Grade) Grade,MAX(d.RoomId) RoomId,MAX(d.RealPrice) RealPrice,MAX(d.BusinessInfoId) BusinessInfoId,Max(h.HotelCategoryId) HotelCategoryId from (select b.BusinessInfoId, b.Grade, r.RoomId, r.RealPrice from BusinessInfo b right join Room r on b.BusinessInfoId = r.BusinessInfoId where b.BusinessTypeId = 4 and b.[Status]=1 and b.IsDelete=0) d inner join HotelRelateCategory h on d.BusinessInfoId = h.BusinessInfoId where 1 = 1  and (RealPrice>=0 and RealPrice<100)  and (Grade>=0 and Grade<3)  and HotelCategoryId in (1,2,4) group by d.BusinessInfoId) z on y.BusinessInfoId=z.BusinessInfoId) b where b.Status=1 and b.IsDelete =0 order by dis 
        #endregion
        public List<BusinessInfo> GetListByLocationAndCond(int module, double longitude, double latitude, int businessGroupId, decimal minPrice, decimal maxPrice, int hotelCategoryId, int page_Index, int page_Size, out int total_count)
        {
            //定义sql
            string sqlStr = @"select *,Round(
                            CAST(CAST(
                            sqrt(
                                (
                                 ((#longitude-Longitude)*PI()*12656*cos(((22.568207+Latitude)/2)*PI()/180)/180)  
                                 *  
                                 ((#longitude-Longitude)*PI()*12656*cos (((22.568207+Latitude)/2)*PI()/180)/180)  
                                )
                                +  
                                (  
                                 ((#latitude-Latitude)*PI()*12656/180)  
                                 *  
                                 ((#latitude-Latitude)*PI()*12656/180)  
                                ) 
                            )   
                            AS DECIMAL(20,7)) AS VARCHAR(20))
                            ,3)
                            as dis from BusinessInfo b where b.Status=1 and b.IsDelete =0 and  b.BusinessTypeId =#BusinessTypeId and b.BusinessGroupId=#BusinessGroupId";
            if (minPrice > 0)
            {
                sqlStr += " and b.AveragePay>=" + minPrice;
            }
            if (maxPrice > 0)
            {
                sqlStr += " and b.AveragePay<=" + maxPrice;
            }
            if (hotelCategoryId > 0)
            {
                sqlStr += " and b.HotelCategoryId=" + hotelCategoryId;
            }
            sqlStr += " order by dis";
            sqlStr = sqlStr.Replace("#longitude", longitude.ToString()).Replace("#latitude", latitude.ToString()).Replace("#BusinessTypeId", module.ToString()).Replace("#BusinessGroupId", businessGroupId.ToString());
            total_count = this._repoBusinessInfo.SqlQuery(sqlStr, new String[] { }).ToList().Count;
            return this._repoBusinessInfo.SqlQuery(sqlStr, new String[] { }).Skip(page_Size * (page_Index - 1)).Take(page_Size).ToList();
        }

        /// <summary>
        /// 获取置顶商家
        /// </summary>
        /// <returns></returns>
        public List<BusinessInfo> GetIndexTopBusinessInfo()
        {
            var where = PredicateBuilder.True<BusinessInfo>();
            //获取置顶商家
            where = where.And(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效 && p.IsTop == (int)IsTop.是 && p.BusinessTypeId != (int)EnumHelp.BusinessTypeEnum.食 && p.BusinessTypeId != (int)EnumHelp.BusinessTypeEnum.衣);
            return this._repoBusinessInfo.Table.Where(where).ToList();
        }

        /// <summary>
        /// 优惠商家
        /// </summary>
        /// <returns></returns>
        public List<BusinessInfo> GetIndexActivityBusinessInfo()
        {
            var where = PredicateBuilder.True<BusinessInfo>();
            //有折扣商家
            where = where.And(p => p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效 && p.BusinessTypeId != (int)EnumHelp.BusinessTypeEnum.食 && p.BusinessTypeId != (int)EnumHelp.BusinessTypeEnum.衣);
            where = where.And(p => p.ActivityDiscount.ActivityDiscountId > 0 || p.ActivityMinusList.Count > 0);
            return this._repoBusinessInfo.Table.Where(where).ToList();
        }

        /// <summary>
        /// 娱乐模块主页 模糊搜索 或附近商家
        /// </summary>
        /// <param name="search_Name"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetIndexListByLocation(string search_Name, double longitude, double latitude, int page_Index, int page_Size, out int total_count)
        {
            //定义sql
            string sqlStr = @"select *,Round(
                            CAST(CAST(
                            sqrt(
                                (
                                 ((#longitude-Longitude)*PI()*12656*cos(((22.568207+Latitude)/2)*PI()/180)/180)  
                                 *  
                                 ((#longitude-Longitude)*PI()*12656*cos (((22.568207+Latitude)/2)*PI()/180)/180)  
                                )
                                +  
                                (  
                                 ((#latitude-Latitude)*PI()*12656/180)  
                                 *  
                                 ((#latitude-Latitude)*PI()*12656/180)  
                                ) 
                            )   
                            AS DECIMAL(20,7)) AS VARCHAR(20))
                            ,3)
                            as dis from BusinessInfo b where b.Status=1 and b.IsDelete =0 and BusinessTypeId not in (1,2) ";
            if (!string.IsNullOrWhiteSpace(search_Name))
            {
                sqlStr += " and b.Name like '%" + search_Name + "%'";
            }
            sqlStr += " order by dis";
            sqlStr = sqlStr.Replace("#longitude", longitude.ToString()).Replace("#latitude", latitude.ToString());
            total_count = this._repoBusinessInfo.SqlQuery(sqlStr, new String[] { }).ToList().Count;
            return this._repoBusinessInfo.SqlQuery(sqlStr, new String[] { }).Skip(page_Size * (page_Index - 1)).Take(page_Size).ToList();
        }

        /// <summary>
        /// 衣模块获取商家列表
        /// </summary>
        /// <param name="module"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetGoodsBusinessInfoByModule(string search_Name, int module, out int total_count)
        {
            var where = PredicateBuilder.True<BusinessInfo>();
            if (!string.IsNullOrWhiteSpace(search_Name))
            {
                where = where.And(p => p.Name.Contains(search_Name));
            }
            where = where.And(p => p.BusinessTypeId == module && p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效);

            total_count = this._repoBusinessInfo.Table.Where(where).ToList().Count;
            //return this._repoBusinessInfo.Table.Where(where).GroupBy(c => c.BusinessTypeId).SelectMany(c => c.OrderBy(d=>d.BusinessInfoId).Skip(page_Size * (page_Index - 1)).Take(page_Size)).ToList();
            return this._repoBusinessInfo.Table.Where(where).ToList();
        }

        /// <summary>
        /// 根据商家分组，定位查询
        /// </summary>
        /// <param name="businessGroup_Id"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="module"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetGoodsBusinessInfoByGroupIdWithLocation(int businessGroup_Id,string District, double longitude, double latitude, int module, int page_Index, int page_Size, out int total_count)
        {
            //定义sql
            string sqlStr = @"select *,Round(
                            CAST(CAST(
                            sqrt(
                                (
                                 ((#longitude-Longitude)*PI()*12656*cos(((22.568207+Latitude)/2)*PI()/180)/180)  
                                 *  
                                 ((#longitude-Longitude)*PI()*12656*cos (((22.568207+Latitude)/2)*PI()/180)/180)  
                                )
                                +  
                                (  
                                 ((#latitude-Latitude)*PI()*12656/180)  
                                 *  
                                 ((#latitude-Latitude)*PI()*12656/180)  
                                ) 
                            )   
                            AS DECIMAL(20,7)) AS VARCHAR(20))
                            ,3)
                            as dis from BusinessInfo b where b.Status=1 and b.IsDelete =0 and BusinessTypeId= " + module;
            if (businessGroup_Id>0)
            {
                sqlStr += " and BusinessGroupId=" + businessGroup_Id;
            }
            if (string.IsNullOrWhiteSpace(District))
            {
                sqlStr += " and District=" + District;
            }
            
            sqlStr += " order by dis";
            sqlStr = sqlStr.Replace("#longitude", longitude.ToString()).Replace("#latitude", latitude.ToString());
            total_count = this._repoBusinessInfo.SqlQuery(sqlStr, new String[] { }).ToList().Count;
            return this._repoBusinessInfo.SqlQuery(sqlStr, new String[] { }).Skip(page_Size * (page_Index - 1)).Take(page_Size).ToList();
        }

        /// <summary>
        /// 获取主页优选商家/置顶商家
        /// </summary>
        /// <param name="search_Name"></param>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="module"></param>
        /// <param name="page_Index"></param>
        /// <param name="page_Size"></param>
        /// <param name="total_count"></param>
        /// <returns></returns>
        public List<BusinessInfo> GetIndexBusinessInfoIsTop(string search_Name, int module, int page_Index, int page_Size, out int total_count)
        {
            var where = PredicateBuilder.True<BusinessInfo>();
            if (!string.IsNullOrWhiteSpace(search_Name))
            {
                where = where.And(p => p.Name.Contains(search_Name));
            }
            where = where.And(p => p.BusinessTypeId == module && p.Status == (int)EnabledEnum.有效 && p.IsDelete == (int)IsDeleteEnum.有效&&p.IsTop==(int)EnumHelp.IsTop.是);

            total_count = this._repoBusinessInfo.Table.Where(where).ToList().Count;
            return this._repoBusinessInfo.Table.Where(where).OrderBy(c=>c.SortNo).Skip(page_Size * (page_Index - 1)).Take(page_Size).ToList();
        }
    }
}


