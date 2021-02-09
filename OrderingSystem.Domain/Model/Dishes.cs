using System;
using System.Collections.Generic;

namespace OrderingSystem.Domain.Model
{
    public class Dishes : IAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int DishesId { get; set; }
        /// <summary>
        /// 菜品名称
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 菜品描述
        /// </summary>
        public virtual string Descript { get; set; }
        /// <summary>
        /// 原价
        /// </summary>
        public virtual decimal OrignPrice { get; set; }
        /// <summary>
        /// 现价
        /// </summary>
        public virtual decimal RealPrice { get; set; }
        /// <summary>
        /// 菜品图片Id
        /// </summary>
        public virtual int BaseImageId { get; set; }
        /// <summary>
        /// 菜品类别Id
        /// </summary>
        public virtual int DishesCategoryId { get; set; }
        /// <summary>
        /// 月售份数
        /// </summary>
        public virtual int SellCountPerMonth { get; set; }

        public virtual int BusinessInfoId { get; set; }

        public virtual int Status { get; set; }
        public virtual int IsDelete { get; set; }

        public virtual int CreatePersonId { get; set; }
        public virtual int EditPersonId { get; set; }
        public virtual DateTime EditTime { get; set; }
        public virtual DateTime CreateTime { get; set; }


        public virtual BaseImage BaseImage { get; set; }

        public virtual BusinessInfo BusinessInfo { get; set; }

        /// <summary>
        /// 菜品类别
        /// </summary>
        public virtual DishesCategory DishesCategory { get; set; }

        public virtual List<DishesLable> DishesLableList { get; set; }
        public virtual List<DishesSpec> DishesSpecList { get; set; }

        public virtual List<DishesRelateLable> DishesRelateLableList { get; set; }
        public virtual List<DishesRelateSpec> DishesRelateSpecList { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl
        {
            get
            {
                if (BaseImage != null)
                {
                    return BaseImage.Source + BaseImage.Path;
                }
                return string.Empty;
            }
        }
    }
}

