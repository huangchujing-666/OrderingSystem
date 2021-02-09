namespace OrderingSystem.WX
{
    /// <summary>
    /// 微信支付配置类
    /// </summary>
    public class WxPayConfig
    {
        /// <summary>
        /// 微信商户证书
        /// 证书路径,注意应该填写绝对路径（仅退款、撤销订单时需要）
        /// </summary>
        public static string SSLCERT_PATH = "/cert/apiclient_cert.p12";
        /// <summary>
        /// 商户id
        /// </summary>
        public static string SSLCERT_PASSWORD = "1245718702";

        /// <summary>
        /// 初始化微信支付配置
        /// </summary>
        public static WxPayCenterConfig config = new WxPayCenterConfig()
        {
            AppSource = "yssg",
            AppId = "wx71cb72a4467120da",
            MchId = "1245718702",
            Key = "b20f92db10f4afdb6ac1fa91c0b7dabe",
            NotifyUrl = "http://mapi.leadyssg.com/api/OrderApi/WxCallback",
            //NotifyUrl = "http://gp.leadyssg.com/food/WxCallback",
        };
    }
}
