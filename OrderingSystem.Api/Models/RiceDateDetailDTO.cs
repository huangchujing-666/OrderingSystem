using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class RiceDateDetailDTO
    {
        /// <summary>
        /// 发布用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 发布用户性别
        /// </summary>
        public int Usex { get; set; }

        /// <summary>
        /// 发布用户年龄
        /// </summary>
        public int Uage { get; set; }
        /// <summary>
        /// 发布人的头像路径
        /// </summary>
        public string UserImage { get; set; }

        /// <summary>
        /// 用户昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 约饭id
        /// </summary>
        public int RiceDateId { get; set; }

        /// <summary>
        /// 约饭报名Id
        /// </summary>
        public int RiceDateUserId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 年龄范围
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 用餐人数
        /// </summary>
        public int UseCount { get; set; }

        /// <summary>
        /// 用餐时间
        /// </summary>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Zone { get; set; }

        /// <summary>
        /// 商家名称
        /// </summary>
        public string BusinessName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 口味
        /// </summary>
        public string Taste { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public string PayWay { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 图片路径集合
        /// </summary>
        public List<string> ImagePath { get; set; }

        /// <summary>
        /// 投诉次数
        /// </summary>
        public int ComplainCount { get; set; }

        /// <summary>
        /// 投诉信息
        /// </summary>
        public List<ComplainDetail> ComplainDetailList { get; set; }
        /// <summary>
        /// 约饭次数
        /// </summary>
        public int DateCount { get; set; }

        /// <summary>
        /// 是否已经报名 1是 0否
        /// </summary>
        public int IsDate { get; set; }

        /// <summary>
        /// 是否能投诉 1是 0否
        /// </summary>
        public int IsComplain { get; set; }

        /// <summary>
        /// 报名人数
        /// </summary>
        public int JoinCount { get; set; }

        /// <summary>
        /// 当前约饭状态
        /// </summary>
        public int RiceDateStatu { get; set; }

        /// <summary>
        /// 报名时间
        /// </summary>
        public DateTime JoinTime { get; set; }
    }

    public class ComplainDetail
    {
        public string NickName { get; set; }

        public string Content { get; set; }
    }
}