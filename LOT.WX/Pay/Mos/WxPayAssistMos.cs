namespace OrderingSystem.WX.Pay.Mos
{

    #region  短链操作实体

    /// <summary>
    ///  获取短链请求实体
    /// </summary>
    public class WxPayGetShortUrlResp : WxPayBaseResp
    {
        /// <summary>   
        ///    URL链接 必填 String(64) 转换后的URL
        /// </summary>  
        public string short_url { get; set; }

        /// <summary>
        /// 格式化自身属性部分
        /// </summary>
        protected override void FormatPropertiesFromMsg()
        {
            base.FormatPropertiesFromMsg();
            short_url = this["short_url"];
        }
    }

    #endregion


    #region  授权码查询OPENID实体

    /// <summary>
    ///  获取短链请求实体
    /// </summary>
    public class WxPayAuthCodeOpenIdResp : WxPayBaseResp
    {
        /// <summary>   
        ///    用户标识 必填 String(128)
        /// </summary>  
        public string openid { get; set; }

        /// <summary>
        /// 格式化自身属性部分
        /// </summary>
        protected override void FormatPropertiesFromMsg()
        {
            base.FormatPropertiesFromMsg();
            openid = this["openid"];
        }
    }


    #endregion
}
