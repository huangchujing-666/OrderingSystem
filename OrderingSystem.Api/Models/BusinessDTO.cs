using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    /// <summary>
    /// 商家信息
    /// </summary>
    public class BusinessDTO
    {
        /// <summary>
        /// 商家Id
        /// </summary>
        public int business_id { get; set; }
        /// <summary>
        /// 商家名称
        /// </summary>
        public string business_name { get; set; }
        /// <summary>
        /// 商家图片id
        /// </summary>
        public int business_img_id { get; set; }

        /// <summary>
        /// 商家图片路径
        /// </summary>
        public string business_img_path { get; set; }
        /// <summary>
        /// 商家评分
        /// </summary>
        public string business_rank { get; set; }

        /// <summary>
        /// 商家评分int
        /// </summary>
        public decimal grade { get; set; }

        /// <summary>
        /// 月售订单
        /// </summary>
        public string month_order_count { get; set; }
        /// <summary>
        /// 人均消费金额
        /// </summary>
        public string average_pay { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string latitude { get; set; }
        /// <summary>
        /// 商家距离
        /// </summary>
        public int distance { get; set; }
        /// <summary>
        /// 折扣信息
        /// </summary>
        public string discount_info { get; set; }
        /// <summary>
        ///  满减信息
        /// </summary>
        public string manjian_info { get; set; }

        /// <summary>
        /// 商家公告
        /// </summary>
        public string notic { get; set; }

        /// <summary>
        /// 商家简介
        /// </summary>
        public string introduction { get; set; }
        /// <summary>
        /// 推荐菜品列表
        /// </summary>
        public List<DishesDTO> dishes_list { get; set; }
        public List<DishesCategoryDTO> dishes_cate_list { get; set; }

        /// <summary>
        /// 实景图列表
        /// </summary>
        public List<BaseImageDTO> base_VR_image_list { get; set; }
        /// <summary>
        /// 商家展示
        /// </summary>
        public List<BaseImageDTO> base_BN_image_list { get; set; }

        /// <summary>
        /// 商家全景图
        /// </summary>
        public List<BaseImageDTO> base_PR_image_list { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// 营业时间
        /// </summary>
        public string business_hour { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string mobile { get; set; }
        #region 娱乐模块字段
        /// <summary>
        /// 距离描述
        /// </summary>
        public string distance_descript { get; set; }

        /// <summary>
        /// 用户点评数量
        /// </summary>
        public int comment_count { get; set; }

        /// <summary>
        ///下单数量
        /// </summary>
        public int order_count { get; set; }

        /// <summary>
        /// 好评比率
        /// </summary>
        public int good_comment_rate { get; set; }

        /// <summary>
        /// 多少元起
        /// </summary>
        public string min_consume { get; set; }

        /// <summary>
        /// 服务设施
        /// </summary>
        public string service { get; set; }

        /// <summary>
        /// 产品列表
        /// </summary>
        public List<ProductDTO> product_list { get; set; }

        /// <summary>
        /// 环境评分
        /// </summary>
        public string environment_grade { get; set; }

        /// <summary>
        /// 服务评分
        /// </summary>
        public string service_grade { get; set; }

        /// <summary>
        /// 设施评分
        /// </summary>
        public string facilities_grade { get; set; }
        public List<ActivityStrDTO> activitylist { get; set; }

        /// <summary>
        /// 附近商家
        /// </summary>
        public List<BusinessDTO> Nearby { get; set; }

        /// <summary>
        /// 商家类型
        /// </summary>
        public int business_type_id { get; set; }
        #endregion


        #region 酒店字段
        /// <summary>
        /// 酒店标签
        /// </summary>
        public List<string> BusinessLable { get; set; }

        /// <summary>
        /// 房间集合
        /// </summary>
        public List<RoomDTO> Room_list { get; set; }

        public string open_date { get; set; }

        public string refresh_date { get; set; }

        public string total_rooms { get; set; }

        public string total_floors { get; set; }
        #endregion

        #region 景点
        /// <summary>
        /// 地区名称
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// 景点门票集合
        /// </summary>
        public List<TicketDTO> Ticket_list { get; set; }

        /// <summary>
        /// 文章列表
        /// </summary>
        public List<JourneyArticleDTO> JourneyArticle_list { get; set; }

        public int is_update { get; set;}

        #endregion

        /// <summary>
        /// 衣品列表
        /// </summary>
        public List<GoodsDTO> goods_list { get; set; }


        public List<BaseDicDTO> goods_cate_list { get; set; }
    }
    public class ActivityStrDTO
    {
        public int type { get; set; }

        public int id { get; set; }

        public string activity_descript { get; set; }
    }

}