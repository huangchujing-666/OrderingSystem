namespace OrderingSystem.IService.ResponseModel
{
    /// <summary>
    /// 响应结果
    /// </summary>
    public enum MessageResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        SUCCESS = 1,

        /// <summary>
        /// 失败
        /// </summary>
        FAILED = 2,

        /// <summary>
        /// 未登录
        /// </summary>
        OFFLINE = 3
    }
}
