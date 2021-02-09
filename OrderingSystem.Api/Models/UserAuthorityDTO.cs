using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class UserAuthorityDTO
    {
        /// <summary>
        /// openId
        /// </summary>
        [Required]
        [Display(Name = "OpenId")]
        public string OpenId { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Required]
        [Display(Name = "性别")]
        public int Sex { get; set; }

        /// <summary>
        ///出生日期
        /// </summary>
        [Required]
        [Display(Name = "出生年月")]
        [DataType(DataType.DateTime)]
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [Required]
        [Display(Name = "身份证号码")]
        public string CardNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [Display(Name = "身份证号码")]
        public string UserName { get; set; }
    }
}