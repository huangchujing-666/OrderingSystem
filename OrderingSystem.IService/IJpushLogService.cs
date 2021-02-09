﻿using OrderingSystem.Domain.Model;
using OrderingSystem.IService.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.IService
{
    public interface IJpushLogService
    {
        #region ***jpush推送服务***
        /// <summary>
        /// 推送消息给所有APP端
        /// </summary>
        /// <param name="request">request.PushMsg</param>
        /// <returns></returns>
        SnsResponse JpushSendToAll(SnsRequest request);

        /// <summary>
        /// 推送消息给别名用户
        /// </summary>
        /// <param name="request">request.PushUsers 以,分隔的多个别名</param>
        /// <returns></returns>
        SnsResponse JpushSendToAlias(SnsRequest request);

        /// <summary>
        /// 推送消息给安卓客户端
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SnsResponse JpushSendToAndroid(SnsRequest request);

        /// <summary>
        /// 推送消息给Ios客户端
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SnsResponse JpushSendToIos(SnsRequest request);

        /// <summary>
        /// 推送消息给标签用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        SnsResponse JpushSendToTag(SnsRequest request);

        /// <summary>
        /// 检测某条消息是否推送成功
        /// </summary>
        /// <param name="request">request.PushMsgId</param>
        /// <returns></returns>
        SnsResponse JpushSendCheck(SnsRequest request);
        #endregion
        JpushLog Insert(JpushLog model);
    }
}
