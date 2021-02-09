using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class RiceDateDTO
    {
        [Required]
        [Display(Name = "用户Id")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "约饭对象性别")]
        public int Sex { get; set; }

        [Required]
        [Display(Name = "年龄范围")]
        public string Age { get; set; }

        [Required]
        [Display(Name = "用餐人数")]
        public int UseCount { get; set; }


        [Required]
        [Display(Name = "约饭时间")]
        public DateTime BeginDate { get; set; }

        [Required]
        [Display(Name = "约饭区域")]
        public string Zone { get; set; }

        [Required]
        [Display(Name = "约饭商家")]
        public string BusinessName { get; set; }

        [Required]
        [Display(Name = "约饭地址")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "口味")]
        public string Taste { get; set; }

        [Required]
        [Display(Name = "费用支付")]
        public string PayWay { get; set; }

        public string Remark { get; set; }
        [Required]
        [Display(Name = "展示图片")]
        public string BaseImageIds { get; set; }
    }
}