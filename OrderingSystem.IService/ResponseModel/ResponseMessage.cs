namespace OrderingSystem.IService.ResponseModel
{
    /// <summary>
    /// 响应消息
    /// </summary>
    public class ResponseMessage
    {
        public ResponseMessage()
        {
            Result = MessageResult.SUCCESS;
        }

        /// <summary>
        /// 消息ID
        /// </summary>
        /// <value>
        /// The message identifier.
        /// </value>
        public string MessageID { get; set; }

        /// <summary>
        /// 消息代码
        /// </summary>
        /// <value>
        /// The message code.
        /// </value>
        public string MessageCode { get; set; }

        /// <summary>
        /// 结果（成功、失败、未登录等）
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public MessageResult Result { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content { get; set; }
    }
}