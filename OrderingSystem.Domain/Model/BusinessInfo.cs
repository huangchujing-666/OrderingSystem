using System;
using System.Collections.Generic;

namespace OrderingSystem.Domain.Model
{
	public class BusinessInfo: IAggregateRoot
    {
		/// <summary>
		/// 主键
		/// </summary>
		public virtual int BusinessInfoId { get; set; }
		/// <summary>
		/// 商家名称
		/// </summary>
		public virtual string Name { get; set; }
        public virtual int BaseImageId { get; set; }
        public virtual int BaseAreaId { get; set; }
        public virtual int BaseLineId { get; set; }
        public virtual int BaseStationId { get; set; }
        /// <summary>
        /// 商家Logo
        /// </summary>
        public virtual BaseImage BaseImage { get; set; }
        /// <summary>
        /// 商家性质
        /// </summary>
        public virtual int BusinessTypeId { get; set; }
        /// <summary>
        /// 商家公告
        /// </summary>
        public virtual string Notic { get; set; }
        /// <summary>
        /// 商家简介
        /// </summary>
        public virtual string Introduction { get; set; }
        /// <summary>
        /// 商家地址
        /// </summary>
        public virtual string Address { get; set; }
        /// <summary>
        /// 商家营业时间
        /// </summary>
        public virtual string BusinessHour { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string Mobile { get; set; }
        /// <summary>
        /// 地图经度
        /// </summary>
        public virtual decimal Longitude { get; set; }
        /// <summary>
        /// 地图纬度
        /// </summary>
        public virtual decimal Latitude { get; set; }
        /// <summary>
        /// 月售单数
        /// </summary>
        public virtual int OrderCountPerMonth { get; set; }
        /// <summary>
        /// 人均消费
        /// </summary>
        public virtual decimal AveragePay { get; set; }
        /// <summary>
        /// 评分
        /// </summary>
        public virtual decimal Grade { get; set; }
        /// <summary>
        /// 距离最近地铁站距离
        /// </summary>
        public virtual string Distance { get; set; }

        /// <summary>
        /// 商家分组id
        /// </summary>
       public virtual int BusinessGroupId { get; set; }

        public virtual BusinessGroup BusinessGroup { get; set; }

       public virtual int BusinessEvaluationId { get; set; }
       public virtual int ActivityDiscountId { get; set; }

        

        /// <summary>
        /// 服务设施
        /// </summary>
        public virtual string Services { get; set; }
         
        /// <summary>
        /// 状态（0为无效 1为有效）
        /// </summary>
        public virtual int Status { get; set; }
        /// <summary>
        /// 是否删除（0为否 1为是）
        /// </summary>
        public virtual int IsDelete { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public virtual int SortNo { get; set; }
        /// <summary>
        /// 是否置顶
        /// </summary>
        public virtual int IsTop { get; set; }

        public virtual int CreatePersonId { get; set; }
        public virtual int EditPersonId { get; set; }
        public virtual DateTime EditTime { get; set; }
        public virtual DateTime CreateTime { get; set; }


        public virtual BusinessEvaluation BusinessEvaluation { get; set; }
        public virtual ActivityDiscount ActivityDiscount { get; set; }
        public virtual List<ActivityMinus> ActivityMinusList { get; set; }
        public virtual List<Dishes> DishesList { get; set; }
        public virtual List<BusinessImage> BusinessImageList { get; set; }


        public virtual List<Product> ProductList { get; set; }

        public virtual List<BusinessComment> BusinessCommentList { get; set; }

        public virtual List<HotelRelateCategory> HotelCategoryList { get; set; }


        //public virtual int HotelCategoryId { get; set; }

        //public virtual HotelCategory HotelCategory { get; set; }

        public virtual List<BusinessLable> BusinessLableList { get; set; }

        public virtual List<Room> BusinessRoomList { get; set; }

        public virtual List<Ticket> BusinessTicketList { get; set; }

        public virtual List<JourneyArticle> BusinessJourneyArticleList { get; set; }

        public virtual string District { get; set; }


        public virtual string OpenDate { get; set; }

        public virtual string RefreshDate { get; set; }

        public virtual string TotalRooms { get; set; }

        public virtual string TotalFloors { get; set; }

        /// <summary>
        /// 衣品列表
        /// </summary>
        public virtual List<Goods> BusinessGoodsList { get; set; }
    }
}

