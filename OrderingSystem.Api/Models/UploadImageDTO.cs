using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderingSystem.Api.Models
{
    /// <summary>
    /// 上传图片返回id
    /// </summary>
    public class UploadImageDTO
    {
        public int baseImageId { get; set; }

        public string path { get; set; }
    }
}