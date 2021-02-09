using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class RiceDateUserReputation
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 约饭次数
        /// </summary>
        public int RiceDateCount { get; set; }

        /// <summary>
        /// 投诉次数
        /// </summary>
        public int Complain { get; set; }


        public List<ComplainDetails> ComplainDetailsList { get; set; }


    }
    public class ComplainDetails {
        public string datetime { get; set; }

        public string BusinessName { get; set; }

        public string Content { get; set; }
    }
}