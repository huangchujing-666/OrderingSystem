﻿namespace OrderingSystem.Http.Mos
{
    /// <summary>
    /// 
    /// </summary>
    public enum HttpMothed
    {
        /// <summary>
        /// Get
        /// </summary>
        GET = 0,

        /// <summary>
        /// post
        /// </summary>
        POST = 10,

        /// <summary>
        /// PUT
        /// </summary>
        PUT = 20,

        /// <summary>
        /// DELETE
        /// </summary>
        DELETE = 30,
        HEAD=40,
        OPTIONS=50,
        TRACE=60

    }

    /// <summary>
    /// 返回的状态
    /// </summary>
    public enum ResponseStatus
    {
        /// <summary>
        /// 没有响应
        /// </summary>
        None,
        /// <summary>
        /// 响应ok
        /// </summary>
        Completed,
        /// <summary>
        /// 响应出错
        /// </summary>
        Error,
        /// <summary>
        /// 响应出错但正确返回数据
        /// </summary>
        ErrorButResponse,
        /// <summary>
        /// 超时
        /// </summary>
        TimedOut,
        /// <summary>
        /// 取消
        /// </summary>
        Aborted
    }
}
