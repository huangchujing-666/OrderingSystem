
namespace OrderingSystem.Api.Models
{
    /// <summary>
    /// 响应模型
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseModel"/> class.
        /// </summary>
        public ResponseModel()
        {
            error_code = Result.SUCCESS;
            //zip = Flag.No;
            //encrypt = Flag.No; 
        }

        /// <summary>
        /// 处理结果
        /// </summary>
        /// <value>
        /// 成功或失败
        /// </value>
        public Result error_code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        /// <value>
        /// 消息内容
        /// </value>
        public string message { get; set; }

        public int total_count { get; set; }
        /// <summary>
        /// 是否压缩
        /// </summary>
        /// <value>
        /// 1表示gzip压缩,0表示未压缩
        /// </value>
        // public Flag zip { get; set; }

        /// <summary>
        /// 是否加密
        /// </summary>
        /// <value>
        /// 1表示返回报文已加密,0表示未加密
        /// </value>
        // public Flag encrypt { get; set; } 
    }

    /// <summary>
    /// 响应模型：指定返回数据
    /// </summary>
    /// <typeparam name="T">返回数据</typeparam>
    public class ResponseModel<T> : ResponseModel
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        /// <value>
        /// 数据，为null时返回空字条串
        /// </value>
        public T data { get; set; }
    }

    /// <summary>
    /// 结果
    /// </summary>
    public enum Result
    {
        /// <summary>
        /// 失败
        /// </summary>
        ERROR = 0,

        /// <summary>
        /// 成功
        /// </summary>
        SUCCESS = 1,
    }

    /// <summary>
    /// 是否标记
    /// </summary>
    public enum Flag
    {
        /// <summary>
        /// 是
        /// </summary>
        Yes = 1,

        /// <summary>
        /// 否
        /// </summary>
        No = 0,
    }
}