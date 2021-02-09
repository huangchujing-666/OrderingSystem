using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    public class UploadImage
    {
        /// <summary>
        /// 64位图片编码
        /// </summary>
        public string Base64 { get; set; }

        /// <summary>
        /// 后缀类型
        /// </summary>
        public string SuffixType { get; set; }
    }
}