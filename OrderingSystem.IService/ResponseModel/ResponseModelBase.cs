namespace OrderingSystem.IService.ResponseModel
{
    /// <summary>
    /// 响应基类
    /// </summary>
    public class ResponseModelBase
    {
        public ResponseModelBase()
        {
            Message = new ResponseMessage();
        }

        /// <summary>
        /// 响应消息
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public ResponseMessage Message { get; set; }

        /// <summary>
        /// 分页查询时的总记录数
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public int TotalCount { get; set; }

        /// <summary>
        /// 分页查询的总页数
        /// </summary>
        /// <value>
        /// The total pages.
        /// </value>
        public int TotalPages { get; set; }

        /// <summary>
        /// 上传图片后文件的存放路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets a value indicating whether this instance is success.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is success; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccess
        {
            get
            {
                return Message.Result == MessageResult.SUCCESS;
            }
            set
            {
                if (value)
                {
                    Message.Result = MessageResult.SUCCESS;
                }
                else
                {
                    Message.Result = MessageResult.FAILED;
                }
            }
        }

        /// <summary>
        /// Gets or sets the content of the message.
        /// </summary>
        /// <value>
        /// The content of the message.
        /// </value>
        public string MessageContent
        {
            get
            {
                return Message.Content;
            }
        }
    }
}
