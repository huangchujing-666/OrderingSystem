using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    /// <summary>
    /// 菜品
    /// </summary>
    public class DishesDTO
    {
        /// <summary>
        /// 子订单Id
        /// </summary>
        public int order_detail_id { get; set; }
        /// <summary>
        /// 菜品Id
        /// </summary>
        public int dishes_id { get; set; }
        /// <summary>
        /// 菜品名称
        /// </summary>
        public string dishes_name { get; set; }
        /// <summary>
        /// 商家图片id
        /// </summary>

        public string dishes_img_id { get; set; }
        /// <summary>
        /// 商家图片路径
        /// </summary>
        public string dishes_img_path { get; set; }
        /// <summary>
        /// 售价
        /// </summary>
        public decimal dishes_orign_price { get; set; }
        /// <summary>
        /// 折扣价
        /// </summary>
        public decimal dishes_real_price { get; set; }

        /// <summary>
        /// 菜品详情
        /// </summary>
        public string descript { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int dishes_count { get; set; }
        /// <summary>
        /// 月销售数量
        /// </summary>
        public int month_sale_count { get; set; }
        /// <summary>
        /// 菜品标签，用英文“,”分隔
        /// </summary>
        public List<string> tags { get; set; }
        /// <summary>
        /// 规格参数列表
        /// </summary>
        public List<DishesSpecDTO> specs { get; set; }

    }
    /// <summary>
    /// 菜品分类数据模型
    /// </summary>
    public class DishesCategoryDTO
    {
        public DishesCategoryDTO()
        {
            dishes_list = new List<DishesDTO>();
        }

        public int category_id { get; set; }
        public string category_name { get; set; }

        public List<DishesDTO> dishes_list { get; set; }
    }
    /// <summary>
    /// 规格参数
    /// </summary>
    public class DishesSpecDTO
    {
        public DishesSpecDTO()
        {
            detail_list = new List<DishesSpecDetailDTO>();
        }
        public int spec_id { get; set; }
        public string spec_name { get; set; }

        public List<DishesSpecDetailDTO> detail_list { get; set; }
    }

    /// <summary>
    /// 规格参数详情
    /// </summary>
    public class DishesSpecDetailDTO
    {
        public int spec_id { get; set; }
        public int specdetail_id { get; set; }
        public string descript { get; set; }
        /// <summary>
        /// 售价
        /// </summary>
        public decimal dishes_orign_price { get; set; }
        /// <summary>
        /// 折扣价
        /// </summary>
        public decimal dishes_real_price { get; set; }

    }
}