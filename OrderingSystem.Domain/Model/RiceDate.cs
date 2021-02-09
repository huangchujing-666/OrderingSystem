using System;
using System.Collections.Generic;

namespace OrderingSystem.Domain.Model
{
    public class RiceDate : IAggregateRoot
    { 
        public virtual int RiceDateId { get; set; }
       
        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public virtual int UserId { get; set; }
        /// <summary>
        /// 参与用户性别
        /// </summary>
        public virtual int Sex { get; set; }
        /// <summary>
        /// 参与用户年龄
        /// </summary>
        public virtual string Age { get; set; }
        /// <summary>
        /// 参与用户数量
        /// </summary>
        public virtual int UserCount { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public virtual DateTime BeginDate { get; set; }
        /// <summary>
        /// 活动区域
        /// </summary>
        public virtual string Zone { get; set; }
        /// <summary>
        /// 活动地点
        /// </summary>
        public virtual string BusinessName { get; set; }
        /// <summary>
        /// 活动地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 口味
        /// </summary>
        public virtual string Taste { get; set; }
        /// <summary>
        /// 费用方式
        /// </summary>
        public virtual string PayWay { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
        public virtual DateTime  CreateTime { get; set; }
        public virtual DateTime EditTime { get; set; }
        /// <summary>
        /// 相关图片
        /// </summary>
        public virtual string BaseImageIds { get; set; }

         /// <summary>
         /// 状态
         /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        public virtual int IsDelete { get; set; }

        public virtual List<RiceDateUser> RiceDateUserList { get; set; }

        //public string ShowStatus
        //{
        //    get
        //    {
        //        if (Status==(int)EnumHelp.EnabledEnum.有效)
        //        {
        //            if (true)
        //            {

        //            }
        //        }
        //        else
        //        {
        //            return "已下架";
        //        }

        //        return "";
        //    }
        //}

    }
}