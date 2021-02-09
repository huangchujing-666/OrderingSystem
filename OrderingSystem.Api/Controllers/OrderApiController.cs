using LOT.WX.Pay;
using OrderingSystem.Api.App_Start;
using OrderingSystem.Api.Models;
using OrderingSystem.Cache.Redis;
using OrderingSystem.Common.SMS;
using OrderingSystem.Core.Infrastructure;
using OrderingSystem.Core.Utils;
using OrderingSystem.Domain;
using OrderingSystem.Domain.Model;
using OrderingSystem.IService;
using OrderingSystem.IService.ResponseModel;
using OrderingSystem.Service;
using OrderingSystem.WX.Pay.Mos;
using OrderingSystem.ZFB.Pay.Mos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using static OrderingSystem.Domain.EnumHelp;

namespace OrderingSystem.Api.Controllers
{
    public class OrderApiController : ApiController
    {
        private readonly IOrderService _OrderService = EngineContext.Current.Resolve<IOrderService>();
        private readonly IOrderStatusLogService _OrderStatusLogService = EngineContext.Current.Resolve<IOrderStatusLogService>();
        private readonly IBusinessCommentService _businessCommentService = EngineContext.Current.Resolve<IBusinessCommentService>();
        private readonly ICommentImageService _commentImageService = EngineContext.Current.Resolve<ICommentImageService>();
        private readonly IUserService _userService = EngineContext.Current.Resolve<IUserService>();
        private readonly IBusinessInfoService _businessInfoService = EngineContext.Current.Resolve<IBusinessInfoService>();
        private readonly IOrderDetailService _orderDetailService = EngineContext.Current.Resolve<IOrderDetailService>();
        private readonly IPayDetailService _payDetailService = EngineContext.Current.Resolve<IPayDetailService>();
        private readonly IWXPayService _wXPayService = EngineContext.Current.Resolve<IWXPayService>();
        private readonly IZFBPayService _zFBPayService = EngineContext.Current.Resolve<IZFBPayService>();
        private readonly IDishesSpecDetailService _dishesSpecDetailService = EngineContext.Current.Resolve<IDishesSpecDetailService>();
        private readonly IRoomService _RoomService = EngineContext.Current.Resolve<IRoomService>();
        private readonly ITicketService _TicketService = EngineContext.Current.Resolve<ITicketService>();
        private readonly ICustomerService _CustomerService = EngineContext.Current.Resolve<ICustomerService>();
        private readonly IOrderCustomerService _OrderCustomerService = EngineContext.Current.Resolve<IOrderCustomerService>();
        private readonly IJpushLogService _JpushLogService = EngineContext.Current.Resolve<IJpushLogService>();

        /// <summary>
        /// 根据用户Id获取订单列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Module">1食 2衣 3娱乐 4酒店 5景点 6除食之外的</param>
        /// <param name="Page_Index"></param>
        /// <param name="Page_Size"></param>
        /// <returns></returns>
        public ResponseModel<List<OrderDTO>> GetOrderByUserId(int UserId, int Module, int Page_Index = 1, int Page_Size = 20)
        {
            var result = new ResponseModel<List<OrderDTO>>();
            result.error_code = Result.SUCCESS;

            if (UserId > 0)
            {
                int totalCount = 0;
                var getlist = _OrderService.GetOrderListByUserId(UserId, Module, Page_Index, Page_Size, out totalCount);
                List<OrderDTO> list = null;
                if (getlist != null)
                {
                    list = new List<OrderDTO>();
                    foreach (var item in getlist)
                    {
                        string is_time_over = null;
                        if (Module == (int)EnumHelp.BusinessTypeEnum.食)
                        {
                            if (item.OrderStatusId == (int)EnumHelp.OrderStatus.未付款)
                            {
                                is_time_over = "待支付";
                            }
                            else if (item.OrderStatusId == (int)EnumHelp.OrderStatus.已付款 || item.OrderStatusId == (int)EnumHelp.OrderStatus.已结算)
                            {
                                is_time_over = "已支付";
                            }
                        }
                        else
                        {
                            TimeSpan timeSpan = System.DateTime.Now - item.OrderTime;
                            //15分钟有效时间
                            if (timeSpan.TotalMinutes >= 15 && item.OrderStatusId == (int)EnumHelp.OrderStatus.未付款)
                            {
                                is_time_over = "支付超时";
                            }
                            else if (item.OrderStatusId == (int)EnumHelp.OrderStatus.已付款)
                            {
                                is_time_over = "已支付";
                            }
                            else
                            {
                                is_time_over = "待支付";
                            }
                        }

                        list.Add(new OrderDTO()
                        {
                            business_id = item.BusinessInfoId,
                            order_no = item.OrderNo,
                            order_id = item.OrderId,
                            order_real_price = item.RealAmount.ToString(),
                            order_time = item.OrderTime.ToString(),
                            order_status = item.OrderStatus == null ? "" : item.OrderStatus.Name,
                            business_img_id = item.BusinessInfo.BaseImageId.ToString(),
                            business_img_path = item.BusinessInfo.BaseImage == null ? "" : item.BusinessInfo.BaseImage.Source + item.BusinessInfo.BaseImage.Path,
                            business_name = item.BusinessInfo.Name,
                            is_time_over = is_time_over,
                            module = item.BusinessInfo == null ? 0 : item.BusinessInfo.BusinessTypeId
                        });
                    }
                    result.data = list;
                    result.total_count = totalCount;
                    // RedisDb._redisHelper_11().StringSet(key, list, RedisConfig._defaultExpiry);
                }
            }
            else
            {
                result.error_code = Result.ERROR;
                result.message = "参数无效";
            }
            return result;
        }

        /// <summary>
        /// 根据订单状态用户Id获取订单列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public ResponseModel<List<OrderDTO>> GetOrderListByOderStatusId(int OrderStatusId = 0, int Module = 0, int UserId = 0, int Page_Index = 1, int Page_Size = 20)
        {
            var result = new ResponseModel<List<OrderDTO>>();
            result.error_code = Result.SUCCESS;

            if (UserId <= 0)
            {
                result.error_code = Result.ERROR;
                result.message = "参数有误";
                result.total_count = 0;
                return result;
            }
            //string key = "GetOrderListByOderStatusId" + OrderStatusId + UserId;
            //if (RedisDb._redisHelper_11().KeyExists(key))
            //{
            //    var data = RedisDb._redisHelper_11().StringGet<List<OrderDTO>>(key);
            //    result.data = data;
            //    result.total_count = data.Count;
            //}
            //else
            //{
            int totalCount = 0;
            var getlist = _OrderService.GetOrderListByOderStatusId(OrderStatusId, Module, UserId, Page_Index, Page_Size, out totalCount);
            List<OrderDTO> list = null;
            if (getlist != null)
            {
                list = new List<OrderDTO>();
                foreach (var item in getlist)
                {
                    string is_time_over = null;
                    if (Module == (int)EnumHelp.BusinessTypeEnum.食)
                    {
                        if (item.OrderStatusId == (int)EnumHelp.OrderStatus.未付款)
                        {
                            is_time_over = "待支付";
                        }
                        else if (item.OrderStatusId == (int)EnumHelp.OrderStatus.已付款 || item.OrderStatusId == (int)EnumHelp.OrderStatus.已结算)
                        {
                            is_time_over = "已支付";
                        }
                    }
                    else
                    {
                        TimeSpan timeSpan = System.DateTime.Now - item.OrderTime;
                        //15分钟有效时间
                        if (timeSpan.TotalMinutes >= 15 && item.OrderStatusId == (int)EnumHelp.OrderStatus.未付款)
                        {
                            is_time_over = "支付超时";
                        }
                        else if (item.OrderStatusId == (int)EnumHelp.OrderStatus.已付款)
                        {
                            is_time_over = "已支付";
                        }
                        else
                        {
                            is_time_over = "待支付";
                        }
                    }
                    list.Add(new OrderDTO()
                    {
                        business_id = item.BusinessInfoId,
                        order_no = item.OrderNo,
                        order_id = item.OrderId,
                        order_real_price = item.RealAmount.ToString(),
                        order_time = item.OrderTime.ToString(),
                        order_status = item.OrderStatus == null ? "" : item.OrderStatus.Name,
                        business_img_id = item.BusinessInfo == null ? "" : item.BusinessInfo.BaseImageId.ToString(),
                        business_img_path = item.BusinessInfo == null ? "" : (item.BusinessInfo.BaseImage == null ? "" : item.BusinessInfo.BaseImage.Source + item.BusinessInfo.BaseImage.Path),
                        business_name = item.BusinessInfo == null ? "" : item.BusinessInfo.Name,
                        is_time_over = is_time_over,
                        is_comment = (item.BusinessCommentList != null && item.BusinessCommentList.Count > 0) ? 1 : 0,
                        module = item.BusinessInfo == null ? 0 : item.BusinessInfo.BusinessTypeId
                    });
                }
                result.data = list;
                result.total_count = totalCount;
                // RedisDb._redisHelper_11().StringSet(key, list, RedisConfig._defaultExpiry);
            }
            // }
            return result;
        }


        /// <summary>
        /// 根据订单编号获取订单详情信息
        /// </summary>
        /// <param name="Order_No">订单编号</param>
        /// <returns></returns>
        public ResponseModel<OrderDetailDTO> GetDetailByOrderNo(string Order_No)
        {
            var result = new ResponseModel<OrderDetailDTO>();
            result.error_code = Result.SUCCESS;
            if (!string.IsNullOrWhiteSpace(Order_No))
            {
                //string key = "GetDetailByOrderNo" + Order_No;
                //if (RedisDb._redisHelper_11().KeyExists(key))
                //{
                //    var data = RedisDb._redisHelper_11().StringGet<OrderDetailDTO>(key);
                //    result.data = data;
                //    result.total_count = 1;
                //}
                //else
                //{
                var getResult = _OrderService.GetDetailByOrderNo(Order_No);
                var payDetail = _payDetailService.GetDetailByOrderNo(Order_No);
                //子订单列表
                var orderDetailList = _orderDetailService.GetListByOrderNo(Order_No);
                if (getResult != null && payDetail != null && orderDetailList != null)
                {
                    string is_time_over = "";
                    //TimeSpan timeSpan = System.DateTime.Now - getResult.OrderTime;

                    if (getResult.BusinessInfo.BusinessTypeId == (int)EnumHelp.BusinessTypeEnum.食)
                    {
                        if (getResult.OrderStatusId == (int)EnumHelp.OrderStatus.未付款)
                        {
                            is_time_over = "待支付";
                        }
                        else if (getResult.OrderStatusId == (int)EnumHelp.OrderStatus.已付款 || getResult.OrderStatusId == (int)EnumHelp.OrderStatus.已结算)
                        {
                            is_time_over = "已支付";
                        }
                    }
                    else
                    {
                        TimeSpan timeSpan = System.DateTime.Now - getResult.OrderTime;
                        //15分钟有效时间
                        if (timeSpan.TotalMinutes >= 15 && getResult.OrderStatusId == (int)EnumHelp.OrderStatus.未付款)
                        {
                            is_time_over = "支付超时";
                        }
                        else if (getResult.OrderStatusId == (int)EnumHelp.OrderStatus.已付款)
                        {
                            is_time_over = "已支付";
                        }
                        else
                        {
                            is_time_over = "待支付";
                        }
                    }

                    int night_count = 0;
                    if (getResult.UseFromDate != null && getResult.UseEndDate != null)
                    {
                        //DateTime startTime = Convert.ToDateTime("2007-04-11 15:53:08");
                        //DateTime endTime = Convert.ToDateTime("2007-05-12 16:54:19");
                        TimeSpan ts = getResult.UseEndDate - getResult.UseFromDate;
                        night_count = ts.Days;
                    }
                    decimal order_discount_price = 0M;
                    if (getResult.OrderDetailList != null && getResult.OrderDetailList.Count > 0)
                    {
                        foreach (var item in getResult.OrderDetailList)
                        {
                            order_discount_price += (item.Dishes.OrignPrice - item.Dishes.OrignPrice);
                        }

                    }
                    var orderDetail = new OrderDetailDTO()
                    {
                        business_id = getResult.BusinessInfoId,
                        order_no = getResult.OrderNo,
                        order_id = getResult.OrderId,
                        order_real_price = getResult.RealAmount.ToString(),
                        order_time = getResult.OrderTime.ToString(),
                        order_status = getResult.OrderStatus == null ? "" : getResult.OrderStatus.Name,
                        business_img_id = getResult.BusinessInfo.BaseImageId.ToString(),
                        business_img_path = getResult.BusinessInfo.BaseImage == null ? "" : getResult.BusinessInfo.BaseImage.Source + getResult.BusinessInfo.BaseImage.Path,
                        business_name = getResult.BusinessInfo.Name,
                        business_mobile = getResult.BusinessInfo.Mobile == null ? "" : getResult.BusinessInfo.Mobile,
                        order_discount_price = (getResult.OrignAmount - getResult.RealAmount) <= 0 ? "0.00" : (getResult.OrignAmount - getResult.RealAmount).ToString(),
                        minus_amount = getResult.MinusAmount.ToString(),
                        order_orign_price = getResult.OrignAmount.ToString(),
                        activity_type = getResult.ActivityType,
                        discount_remark = getResult.DiscountRemark,
                        pay_type = ((EnumHelp.PayType)payDetail.PayType).ToString(),
                        pay_status = ((EnumHelp.PayStatus)payDetail.PayStatus).ToString(),
                        remark_message = getResult.Remark,
                        seat_no = getResult.SeatNo,
                        longitude = getResult.BusinessInfo == null ? "0" : getResult.BusinessInfo.Longitude.ToString(),
                        latitude = getResult.BusinessInfo == null ? "0" : getResult.BusinessInfo.Latitude.ToString(),
                        address = getResult.BusinessInfo == null ? "" : getResult.BusinessInfo.Address,
                        is_time_over = is_time_over,
                        count = getResult.Count,
                        use_end_time = getResult.UseEndDate == null ? System.DateTime.Now.ToString("yyyy-MM-dd") : getResult.UseEndDate.ToString("yyyy-MM-dd"),
                        use_start_time = getResult.UseFromDate == null ? System.DateTime.Now.ToString("yyyy-MM-dd") : getResult.UseFromDate.ToString("yyyy-MM-dd"),
                        night_count = night_count
                    };
                    switch (getResult.BusinessInfo.BusinessTypeId)
                    {
                        case (int)EnumHelp.BusinessTypeEnum.食:
                            orderDetail.dishes_list = (orderDetailList != null) ? OrderDetailToDishesDTO(orderDetailList) : null;
                            break;
                        case (int)EnumHelp.BusinessTypeEnum.乐:
                            orderDetail.product_list = (orderDetailList != null) ? OrderDetailToProductDTO(orderDetailList) : null;
                            break;
                        case (int)EnumHelp.BusinessTypeEnum.酒店:
                            orderDetail.roomdto = (orderDetailList != null) ? OrderDetailToRoomDTO(orderDetailList.FirstOrDefault()) : null;
                            break;
                        case (int)EnumHelp.BusinessTypeEnum.景点:
                            orderDetail.ticketdto = (orderDetailList != null) ? OrderDetailToTicketDTO(orderDetailList.FirstOrDefault(), getResult.OrderCustomerList) : null;
                            break;
                        default:
                            break;
                    }
                    result.data = orderDetail;
                    result.total_count = 1;
                    //RedisDb._redisHelper_11().StringSet(key, result.data, RedisConfig._defaultExpiry);
                }
                else
                {
                    result.total_count = 0;
                    result.message = "订单不存在";
                }
                //}

            }
            else
            {
                result.message = "参数有误";
                result.error_code = Result.ERROR;
            }
            return result;
        }



        /// <summary>
        /// 提交商家评价
        /// </summary>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel OrderComment(PostComment Comment)
        {
            var result = new ResponseModel();
            result.error_code = Result.SUCCESS;
            if (Comment != null)
            {
                if (Comment.business_id <= 0 || Comment.user_id <= 0)
                {
                    result.message = "参数无效";
                    result.error_code = Result.ERROR;
                    return result;
                }
                if (Comment.comment_id > 0)//编辑
                {
                    var comment = _businessCommentService.GetById(Comment.comment_id);
                    if (comment != null)
                    {
                        comment.Contents = Comment.content;
                        comment.LevelId = Comment.level_id;
                        comment.IsAnonymous = Comment.is_anonymous;
                        _businessCommentService.Update(comment);//更新对象
                        var commentImageList = _commentImageService.GetByBusinessCommentId(Comment.comment_id);
                        if (string.IsNullOrWhiteSpace(Comment.imageId))//没有提交评价图片
                        {
                            if (commentImageList != null && commentImageList.Count > 0)
                            {
                                foreach (var item in commentImageList)
                                {
                                    _commentImageService.Delete(item);
                                }
                            }
                        }
                        else//提交评价图片
                        {
                            string[] baseImageIds = Comment.imageId.Split(',');
                            foreach (var item in commentImageList)//删掉不提交
                            {
                                if (!baseImageIds.Contains(item.BaseImageId.ToString()))
                                {
                                    _commentImageService.Delete(item);
                                }
                            }
                            for (int i = 0; i < baseImageIds.Length; i++)
                            {
                                int id = Convert.ToInt32(baseImageIds[i]);
                                var commentImageobj = commentImageList.Where(c => c.BaseImageId == id).FirstOrDefault();
                                if (commentImageobj == null)
                                {
                                    _commentImageService.Insert(new CommentImage()
                                    {
                                        BaseImageId = id,
                                        BusinessCommentId = Comment.comment_id
                                    });
                                }
                            }
                        }
                    }
                    else
                    {
                        result.message = "编辑失败，该评论不存在";
                        result.error_code = Result.ERROR;
                        return result;
                    }

                }
                else if (!string.IsNullOrWhiteSpace(Comment.order_no))//新增
                {
                    //判断用户是否已经针对此订单进行评论过
                    if (_businessCommentService.IsExist(Comment.business_id, Comment.user_id, Comment.order_no))
                    {
                        result.message = "您已经针对此订单评论过了！";
                        result.error_code = Result.ERROR;
                        return result;
                    }

                    //获取订单id，插入评论表
                    Order orderModel = _OrderService.GetDetailByOrderNo(Comment.order_no);
                    if (orderModel == null)
                    {
                        result.message = "没有获取到对应的订单数据！";
                        result.error_code = Result.ERROR;
                        return result;
                    }

                    //获取订单详情关联菜品数据
                    List<OrderDetail> orderDetailList = _orderDetailService.GetListByOrderNo(Comment.order_no);
                    var orderResult = _OrderService.GetDetailByOrderNo(Comment.order_no);
                    //插入商家评论
                    var businessComment = _businessCommentService.Insert(new BusinessComment()
                    {
                        BusinessInfoId = Comment.business_id,
                        OrderId = orderModel.OrderId,
                        Contents = string.IsNullOrWhiteSpace(Comment.content) ? "" : Comment.content,
                        CreateTime = System.DateTime.Now,
                        UserId = Comment.user_id,
                        IsAnonymous = Comment.is_anonymous,
                        LevelId = Comment.level_id,
                        IsDelete = (int)IsDeleteEnum.有效,
                        Status = (int)EnabledEnum.有效,
                        RecommendDishes = OrderDetailsToString(orderDetailList, orderResult)
                    });
                    //插入评论图片
                    if (!string.IsNullOrWhiteSpace(Comment.imageId) && businessComment.BusinessCommentId > 0)
                    {
                        string[] imageId = Comment.imageId.Split(',');
                        for (int i = 0; i < imageId.Length; i++)
                        {
                            _commentImageService.Insert(new CommentImage()
                            {
                                BaseImageId = Convert.ToInt32(imageId[i]),
                                BusinessCommentId = Convert.ToInt32(businessComment.BusinessCommentId)
                            });
                        }
                    }
                }
                else
                {
                    result.error_code = Result.ERROR;
                    result.message = "参数无效";
                }
            }
            else
            {
                result.error_code = Result.ERROR;
                result.message = "参数无效";
            }
            return result;
        }

        /// <summary>
        /// 获取根据商家id用户id获取当前用户最新未付款订单编号
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="BusinessInfoId"></param>
        /// <returns></returns>
        [HttpGet]
        public ResponseModel<string> GetOrderNoByUserId(int UserId, int BusinessInfoId)
        {
            var result = new ResponseModel<string>();
            result.error_code = Result.SUCCESS;
            if (UserId <= 0)
            {
                throw new Exception("用户id不合法");
            }
            if (BusinessInfoId <= 0)
            {
                throw new Exception("商家id不合法");
            }
            var OrderList = _OrderService.GetOrderNoByUserIdAndBusinessId(UserId, BusinessInfoId);
            var order = OrderList.Where(c => c.OrderStatusId == (int)EnumHelp.OrderStatus.未付款).OrderByDescending(c => c.OrderTime).FirstOrDefault();
            if (order != null)
            {
                result.data = order.OrderNo;
            }
            else
            {
                result.data = "";
            }
            return result;
        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseModel<string> CommitOrder(PostOrder Order)
        {
            var result = new ResponseModel<string>();
            result.error_code = Result.SUCCESS;
            //1.生成订单信息
            if (Order != null)
            {
                //1.1用户和商家验证
                if (true)
                {//_userService.GetById(Order.user_id) != null && _businessInfoService.GetById(Order.business_id) != null
                    string orderNo = RandomHelper.GetOrderNumber();
                    DateTime orderTime = System.DateTime.Now;
                    var businessInfo = _businessInfoService.GetById(Order.business_id);
                    decimal totalOrignAmount = 0.0M;
                    decimal totalrealAmount = 0.0M;
                    List<int> orderDetailIdList = new List<int>();
                    int dishesCount = 0;
                    decimal minusAmount = 0.0M;//折扣金额
                    #region 点餐
                    if (Order.module == (int)BusinessTypeEnum.食)//商家存在且菜品不为空
                    {
                        if (businessInfo != null && Order.dishes_list != null)
                        {
                            foreach (var item in Order.dishes_list)
                            {
                                var dish = businessInfo.DishesList.Where(c => c.DishesId == item.dishes_id).FirstOrDefault();
                                if (dish != null)
                                {
                                    totalOrignAmount += (dish.OrignPrice * item.dishes_count);
                                    totalrealAmount += (dish.RealPrice * item.dishes_count);
                                    decimal spec_orign_price = 0;
                                    decimal spec_real_price = 0;
                                    var dishesSpecDetaillist = _dishesSpecDetailService.GetListByIds(item.dishes_spec_detail_ids);
                                    if (dishesSpecDetaillist != null)
                                    {
                                        foreach (var spec in dishesSpecDetaillist)
                                        {
                                            spec_orign_price += spec.OrignPrice;
                                            spec_real_price += spec.RealPrice;
                                        }
                                        //spec_orign_price = (dishesSpecDetaillist.Sum(c => c.OrignPrice) * item.dishes_count);
                                        //spec_real_price = (dishesSpecDetaillist.Sum(c => c.RealPrice) * item.dishes_count);
                                        totalOrignAmount += spec_orign_price * item.dishes_count;
                                        totalrealAmount += spec_real_price * item.dishes_count;
                                    }
                                    else
                                    {
                                        var specList = dish.DishesSpecList;
                                        if (specList != null)
                                        {
                                            foreach (var spec in specList)
                                            {
                                                if (spec.DishesSpecDetailList != null)
                                                {
                                                    var specDetail = spec.DishesSpecDetailList.OrderBy(c => c.DishesSpecDetailId).FirstOrDefault();
                                                    if (specDetail != null)
                                                    {
                                                        spec_orign_price = specDetail.OrignPrice;
                                                        spec_real_price = specDetail.RealPrice;
                                                        totalrealAmount += specDetail.RealPrice * item.dishes_count;
                                                        totalOrignAmount += specDetail.OrignPrice * item.dishes_count;
                                                    }
                                                }

                                            }
                                        }

                                    }

                                    //1.2 生成子订单信息
                                    //int i = 0;
                                    //foreach (var dishitem in Order.dishes_list)
                                    //{
                                    //decimal spec_orign_price = 0;
                                    //decimal spec_real_price = 0;
                                    //if (!string.IsNullOrWhiteSpace(item.dishes_spec_detail_ids))
                                    //{
                                    //    spec_orign_price = _dishesSpecDetailService.GetListByIds(item.dishes_spec_detail_ids).Sum(c => c.OrignPrice);
                                    //    spec_real_price = _dishesSpecDetailService.GetListByIds(item.dishes_spec_detail_ids).Sum(c => c.RealPrice);
                                    //}
                                    var orderDetail = _orderDetailService.Insert(new OrderDetail()
                                    {
                                        Count = item.dishes_count,
                                        DishesId = item.dishes_id,
                                        OrderNo = orderNo,
                                        OrderTime = orderTime,
                                        OrignAmount = (dish.OrignPrice + spec_orign_price) * item.dishes_count,
                                        RealAmount = (dish.RealPrice + spec_real_price) * item.dishes_count,
                                        DishesSpecDetailIds = item.dishes_spec_detail_ids,
                                        ProductId = 0,
                                        RoomId = 0,
                                        PhoneNo = "",
                                        CustomerName = "",
                                        OrderId = 0
                                    });


                                    if (orderDetail.OrderDetailId > 0)
                                    {
                                        orderDetailIdList.Add(orderDetail.OrderDetailId);
                                        dishesCount++;
                                    }
                                    //    i++;
                                    //}
                                }
                                else
                                {
                                    result.error_code = Result.ERROR;
                                    result.message = "参数有误，菜品不存在";
                                    result.data = "";
                                    return result;
                                }
                            }


                        }
                        else
                        {
                            result.error_code = Result.ERROR;
                            result.message = "参数有误，商家或菜品不存在";
                            result.data = "";
                            return result;
                        }
                    }
                    #endregion
                    #region 娱乐
                    else if (Order.module == (int)BusinessTypeEnum.乐)
                    {
                        if (businessInfo != null && Order.product_list != null)//商家存在且菜品不为空
                        {
                            foreach (var item in Order.product_list)
                            {
                                var product = businessInfo.ProductList.Where(c => c.ProductId == item.product_id).FirstOrDefault();
                                if (product != null)
                                {
                                    totalOrignAmount += (product.OrignPrice * item.product_count);
                                    totalrealAmount += (product.RealPrice * item.product_count);
                                    _orderDetailService.Insert(new OrderDetail()
                                    {
                                        Count = item.product_count,
                                        ProductId = item.product_id,
                                        OrderNo = orderNo,
                                        OrderTime = orderTime,
                                        OrignAmount = item.product_orign_price * item.product_count,
                                        RealAmount = item.product_real_price * item.product_count,
                                        DishesSpecDetailIds = "",
                                        DishesId = 0,
                                        CustomerName = "",
                                        PhoneNo = "",
                                        RoomId = 0,
                                        OrderId = 0
                                    });
                                }
                            }
                        }
                        else
                        {
                            result.error_code = Result.ERROR;
                            result.message = "参数有误，产品不存在";
                            result.data = "";
                            return result;
                        }

                    }
                    #endregion
                    #region 酒店
                    else if (Order.module == (int)BusinessTypeEnum.酒店)
                    {
                        if (businessInfo != null && Order.room_info != null && Order.use_from_date != null && Order.use_end_date != null)
                        {
                            var room = _RoomService.GetById(Order.room_info.room_id);
                            if (!CheckInputHelper.RegexPhone(Order.room_info.phone_no) || room == null)
                            {
                                result.error_code = Result.ERROR;
                                result.message = "手机号码有误或房间Id不存在";
                                result.data = "";
                                return result;
                            }
                            if (string.IsNullOrWhiteSpace(Order.room_info.customer_name) || string.IsNullOrWhiteSpace(Order.room_info.phone_no) || Order.room_info.count <= 0)
                            {
                                result.error_code = Result.ERROR;
                                result.message = "参数有误";
                                result.data = "";
                                return result;
                            }

                            totalOrignAmount += (room.OrignPrice * Order.room_info.count);
                            totalrealAmount += (room.RealPrice * Order.room_info.count);
                            _orderDetailService.Insert(new OrderDetail()
                            {
                                RoomId = Order.room_info.room_id,
                                Count = Order.room_info.count,
                                ProductId = 0,
                                OrderNo = orderNo,
                                OrderTime = orderTime,
                                OrignAmount = room.OrignPrice * Order.room_info.count,
                                RealAmount = room.RealPrice * Order.room_info.count,
                                DishesSpecDetailIds = "",
                                DishesId = 0,
                                PhoneNo = Order.room_info.phone_no,
                                CustomerName = Order.room_info.customer_name,
                                TicketId = 0,
                                OrderId = 0
                            });
                        }
                    }
                    #endregion
                    #region 景点
                    else if (Order.module == (int)BusinessTypeEnum.景点)
                    {
                        if (businessInfo != null && Order.ticket_info != null && Order.count > 0 && Order.use_from_date != null)//商家存在且门票信息不为空  && Order.use_end_date != null
                        {
                            var ticket = Order.ticket_info;
                            if (ticket.ticket_id <= 0)
                            {
                                result.error_code = Result.ERROR;
                                result.message = "参数有误，门票id不合法";
                                result.data = "";
                                return result;
                            }
                            if (ticket.update_room == 1)
                            {
                                if (ticket.room_id <= 0 || ticket.hotel_id <= 0)
                                {
                                    result.error_code = Result.ERROR;
                                    result.message = "参数有误，更新房间参数有误";
                                    result.data = "";
                                    return result;
                                }
                                else
                                {
                                    var updRoom = _RoomService.GetById(ticket.room_id);
                                    if (updRoom != null)
                                    {
                                        if (updRoom.Remain <= 0)
                                        {
                                            result.error_code = Result.ERROR;
                                            result.message = "参数有误，更新酒店房间不足";
                                            result.data = "";
                                            return result;
                                        }
                                    }
                                    else
                                    {
                                        result.error_code = Result.ERROR;
                                        result.message = "参数有误，更新酒店房间不存在";
                                        result.data = "";
                                        return result;
                                    }
                                }
                            }
                            var ticketResult = _TicketService.GetById(ticket.ticket_id);
                            if (ticketResult.BindCard == 1 && string.IsNullOrWhiteSpace(ticket.customer_ids))
                            {
                                result.error_code = Result.ERROR;
                                result.message = "参数有误，绑定身份证信息不为空";
                                result.data = "";
                                return result;
                            }
                            var list = ticket.customer_ids.Split(',').ToArray();
                            int[] output = Array.ConvertAll<string, int>(list, delegate (string s) { return int.Parse(s); });
                            bool IsAllExist = _CustomerService.CustomerIdIsAllExist(output);
                            if (!IsAllExist)
                            {
                                result.error_code = Result.ERROR;
                                result.message = "参数有误，添加出行人参数有误";
                                result.data = "";
                                return result;
                            }
                            if (ticketResult.UseCount * Order.count < output.Length)
                            {
                                result.error_code = Result.ERROR;
                                result.message = "参数有误，添加出行人超过可使用人数";
                                result.data = "";
                                return result;
                            }
                            if (ticketResult == null)
                            {
                                result.error_code = Result.ERROR;
                                result.message = "参数有误，景点门票id不存在";
                                result.data = "";
                                return result;
                            }
                            else if (ticket.update_room == 1)//更换房间
                            {
                                var updateRoom = _RoomService.GetById(ticket.room_id);
                                if (updateRoom.BusinessInfoId == ticket.hotel_id)
                                {
                                    var room = (ticketResult.TicketRelateRoom != null && ticketResult.TicketRelateRoom[0] != null) ? ticketResult.TicketRelateRoom[0].Room : null;
                                    decimal minus_price = (room == null ? 0.00m : (room.RealPrice > updateRoom.RealPrice ? 0.00m : updateRoom.RealPrice - room.RealPrice)) * Order.count;
                                    totalOrignAmount += (ticketResult.OrignPrice * Order.count);
                                    totalOrignAmount += minus_price;
                                    totalrealAmount += (ticketResult.RealPrice * Order.count);
                                    totalrealAmount += minus_price;
                                    _orderDetailService.Insert(new OrderDetail()
                                    {
                                        Count = Order.count,
                                        ProductId = 0,
                                        OrderNo = orderNo,
                                        OrderTime = orderTime,
                                        OrignAmount = ticketResult.OrignPrice * Order.count,
                                        RealAmount = ticketResult.RealPrice * Order.count,
                                        DishesSpecDetailIds = "",
                                        DishesId = 0,
                                        CustomerName = "",
                                        PhoneNo = "",
                                        RoomId = ticket.room_id,
                                        TicketId = Order.ticket_info.ticket_id,
                                        OrderId = 0
                                    });
                                }
                                else
                                {
                                    result.error_code = Result.ERROR;
                                    result.message = "参数有误，房间id或酒店id有误";
                                    result.data = "";
                                    return result;
                                }
                            }
                            else
                            {
                                totalOrignAmount += (ticketResult.OrignPrice * Order.count);
                                totalrealAmount += (ticketResult.RealPrice * Order.count);
                                _orderDetailService.Insert(new OrderDetail()
                                {
                                    Count = Order.count,
                                    ProductId = 0,
                                    OrderNo = orderNo,
                                    OrderTime = orderTime,
                                    OrignAmount = ticketResult.OrignPrice * Order.count,
                                    RealAmount = ticketResult.RealPrice * Order.count,
                                    DishesSpecDetailIds = "",
                                    DishesId = 0,
                                    CustomerName = "",
                                    PhoneNo = "",
                                    RoomId = 0,
                                    TicketId = Order.ticket_info.ticket_id
                                });
                            }
                        }
                        else
                        {
                            result.error_code = Result.ERROR;
                            result.message = "参数有误，门票信息或使用时间不为空";
                            result.data = "";
                            return result;
                        }
                    }
                    #endregion
                    else
                    {
                        result.error_code = Result.ERROR;
                        result.message = "参数有误，模块id不存在";
                        result.data = "";
                        return result;
                    }

                    if (Order.discount_type == (int)DiscountType.折扣)
                    {
                        minusAmount = totalrealAmount - (totalrealAmount * (businessInfo.ActivityDiscount != null ? businessInfo.ActivityDiscount.Discount : 1));
                        totalrealAmount *= businessInfo.ActivityDiscount != null ? businessInfo.ActivityDiscount.Discount : 1;
                    }
                    else if (Order.discount_type == (int)DiscountType.满减 && Order.discount_id > 0 && businessInfo.ActivityMinusList != null)
                    {
                        var minusobj = businessInfo.ActivityMinusList.Where(c => c.ActivityMinusId == Order.discount_id).FirstOrDefault();
                        var bestPolution = businessInfo.ActivityMinusList.OrderByDescending(c => c.AchiveAmount).Where(c => c.IsDelete == (int)IsDeleteEnum.有效 && c.Status == (int)EnabledEnum.有效 && c.AchiveAmount <= totalrealAmount).FirstOrDefault();
                        if (minusobj != null)
                        {
                            var amount = (int)totalrealAmount;
                            minusAmount = (amount / (int)minusobj.AchiveAmount) * (int)minusobj.MinusAmount;
                            totalrealAmount -= (amount / (int)minusobj.AchiveAmount) * (int)minusobj.MinusAmount;
                        }
                        else if (bestPolution != null)
                        {
                            minusAmount = ((int)totalrealAmount / (int)bestPolution.AchiveAmount) * (int)bestPolution.MinusAmount;
                            totalrealAmount -= (totalrealAmount / (int)bestPolution.AchiveAmount) * (int)bestPolution.MinusAmount;
                        }
                    }
                    else
                    {
                        decimal minus = 0.0M;
                        decimal discount = 0.0M;
                        decimal discountTotalMount = totalrealAmount * (businessInfo.ActivityDiscount != null ? businessInfo.ActivityDiscount.Discount : 1);
                        discount = totalrealAmount - discountTotalMount;
                        var bestPolution = businessInfo.ActivityMinusList.OrderByDescending(c => c.AchiveAmount).Where(c => c.IsDelete == (int)IsDeleteEnum.有效 && c.Status == (int)EnabledEnum.有效 && c.AchiveAmount <= totalrealAmount).FirstOrDefault();
                        decimal minusTotalMount = totalrealAmount;
                        if (bestPolution != null)
                        {
                            var amount = (int)totalrealAmount;
                            minus = (amount / (int)bestPolution.AchiveAmount) * (int)bestPolution.MinusAmount;
                            minusTotalMount = amount - (amount / (int)bestPolution.AchiveAmount) * (int)bestPolution.MinusAmount;
                        }
                        totalrealAmount = discountTotalMount > minusTotalMount ? minusTotalMount : discountTotalMount;
                        minusAmount = discountTotalMount > minusTotalMount ? minus : discount;
                    }

                    //1.3 生成订单信息
                    var orderResult = _OrderService.Insert(new Order()
                    {
                        OrderNo = orderNo,
                        BusinessInfoId = Order.business_id,
                        ActivityType = Order.discount_type,
                        DiscountRemark = Order.discount_remark,
                        OrderStatusId = (int)EnumHelp.OrderStatus.未付款,
                        OrderTime = orderTime,
                        EditTime = orderTime,
                        OrignAmount = Math.Round(totalOrignAmount, 2, MidpointRounding.AwayFromZero),//Order.order_orign_price,
                        RealAmount = Math.Round(totalrealAmount, 2, MidpointRounding.AwayFromZero),//Order.order_real_price,
                        MinusAmount = Math.Round(minusAmount, 2, MidpointRounding.AwayFromZero),
                        Remark = Order.user_remark,
                        UserId = Order.user_id,
                        PayTime = orderTime,//默认值
                        SeatNo = Order.seat_no,
                        UseFromDate = Order.use_from_date == null ? System.DateTime.Now : Order.use_from_date,
                        UseEndDate = Order.use_end_date == null ? System.DateTime.Now : Order.use_end_date,
                        Count = Order.count <= 0 ? 1 : Order.count
                    });
                    var payDetailResult = _payDetailService.Insert(new PayDetail()
                    {
                        OrderNo = orderNo,
                        Amount = Math.Round(totalrealAmount, 2, MidpointRounding.AwayFromZero),//Order.order_real_price,
                        OrderTime = orderTime,
                        PaySerialNo = "",
                        PayStatus = (int)EnumHelp.PayStatus.未支付,
                        PayTime = orderTime,
                        PayType = Order.pay_type,
                        Remark = Order.user_remark,
                        UserId = Order.user_id
                    });
                    //订单表/子订单表/支付表插入数据完成
                    if (orderResult.OrderId > 0 && payDetailResult.PayDetailId > 0)
                    {
                        //更新子订单表主键Id
                        int updCount = _OrderService.UpdateOrderDetailByOrdereNo(orderResult.OrderId, orderNo);
                        if (Order.module == (int)BusinessTypeEnum.景点)
                        {
                            var ticket = Order.ticket_info;
                            var ticketResult = _TicketService.GetById(ticket.ticket_id);
                            if (ticketResult.BindCard == 1 && string.IsNullOrWhiteSpace(ticket.customer_ids))
                            {
                                result.error_code = Result.ERROR;
                                result.message = "参数有误，绑定身份证信息不为空";
                                result.data = "";
                                return result;
                            }
                            var list = ticket.customer_ids.Split(',').ToArray();
                            int[] output = Array.ConvertAll<string, int>(list, delegate (string s) { return int.Parse(s); });
                            for (int i = 0; i < output.Length; i++)
                            {
                                _OrderCustomerService.Insert(new OrderCustomer()
                                {
                                    CustomerId = output[i],
                                    OrderId = orderResult.OrderId
                                });
                            }
                        }

                        result.data = orderResult.OrderNo;

                        //添加订单状态记录
                        Task.Run(() =>
                        {
                            _OrderStatusLogService.Insert(new OrderStatusLog
                            {
                                OrderId = orderResult.OrderId,
                                Status = (int)EnumHelp.OrderStatus.未付款,
                                StatusName = EnumHelp.OrderStatus.未付款.ToString(),
                                CreateTime = DateTime.Now
                            });
                        });


                        //极光推送商家客户端
                        //Task.Run(() =>
                        //{
                        //    var _request = new SnsRequest();
                        //    _request.UserId = Order.user_id;
                        //    _request.PushMsg = "您有新的订单啦！";
                        //    _request.PushUsers = Order.business_id.ToString();
                        //    var dic = new Dictionary<string, string>();
                        //    dic.Add("orderNo", orderNo);
                        //    dic.Add("dateTime", orderTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                        //    dic.Add("addDetail", ((int)EnumHelp.IsAdd.否).ToString());
                        //    _request.PushExtras = dic;
                        //    var _response = _JpushLogService.JpushSendToTag(_request);
                        //});
                    }
                }
                else
                {
                    result.error_code = Result.ERROR;
                    result.message = "参数有误";
                    result.data = "";
                    return result;
                }
            }
            else
            {
                result.error_code = Result.ERROR;
                result.message = "参数有误";
                result.data = "";
                return result;
            }
            return result;
        }

        [HttpPost]
        public ResponseModel<string> AddDishesCommitOrder(PostOrder Order)
        {
            var result = new ResponseModel<string>();
            result.error_code = Result.SUCCESS;
            DateTime edittime = System.DateTime.Now;

            //1.生成订单信息
            if (Order != null)
            {
                if (Order.module != (int)EnumHelp.BusinessTypeEnum.食)
                {
                    throw new Exception("模块Id有误");
                }
                string orderNo = RandomHelper.GetOrderNumber();
                result.data = orderNo;
                var order = _OrderService.GetDetailByOrderNo(Order.order_no);
                var payDetail = _payDetailService.GetDetailByOrderNo(Order.order_no);
                var businessInfo = _businessInfoService.GetById(Order.business_id);
                decimal totalOrignAmount = 0.0M;
                decimal totalrealAmount = 0.0M;
                List<int> orderDetailIdList = new List<int>();
                int dishesCount = 0;
                decimal minusAmount = 0;//折扣金额
                if (order == null || payDetail == null)
                {
                    throw new Exception("订单不存在");
                }
                if (order.OrderStatusId == (int)EnumHelp.OrderStatus.未付款 && payDetail.PayStatus == (int)EnumHelp.PayStatus.未支付)//未付款 加餐
                {
                    if (businessInfo != null && Order.dishes_list != null)
                    {
                        order.EditTime = edittime;
                        foreach (var item in Order.dishes_list)
                        {
                            var dish = businessInfo.DishesList.Where(c => c.DishesId == item.dishes_id).FirstOrDefault();
                            if (dish != null)
                            {

                                //order.OrignAmount += (dish.OrignPrice * item.dishes_count);
                                totalOrignAmount += (dish.OrignPrice * item.dishes_count);
                                //order.RealAmount += (dish.RealPrice * item.dishes_count);
                                totalrealAmount += (dish.RealPrice * item.dishes_count);
                                decimal spec_orign_price = 0.0M;
                                decimal spec_real_price = 0.0M;
                                var dishesSpecDetaillist = _dishesSpecDetailService.GetListByIds(item.dishes_spec_detail_ids);
                                if (dishesSpecDetaillist != null)
                                {
                                    foreach (var spec in dishesSpecDetaillist)
                                    {
                                        spec_orign_price += spec.OrignPrice;
                                        spec_real_price += spec.RealPrice;

                                    }
                                    totalOrignAmount += spec_orign_price * item.dishes_count;
                                    totalrealAmount += spec_real_price * item.dishes_count;
                                    //spec_orign_price = (dishesSpecDetaillist.Sum(c => c.OrignPrice) * item.dishes_count);
                                    //spec_real_price = (dishesSpecDetaillist.Sum(c => c.RealPrice) * item.dishes_count);
                                    //order.OrignAmount += spec_orign_price;

                                    //order.RealAmount += spec_real_price;

                                }
                                else
                                {
                                    var specList = dish.DishesSpecList;
                                    if (specList != null)
                                    {
                                        foreach (var spec in specList)
                                        {
                                            if (spec.DishesSpecDetailList != null)
                                            {
                                                var specDetail = spec.DishesSpecDetailList.OrderBy(c => c.DishesSpecDetailId).FirstOrDefault();
                                                if (specDetail != null)
                                                {
                                                    spec_orign_price = specDetail.OrignPrice;
                                                    spec_real_price = specDetail.RealPrice;
                                                    totalrealAmount += specDetail.RealPrice * item.dishes_count;
                                                    totalOrignAmount += specDetail.OrignPrice * item.dishes_count;
                                                }
                                            }

                                        }
                                    }

                                }
                                var orderDetail = _orderDetailService.Insert(new OrderDetail()
                                {
                                    Count = item.dishes_count,
                                    DishesId = item.dishes_id,
                                    OrderNo = orderNo,
                                    OrderTime = edittime,
                                    OrignAmount = (dish.OrignPrice + spec_orign_price) * item.dishes_count,
                                    RealAmount = (dish.RealPrice + spec_real_price) * item.dishes_count,
                                    DishesSpecDetailIds = item.dishes_spec_detail_ids,
                                    ProductId = 0,
                                    RoomId = 0,
                                    PhoneNo = "",
                                    CustomerName = "",
                                    OrderId = order.OrderId
                                });
                                if (orderDetail.OrderDetailId > 0)
                                {
                                    orderDetailIdList.Add(orderDetail.OrderDetailId);
                                    dishesCount++;
                                }
                            }
                            else
                            {
                                foreach (var orderDetailId in orderDetailIdList)
                                {
                                    var orderDetailObj = _orderDetailService.GetById(orderDetailId);
                                    _orderDetailService.Delete(orderDetailObj);
                                }
                                throw new Exception("参数有误，菜品不存在");
                            }


                        }
                        if (dishesCount != Order.dishes_list.Count)
                        {
                            foreach (var item in orderDetailIdList)
                            {
                                var orderDetailObj = _orderDetailService.GetById(item);
                                _orderDetailService.Delete(orderDetailObj);
                            }
                            throw new Exception("加菜失败");

                        }
                        totalOrignAmount = 0.0M;
                        totalrealAmount = 0.0M;
                        foreach (var item in order.OrderDetailList)
                        {
                            var orderDetail=_orderDetailService.GetById(item.OrderDetailId);
                            orderDetail.OrderNo = orderNo;
                            _orderDetailService.Update(orderDetail);
                            totalOrignAmount += (item.Dishes.OrignPrice * item.Count);
                            //order.RealAmount += (dish.RealPrice * item.dishes_count);
                            totalrealAmount += (item.Dishes.RealPrice * item.Count);
                            decimal spec_orign_price = 0;
                            decimal spec_real_price = 0;
                            var dishesSpecDetaillist = _dishesSpecDetailService.GetListByIds(item.DishesSpecDetailIds);
                            if (dishesSpecDetaillist != null)
                            {
                                foreach (var spec in dishesSpecDetaillist)
                                {
                                    spec_orign_price += spec.OrignPrice;
                                    spec_real_price += spec.RealPrice;
                                }
                                totalOrignAmount += spec_orign_price * item.Count;
                                totalrealAmount += spec_real_price * item.Count;
                            }
                            else
                            {
                                var specList = item.Dishes.DishesSpecList;
                                if (specList != null)
                                {
                                    foreach (var spec in specList)
                                    {
                                        if (spec.DishesSpecDetailList != null)
                                        {
                                            var specDetail = spec.DishesSpecDetailList.OrderBy(c => c.DishesSpecDetailId).FirstOrDefault();
                                            if (specDetail != null)
                                            {
                                                totalrealAmount += specDetail.RealPrice * item.Count;
                                                totalOrignAmount += specDetail.OrignPrice * item.Count;
                                            }
                                        }

                                    }
                                }

                            }
                        }

                    }
                    else
                    {
                        throw new Exception("参数有误，商家或菜品不存在");
                    }

                    if (Order.discount_type == (int)DiscountType.折扣)
                    {
                        minusAmount = totalrealAmount - (totalrealAmount * (businessInfo.ActivityDiscount != null ? businessInfo.ActivityDiscount.Discount : 1));
                        totalrealAmount *= businessInfo.ActivityDiscount != null ? businessInfo.ActivityDiscount.Discount : 1;
                    }
                    else if (Order.discount_type == (int)DiscountType.满减 && Order.discount_id > 0 && businessInfo.ActivityMinusList != null)
                    {
                        var minusobj = businessInfo.ActivityMinusList.Where(c => c.ActivityMinusId == Order.discount_id).FirstOrDefault();
                        var bestPolution = businessInfo.ActivityMinusList.OrderByDescending(c => c.AchiveAmount).Where(c => c.IsDelete == (int)IsDeleteEnum.有效 && c.Status == (int)EnabledEnum.有效 && c.AchiveAmount <= totalrealAmount).FirstOrDefault();
                        if (minusobj != null)
                        {
                            var amount = (int)totalrealAmount;
                            minusAmount = (amount / (int)minusobj.AchiveAmount) * (int)minusobj.MinusAmount;
                            totalrealAmount -= (amount / (int)minusobj.AchiveAmount) * (int)minusobj.MinusAmount;
                        }
                        else if (bestPolution != null)
                        {
                            minusAmount = ((int)totalrealAmount / (int)bestPolution.AchiveAmount) * (int)bestPolution.MinusAmount;
                            totalrealAmount -= ((int)totalrealAmount / (int)bestPolution.AchiveAmount) * (int)bestPolution.MinusAmount;
                        }
                    }
                    else
                    {
                        decimal minus = 0.0M;
                        decimal discount = 0.0M;
                        decimal discountTotalMount = totalrealAmount * (businessInfo.ActivityDiscount != null ? businessInfo.ActivityDiscount.Discount : 1);
                        discount = totalrealAmount - discountTotalMount;
                        var bestPolution = businessInfo.ActivityMinusList.OrderByDescending(c => c.AchiveAmount).Where(c => c.IsDelete == (int)IsDeleteEnum.有效 && c.Status == (int)EnabledEnum.有效 && c.AchiveAmount <= totalrealAmount).FirstOrDefault();
                        decimal minusTotalMount = totalrealAmount;
                        if (bestPolution != null)
                        {
                            var amount = (int)totalrealAmount;
                            minus = (amount / (int)bestPolution.AchiveAmount) * (int)bestPolution.MinusAmount;
                            minusTotalMount = amount - (amount / (int)bestPolution.AchiveAmount) * (int)bestPolution.MinusAmount;
                        }
                        totalrealAmount = discountTotalMount > minusTotalMount ? minusTotalMount : discountTotalMount;
                        minusAmount = discountTotalMount > minusTotalMount ? minus : discount;
                    }
                    order.RealAmount = Math.Round(totalrealAmount, 2, MidpointRounding.AwayFromZero);
                    order.OrignAmount = Math.Round(totalOrignAmount, 2, MidpointRounding.AwayFromZero);
                    order.ActivityType = Order.discount_type;
                    order.MinusAmount = Math.Round(minusAmount, 2, MidpointRounding.AwayFromZero);
                    order.DiscountRemark = Order.discount_remark;
                    order.Remark = Order.user_remark;
                    order.OrderNo = orderNo;
                    _OrderService.Update(order);
                    payDetail.OrderTime = edittime;
                    payDetail.OrderNo = orderNo;
                    payDetail.Amount = Math.Round(totalrealAmount, 2, MidpointRounding.AwayFromZero);
                    _payDetailService.Update(payDetail);

                }
                else
                {
                    throw new Exception("订单不存在");
                }
            }
            else
            {
                throw new Exception("订单不存在");
            }

            //极光推送商家客户端
            //Task.Run(() =>
            //{
            //    var _request = new SnsRequest();
            //    _request.UserId = Order.user_id;
            //    _request.PushMsg = "您有新的订单啦！";
            //    _request.PushUsers = Order.business_id.ToString();
            //    var dic = new Dictionary<string, string>();
            //    dic.Add("orderNo", Order.order_no);
            //    dic.Add("dateTime", edittime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            //    dic.Add("addDetail",((int)EnumHelp.IsAdd.是).ToString());
            //    _request.PushExtras = dic;
            //    var _response = _JpushLogService.JpushSendToTag(_request);
            //});

            return result;
        }

        public decimal GetDecimal()
        {
            Math.Round(45.367, 2, MidpointRounding.AwayFromZero);
            var d = Math.Round(32.9984, 2, MidpointRounding.AwayFromZero);
            return (decimal)d;

        }

        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="Order"></param>
        /// <returns></returns>
        public async Task<ResponseModel<object>> QuicklysOrderPay(PostPay pay)
        {
            ResponseModel<object> result = new ResponseModel<object>();
            result.error_code = Result.SUCCESS;
            //1.参数验证
            if (!string.IsNullOrWhiteSpace(pay.OrderNo) && !string.IsNullOrWhiteSpace(pay.OpenId))
            {
                var order = _OrderService.GetDetailByOrderNo(pay.OrderNo);
                var paydetail = _payDetailService.GetDetailByOrderNo(pay.OrderNo);
                TimeSpan timeSpan = System.DateTime.Now - order.OrderTime;
                //15分钟有效时间
                if (order.BusinessInfo.BusinessTypeId != (int)EnumHelp.BusinessTypeEnum.食)
                {
                    if (timeSpan.TotalMinutes >= 15)
                    {
                        paydetail.PayStatus = (int)EnumHelp.PayStatus.支付超时;
                        _payDetailService.Update(paydetail);
                        result.error_code = Result.ERROR;
                        result.message = "支付超时";
                        return result;
                    }
                }
                if (order != null && paydetail != null && order.User.OpenId.Equals(pay.OpenId))
                {
                    PostOrder Order = new PostOrder();
                    Order.open_Id = pay.OpenId;
                    Order.spbill_create_ip = HttpContext.Current.Request.ServerVariables.Get("Remote_Addr").ToString();//((System.Web.HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
                    Order.order_real_price = order.RealAmount;
                    Order.order_no = order.OrderNo;
                    Order.dishes_list = null;
                    Order.pay_type = paydetail.PayType;
                    //2.根据支付类型（微信/支付宝）调用支付接口，生成prepay_id预付Id
                    switch (Order.pay_type)
                    {
                        case 1://微信
                            var res = await _wXPayService.WxDowloadOrder(Order);
                            WXPayDto model = new WXPayDto();
                            if (res != null)
                            {
                                TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
                                string GenerateTimeStamp = Convert.ToInt64(ts.TotalSeconds).ToString();
                                model.appId = res.appid;
                                model.nonce_str = res.nonce_str;
                                model.prepay_id = "prepay_id=" + res.prepay_id;
                                model.sign_type = "MD5";
                                model.timeStamp = GenerateTimeStamp;
                                model.paySign = _wXPayService.GetSign(res);
                                result.total_count = 1;
                            }
                            else
                            {
                                result.total_count = 0;
                                result.error_code = Result.ERROR;
                            }
                            result.data = model;
                            return result;
                        case 2://支付宝
                            var zres = _zFBPayService.ZPay(Order);
                            result.total_count = 1;
                            result.data = zres;
                            return result;
                        default:
                            break;
                    }
                }
                else
                {
                    result.error_code = Result.ERROR;
                    result.message = "参数有误";
                    result.total_count = 0;
                    return result;
                }
            }
            else
            {
                result.error_code = Result.ERROR;
                result.message = "参数无效";
                result.total_count = 0;
                return result;
            }
            return result;
        }

        public string GetOrderNo(string ss = "180514111554086249")
        {
            //未支付返回true
            if (!_OrderService.CheckOrder(ss))
            {
                return "已经支付了";
            }
            else
            {
                return "订单编号:" + ss + "未支付";
            }
        }

        #region 快捷支付(订单生成，支付)
        ///// <summary>
        ///// 付款
        ///// </summary>
        ///// <param name="Order"></param>
        ///// <returns></returns>
        //public async Task<IHttpActionResult> QuicklysOrderPay(PostOrder Order)
        //{
        //    //1.生成订单信息
        //    if (Order != null && Order.dishes_list != null)
        //    {
        //        //1.1用户和商家验证
        //        if (_userService.GetById(Order.user_id) != null && _businessInfoService.GetById(Order.business_id) != null)
        //        {
        //            string orderNo = RandomHelper.GetOrderNumber();
        //            DateTime orderTime = System.DateTime.Now;
        //            //1.2 生成子订单信息
        //            int i = 0;
        //            foreach (var item in Order.dishes_list)
        //            {
        //                _orderDetailService.Insert(new OrderDetail()
        //                {
        //                    Count = item.dishes_count,
        //                    DishesId = item.dishes_id,
        //                    OrderNo = orderNo,
        //                    OrderTime = orderTime,
        //                    OrignAmount = item.dishes_orign_price * item.dishes_count,
        //                    RealAmount = item.dishes_real_price * item.dishes_count
        //                });
        //                i++;
        //            }
        //            //1.3 生成订单信息
        //            var orderResult = _OrderService.Insert(new Order()
        //            {
        //                OrderNo = orderNo,
        //                BusinessInfoId = Order.business_id,
        //                ActivityType = Order.discount_type,
        //                DiscountRemark = Order.discount_remark,
        //                OrderStatusId = (int)EnumHelp.OrderStatus.未付款,
        //                OrderTime = orderTime,
        //                OrignAmount = Order.order_orign_price,
        //                RealAmount = Order.order_real_price,
        //                Remark = Order.user_remark,
        //                UserId = Order.user_id,
        //                PayTime = orderTime//默认值
        //            });
        //            var payDetailResult = _payDetailService.Insert(new PayDetail()
        //            {
        //                OrderNo = orderNo,
        //                Amount = Order.order_real_price,
        //                OrderTime = orderTime,
        //                PaySerialNo = "",
        //                PayStatus = (int)EnumHelp.PayStatus.未支付,
        //                PayTime = orderTime,
        //                PayType = Order.pay_type,
        //                Remark = Order.user_remark,
        //                UserId = Order.user_id
        //            });
        //            //订单表/子订单表/支付表插入数据完成
        //            if (i == Order.dishes_list.Count && orderResult.OrderId > 0 && payDetailResult.PayDetailId > 0)
        //            {
        //                //2.根据支付类型（微信/支付宝）调用支付接口，生成prepay_id预付Id
        //                switch (Order.pay_type)
        //                {
        //                    case 1://微信
        //                        var res = await _wXPayService.WxDowloadOrder(Order);
        //                        return Json(new { error_code = Result.SUCCESS, message = "success", data = res });
        //                    case 2://支付宝
        //                        var zres = _zFBPayService.ZPay(Order);
        //                        return Json(new { error_code = Result.SUCCESS, message = "success", data = zres });
        //                    default:
        //                        break;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            return Json(new { error_code = Result.ERROR, message = "支付失败", data = "" });
        //        }
        //    }
        //    else
        //    {
        //        return Json(new { error_code = Result.SUCCESS, message = "支付失败", data = "" });
        //    }
        //    return Json(new { error_code = Result.SUCCESS, message = "支付失败", data = "" });
        //} 
        #endregion

        /// <summary>
        /// 微信回调方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> WxCallback()
        {
            //验证签名，验证key
            //r reqResult = new WxPayTradeApi().DecryptTradeResult();
            //判断支付是否成功
            try
            {
                var s = System.Web.HttpContext.Current.Request.InputStream;
                byte[] b = new byte[s.Length];
                s.Read(b, 0, (int)s.Length);
                string postStr = Encoding.UTF8.GetString(b);
                //获取到支付回调参数，并且验证签名
                var reqResult = new WxPayTradeApi().DecryptTradeResult(postStr);
                //验签成功
                var data = RedisDb._redisHelper_12().StringSet<WxPayOrderTradeResp>("reqResult", reqResult);
                if (reqResult.Ret == 200 && reqResult.result_code == "SUCCESS")
                {
                    //查询订单是否支付完成
                    if (!_OrderService.CheckOrder(reqResult.out_trade_no))
                    {
                        throw new Exception("支付失败;订单号：" + reqResult.out_trade_no + ";支付失败");
                    }
                    //再查询支付是否完成。
                    var orderResult = await new WxPayTradeApi().QueryOrderAsync(reqResult.transaction_id, reqResult.out_trade_no);

                    RedisDb._redisHelper_12().StringSet<WxPayOrderTradeResp>("orderResult", orderResult);
                    if (orderResult.trade_state != "SUCCESS")
                    {
                        throw new Exception("支付失败;订单号：" + reqResult.out_trade_no + ";支付失败");
                    }
                    else if (orderResult.trade_state == "SUCCESS")
                    {

                        //1.0 根据订单编号获取订单详细信息
                        DateTime dateTime = System.DateTime.Now;
                        var orderObj = _OrderService.GetDetailByOrderNo(reqResult.out_trade_no);
                        var payDetail = _payDetailService.GetDetailByOrderNo(reqResult.out_trade_no);
                        orderObj.OrderStatusId = (int)EnumHelp.OrderStatus.已付款;
                        orderObj.PayTime = dateTime;
                        payDetail.PayStatus = (int)EnumHelp.PayStatus.已支付;
                        payDetail.PayTime = dateTime;
                        _OrderService.Update(orderObj);
                        _payDetailService.Update(payDetail);

                        //添加订单状态记录
                        Task.Run(() =>
                        {
                            _OrderStatusLogService.Insert(new OrderStatusLog
                            {
                                CreateTime = DateTime.Now,
                                OrderId = orderObj.OrderId,
                                Status = (int)EnumHelp.OrderStatus.已付款,
                                StatusName = "支付方式:" + "微信支付" + "--订单号:" + orderObj.OrderNo + "--回调:成功",
                            });
                        });


                        //2.0 根据订单相关信息获取短信模板所需参数
                        //_smsService.SmsOrderNoToBusiness(orderListDto.
                        //3.0 调用发送短信方法，发送短信给商家

                        //发送通知给管理员
                        var adminlist = ConfigurationManager.AppSettings["AdminList"];
                        if (!string.IsNullOrWhiteSpace(adminlist))
                        {
                            foreach (var item in adminlist.Split(','))
                            {
                                Dictionary<string, string> dic = new Dictionary<string, string>();
                                dic.Add("code", reqResult.out_trade_no);
                                SendSMSCommon.SendSMSsingle(SmsTemplate.SmsOrderNoToBusiness, item, dic, SmsSignNameTemplate.YssgDefaultSignName);
                            }
                        }

                        ////极光推送商家客户端
                        //Task.Run(() =>
                        //{
                        //    var _request = new SnsRequest();
                        //    _request.UserId = orderObj.UserId;
                        //    _request.PushMsg = "您有新的订单啦！";
                        //    _request.PushUsers = orderObj.BusinessInfoId.ToString();
                        //    var dic = new Dictionary<string, string>();
                        //    dic.Add("orderNo", orderObj.OrderNo);
                        //    dic.Add("dateTime", orderObj.OrderTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        //    dic.Add("addDetail",((int)EnumHelp.IsAdd.否).ToString());
                        //    _request.PushExtras = dic;
                        //    var _response = _JpushLogService.JpushSendToTag(_request);
                        //});

                    }
                    var reqResult2 = new WxPayTradeApi().GetTradeSendXml(reqResult);

                    //支付回调
                    //_OrderService.PaymentCallback(reqResult.out_trade_no, reqResult.openid, reqResult.transaction_id, (int)EnumHelp.PayType.微信);
                    return reqResult2;
                }
            }
            catch (Exception ex)
            {
                string str = "调用WxCallBACK出错" + ex.Message + "-----行号：" + ex.StackTrace;
                System.IO.File.WriteAllText(@"C:\Release_OderSystem\Api\txt\1.txt", str);
            }
            return "";

        }


        /// <summary>
        /// 支付宝支付回调
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ZCallBack(ZPayCallBackResp resp)
        {
            return Json(new { error_code = Result.SUCCESS, message = "回调结束", data = _zFBPayService.ZCallBack(resp, _zFBPayService.GetRequestPost()) });
        }

        /// <summary>
        /// 将子订单菜品名拼接成字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public string OrderDetailsToString(List<OrderDetail> list, Order order)
        {

            StringBuilder sb = new StringBuilder(200);
            if (list != null && list.Count > 0 && order != null)
            {
                int i = 0;
                switch (order.BusinessInfo.BusinessTypeId)
                {
                    case (int)EnumHelp.BusinessTypeEnum.乐:
                        foreach (var item in list)
                        {
                            if (i == 0)
                            {
                                sb.Append(item.Product.Name);
                            }
                            else
                            {
                                sb.Append(",");
                                sb.Append(item.Product.Name);
                            }

                        }
                        break;
                    case (int)EnumHelp.BusinessTypeEnum.食:
                        foreach (var item in list)
                        {
                            if (i == 0)
                            {
                                sb.Append(item.Dishes.Name);
                            }
                            else
                            {
                                sb.Append(",");
                                sb.Append(item.Dishes.Name);
                            }

                        }
                        break;
                    default:
                        break;
                }
            }
            return sb.ToString();
        }

        public List<List<DishesDTO>> OrderDetailToDishesDTO(List<OrderDetail> list)
        {
            List<List<DishesDTO>> result = new List<List<DishesDTO>>();
            if (list != null)
            {
                var data = list.OrderBy(c => c.OrderTime).GroupBy(c => c.OrderTime).ToList();
                foreach (var items in data)
                {
                    List<DishesDTO> obj = new List<DishesDTO>();
                    foreach (var item in items)
                    {
                        var dishesSpecDTO = new List<DishesSpecDTO>();
                        //有规格
                        if (!string.IsNullOrWhiteSpace(item.DishesSpecDetailIds))
                        {
                            var dishesSpecDetailList = _dishesSpecDetailService.GetListByIds(item.DishesSpecDetailIds);
                            foreach (var dishesSpecDetail in dishesSpecDetailList)
                            {
                                dishesSpecDTO.Add(new DishesSpecDTO()
                                {
                                    spec_id = dishesSpecDetail.DishesSpecId,
                                    spec_name = dishesSpecDetail.DishesSpec == null ? "" : dishesSpecDetail.DishesSpec.Name,
                                    detail_list = DishesSpecDetailToDTO(dishesSpecDetail)
                                });
                            }

                        }
                        obj.Add(new DishesDTO()
                        {
                            dishes_id = item.DishesId,
                            dishes_img_id = item.Dishes == null ? "0" : item.Dishes.BaseImageId.ToString(),
                            dishes_name = item.Dishes == null ? "" : item.Dishes.Name,
                            dishes_orign_price = item.OrignAmount,
                            dishes_real_price = item.RealAmount,
                            month_sale_count = item.Dishes == null ? 0 : item.Dishes.SellCountPerMonth,
                            dishes_count = item.Count,
                            order_detail_id = item.OrderDetailId,
                            dishes_img_path = item.Dishes == null ? "" : (item.Dishes.BaseImage == null ? "" : item.Dishes.BaseImage.Source + item.Dishes.BaseImage.Path),
                            specs = dishesSpecDTO,//item.Dishes == null ? null : (item.Dishes.DishesSpecList == null ? null : SpecToDTO(item.Dishes.DishesSpecList)),
                            tags = null//item.Dishes == null ? null : (item.Dishes.DishesLableList == null ? null : DishesLableToList(item.Dishes.DishesLableList)),
                        });
                    }
                    result.Add(obj);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取景点订单票券详情
        /// </summary>
        /// <param name="orderDetail"></param>
        /// <param name="orderComusterList"></param>
        /// <returns></returns>
        private TicketDTO OrderDetailToTicketDTO(OrderDetail orderDetail, List<OrderCustomer> orderComusterList)
        {
            TicketDTO result = new TicketDTO();
            List<RelateRoomDTO> relateRoomList = new List<RelateRoomDTO>();
            List<RelateTicketDTO> relateTicketList = new List<RelateTicketDTO>();
            List<CustomerDTO> customer_list = new List<CustomerDTO>();
            if (orderDetail != null)
            {
                result.name = orderDetail.Ticket == null ? "" : orderDetail.Ticket.Name; //(orderDetail.Ticket.BusinessInfo == null ? "" : orderDetail.Ticket.BusinessInfo.Name);
                result.real_price = orderDetail.Ticket == null ? "" : orderDetail.Ticket.RealPrice.ToString();
                result.orign_price = orderDetail.Ticket == null ? "" : orderDetail.Ticket.OrignPrice.ToString();
                result.rules = orderDetail.Ticket == null ? "" : orderDetail.Ticket.Rules;
                result.special = orderDetail.Ticket == null ? "" : orderDetail.Ticket.Special;
            }
            if (orderDetail != null && orderDetail.Ticket != null)
            {
                if (orderDetail.Ticket.TicketRelateRoom != null && orderDetail.Ticket.TicketRelateRoom.Count > 0)//关联酒店房间
                {
                    var list = orderDetail.Ticket.TicketRelateRoom;
                    if (orderDetail.RoomId > 0)
                    {
                        if (orderDetail.Room != null && list[0] != null)
                        {
                            var relateRoom = list[0];
                            var updRoom = orderDetail.Room;
                            relateRoomList.Add(new RelateRoomDTO()
                            {
                                count = relateRoom.Count,
                                name = updRoom.Name
                            });
                        }

                    }
                    else
                    {
                        foreach (var item in list)
                        {
                            var room = item.Room;
                            if (room != null)
                            {
                                relateRoomList.Add(new RelateRoomDTO()
                                {
                                    name = room.Name,
                                    count = item.Count
                                });
                            }

                        }
                    }

                }
                if (orderDetail.Ticket.TicketRelateTicket != null && orderDetail.Ticket.TicketRelateTicket.Count > 0)//关联票券
                {
                    var list = orderDetail.Ticket.TicketRelateTicket;
                    foreach (var item in list)
                    {
                        var ticket = _TicketService.GetById(item.RelateTicketId);
                        if (ticket != null)
                        {
                            relateTicketList.Add(new RelateTicketDTO()
                            {
                                count = item.Count,
                                name = ticket.Name
                            });
                        }
                    }
                }
            }
            if (orderComusterList != null)
            {
                foreach (var item in orderComusterList)
                {
                    customer_list.Add(new CustomerDTO()
                    {
                        CardNo = item.Customer == null ? "" : item.Customer.CardNo,
                        CardType = item.Customer == null ? 0 : item.Customer.CardType,
                        CustomerId = item.Customer == null ? 0 : item.Customer.CustomerId,
                        Name = item.Customer == null ? "" : item.Customer.Name
                    });
                }
            }
            result.relate_room_list = relateRoomList;
            result.relate_ticket_list = relateTicketList;
            result.customer_list = customer_list;
            return result;
        }

        private RoomDTO OrderDetailToRoomDTO(OrderDetail orderDetail)
        {
            RoomDTO result = new RoomDTO();
            if (orderDetail != null)
            {

                result.phone_no = orderDetail.PhoneNo;
                result.customer_name = orderDetail.CustomerName;
                result.count = orderDetail.Count;
                result.name = orderDetail.Room == null ? "" : orderDetail.Room.Name;
                result.orignPrice = orderDetail.Room == null ? "0.00" : orderDetail.Room.OrignPrice.ToString();
                result.realPrice = orderDetail.Room == null ? "0.00" : orderDetail.Room.RealPrice.ToString();
                result.window = orderDetail.Room == null ? "" : orderDetail.Room.Window.ToString();
                result.airConditioner = orderDetail.Room == null ? "" : orderDetail.Room.AirConditioner.ToString();
                result.area = orderDetail.Room == null ? "" : orderDetail.Room.Area.ToString();
                result.bathroom = orderDetail.Room == null ? "" : orderDetail.Room.Bathroom.ToString();
                result.bed = orderDetail.Room == null ? "" : orderDetail.Room.Bed.ToString();
                result.bed_type = orderDetail.Room == null ? "" : orderDetail.Room.BedType.ToString();
                result.breakfast = orderDetail.Room == null ? "" : orderDetail.Room.Breakfast.ToString();
                result.floor = orderDetail.Room == null ? "" : orderDetail.Room.Floor.ToString();
                result.internet = orderDetail.Room == null ? "" : orderDetail.Room.Internet.ToString();
            }
            return result;
        }

        public List<ProductDTO> OrderDetailToProductDTO(List<OrderDetail> list)
        {
            List<ProductDTO> result = new List<ProductDTO>();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    //var dishesSpecDTO = new List<DishesSpecDTO>();
                    ////有规格
                    //if (!string.IsNullOrWhiteSpace(item.DishesSpecDetailIds))
                    //{
                    //    var dishesSpecDetailList = _dishesSpecDetailService.GetListByIds(item.DishesSpecDetailIds);
                    //    foreach (var dishesSpecDetail in dishesSpecDetailList)
                    //    {
                    //        dishesSpecDTO.Add(new DishesSpecDTO()
                    //        {
                    //            spec_id = dishesSpecDetail.DishesSpecId,
                    //            spec_name = dishesSpecDetail.DishesSpec == null ? "" : dishesSpecDetail.DishesSpec.Name,
                    //            detail_list = DishesSpecDetailToDTO(dishesSpecDetail)
                    //        });
                    //    }

                    //}
                    result.Add(new ProductDTO()
                    {
                        product_id = item.ProductId,
                        product_name = item.Product == null ? "" : item.Product.Name,
                        count = item.Count,
                        orign_price = item.OrignAmount.ToString(),
                        real_price = item.RealAmount.ToString(),
                        start_date = item.Product.StartDate.ToString(),
                        end_date = item.Product.EndDate.ToString()
                        //product_id = item.ProductId,
                        //product_count = item.Count,
                        //product_orign_price = item.OrignAmount,
                        //product_real_price = item.RealAmount

                        //dishes_id = item.DishesId,
                        //dishes_img_id = item.Dishes == null ? "0" : item.Dishes.BaseImageId.ToString(),
                        //dishes_name = item.Dishes == null ? "" : item.Dishes.Name,
                        //dishes_orign_price = item.OrignAmount,
                        //dishes_real_price = item.RealAmount,
                        //month_sale_count = item.Dishes == null ? 0 : item.Dishes.SellCountPerMonth,
                        //dishes_count = item.Count,
                        //order_detail_id = item.OrderDetailId,
                        //dishes_img_path = item.Dishes == null ? "" : (item.Dishes.BaseImage == null ? "" : item.Dishes.BaseImage.Source + item.Dishes.BaseImage.Path),
                        //specs = dishesSpecDTO,//item.Dishes == null ? null : (item.Dishes.DishesSpecList == null ? null : SpecToDTO(item.Dishes.DishesSpecList)),
                        //tags = null//item.Dishes == null ? null : (item.Dishes.DishesLableList == null ? null : DishesLableToList(item.Dishes.DishesLableList)),
                    });
                }
            }
            return result;
        }

        public List<DishesSpecDTO> SpecToDTO(List<DishesSpec> list)
        {
            var result = new List<DishesSpecDTO>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    result.Add(new DishesSpecDTO()
                    {
                        spec_id = item.DishesSpecId,
                        spec_name = item.Name,
                        detail_list = item.DishesSpecDetailList == null ? null : DishesSpecDetailListToDTO(item.DishesSpecDetailList)
                    });
                }
            }
            return result;
        }

        public List<DishesSpecDetailDTO> DishesSpecDetailListToDTO(List<DishesSpecDetail> list)
        {
            var result = new List<DishesSpecDetailDTO>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    result.Add(new DishesSpecDetailDTO()
                    {
                        descript = item.Descript,
                        dishes_orign_price = item.OrignPrice,
                        dishes_real_price = item.RealPrice,
                        specdetail_id = item.DishesSpecDetailId,
                        spec_id = item.DishesSpecId
                    });
                }
            }
            return result;
        }

        public List<DishesSpecDetailDTO> DishesSpecDetailToDTO(DishesSpecDetail dsd)
        {
            var result = new List<DishesSpecDetailDTO>();
            if (dsd != null)
            {
                result = new List<DishesSpecDetailDTO>();

                result.Add(new DishesSpecDetailDTO()
                {
                    descript = dsd.Descript,
                    dishes_orign_price = dsd.OrignPrice,
                    dishes_real_price = dsd.RealPrice,
                    specdetail_id = dsd.DishesSpecDetailId,
                    spec_id = dsd.DishesSpecId
                });
            }
            return result;
        }

        public List<string> DishesLableToList(List<DishesLable> list)
        {
            var result = new List<string>();
            if (list != null)
            {
                foreach (var item in list)
                {
                    result.Add(item.Name);
                }
            }
            return result;
        }

        #region 商家客户端

        /// <summary>
        /// 获取订单列表接口
        /// </summary>
        /// <param name="Start_Time"></param>
        /// <param name="End_Time"></param>
        /// <param name="Business_Id"></param>
        /// <param name="Page_Index"></param>
        /// <param name="Page_Size"></param>
        /// <returns></returns>
        [HttpGet]
        [ApiSecurityFilter]
        public ResponseModel<BCOrderListDTO> GetBusinessClientOrderList(DateTime Start_Time, DateTime End_Time, int Business_Id = 0, int Page_Index = 1, int Page_Size = 20)
        {
            var result = new ResponseModel<BCOrderListDTO>();
            var data = new BCOrderListDTO();
            result.error_code = Result.SUCCESS;
            result.message = "";
            if (Business_Id <= 0 || Start_Time == null || End_Time == null)
            {
                result.error_code = Result.ERROR;
                result.message = "参数有误";
                result.total_count = 0;
                result.data = data;
                return result;
            }
            int totalCount = 0;
            var getlist = _OrderService.GetBusinessClientOrderList(Business_Id, Start_Time, End_Time, out totalCount);
            var orderList = new List<BCOrderDTO>();
            if (getlist != null && getlist.Count > 0)
            {
                data.order_total_count = getlist.Count;
                data.total_amount = getlist.Sum(c => c.RealAmount).ToString();
                data.order_count = getlist.Count;
                var payOrderList = getlist.Where(c => c.OrderStatusId == (int)EnumHelp.OrderStatus.已付款).OrderByDescending(c => c.PayTime).ToList();
                var othersOrderList = getlist.Where(c => c.OrderStatusId != (int)EnumHelp.OrderStatus.已付款).OrderBy(c => c.PayTime).ToList();
                var resultList = payOrderList.Concat(othersOrderList).ToList();
                var pageResult = resultList.Take(Page_Size * Page_Index).Skip(Page_Size * (Page_Index - 1)).ToList();
                if (pageResult != null && pageResult.Count > 0)
                {
                    foreach (var item in pageResult)
                    {
                        var order = new BCOrderDTO()
                        {
                            order_no = item.OrderNo,
                            order_statu = item.OrderStatusId.ToString(),
                            order_time = item.PayTime.ToString("yyyy-MM-dd hh:mm:ss"),
                            real_amount = item.RealAmount.ToString(),
                            seat_no = item.SeatNo
                        };
                        var orderDetail = new List<BCOrderDetailsDTO>();
                        if (item.OrderDetailList != null)
                        {
                            foreach (var detail in item.OrderDetailList)
                            {
                                orderDetail.Add(new BCOrderDetailsDTO()
                                {
                                    count = detail.Count,
                                    dishes_name = detail.Dishes == null ? "" : detail.Dishes.Name,
                                    real_amount = detail.RealAmount.ToString()
                                });
                            }
                        }
                        order.order_details_list = orderDetail;
                        orderList.Add(order);
                    }
                    data.orderDto_list = orderList;
                }
                result.data = data;
                result.total_count = totalCount;
            }
            else
            {
                data.order_count = 0;
                data.total_amount = "0.00";
                data.order_total_count = 0;
                data.orderDto_list = orderList;
                result.data = data;
            }
            return result;
        }

        [HttpGet]
        [ApiSecurityFilter]
        /// <summary>
        /// 获取热销菜品接口
        /// </summary>
        /// <param name="Start_Time"></param>
        /// <param name="End_Time"></param>
        /// <param name="Business_Id"></param>
        /// <returns></returns>
        public ResponseModel<BCDishesDTO> GetTopDishes(DateTime Start_Time, DateTime End_Time, int Business_Id = 0)
        {
            var result = new ResponseModel<BCDishesDTO>();
            result.error_code = Result.SUCCESS;
            result.message = "";
            var data = new BCDishesDTO();
            if (Business_Id <= 0 || Start_Time == null || End_Time == null)
            {
                result.error_code = Result.ERROR;
                result.message = "参数有误";
                result.total_count = 0;
                result.data = data;
                return result;
            }
            int totalCount = 0;
            //2, 7, 8
            var getlist = _OrderService.GetBusinessClientOrderList(Business_Id, Start_Time, End_Time, out totalCount);
            var orderlist = getlist.Where(c => c.OrderStatusId == (int)EnumHelp.OrderStatus.已付款).ToList();
            var amountlist = getlist.Where(c => (new int[] { 2, 7 }).Contains(c.OrderStatusId)).ToList();
            data.order_count = orderlist == null ? 0 : orderlist.Count;
            data.total_amount = amountlist == null ? "0.00" : amountlist.Sum(c => c.OrignAmount).ToString();
            result.total_count = orderlist == null ? 0 : orderlist.Count;
            //获取子订单集合
            List<BCTopDishesDTO> topDishesList = new List<BCTopDishesDTO>();
            if (getlist != null)
            {
                var orderDetaillis = getlist.SelectMany(c => c.OrderDetailList).ToList();
                //List<OrderDetail> orderDetaillis = new List<OrderDetail>();
                //foreach (var item in getlist)
                //{
                //    orderDetaillis.AddRange(item.OrderDetailList);
                //}
                if (orderDetaillis != null && orderDetaillis.Count > 0)
                {
                    var groupDetailList = orderDetaillis.GroupBy(c => c.DishesId).ToList();
                    foreach (var items in groupDetailList)
                    {
                        var dto = new BCTopDishesDTO();
                        var dishes = items.FirstOrDefault();
                        dto.count = items.Sum(c => c.Count);
                        dto.name = dishes == null ? "" : (dishes.Dishes == null ? "" : dishes.Dishes.Name);
                        dto.path = dishes == null ? "" : (dishes.Dishes == null ? "" : dishes.Dishes.ImageUrl);
                        topDishesList.Add(dto);
                        topDishesList.OrderByDescending(c => c.count).Take(10).ToList();
                    }
                }
            }
            data.top_dishesDto_list = topDishesList;
            result.data = data;
            return result;
        }

        /// <summary>
        /// 确认订单
        /// </summary>
        /// <param name="bcconfirmOrderDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiSecurityFilter]
        public ResponseModel<string> ConfirmOrder(BCConfirmOrderDTO bcconfirmOrderDTO)
        {
            var result = new ResponseModel<string>();
            result.error_code = Result.SUCCESS;
            result.message = "";
            result.data = "";
            string msg = string.Empty;
            var updResult = _OrderService.ConfirmOrder(bcconfirmOrderDTO.Order_No, bcconfirmOrderDTO.Business_Id, out msg);
            if (updResult)
            {
                return result;
            }
            else
            {
                result.error_code = Result.ERROR;
                result.message = msg;
            }
            return result;
        }
        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        [HttpGet]
        [ApiSecurityFilter]
        public ResponseModel<OrderDetailDTO> GetOrderDetail(string Order_No, DateTime OrderTime, int IsAdd)
        {
            var result = new ResponseModel<OrderDetailDTO>();
            result.error_code = Result.SUCCESS;
            if (!string.IsNullOrWhiteSpace(Order_No))
            {
                var getResult = _OrderService.GetDetailByOrderNo(Order_No);
                var payDetail = _payDetailService.GetDetailByOrderNo(Order_No);
                //子订单列表
                var orderDetailList = getResult.OrderDetailList;//_orderDetailService.GetListByOrderNo(Order_No);
                if (getResult != null && payDetail != null && orderDetailList != null)
                {
                    string is_time_over = null;
                    if (getResult.OrderStatusId == (int)EnumHelp.OrderStatus.未付款)
                    {
                        is_time_over = "待支付";
                    }
                    else if (getResult.OrderStatusId == (int)EnumHelp.OrderStatus.已付款 || getResult.OrderStatusId == (int)EnumHelp.OrderStatus.已结算)
                    {
                        is_time_over = "已支付";
                    }
                    //TimeSpan timeSpan = System.DateTime.Now - getResult.OrderTime;
                    //15分钟有效时间
                    //if (timeSpan.TotalMinutes >= 15 && getResult.OrderStatusId == (int)EnumHelp.OrderStatus.未付款)
                    //{
                    //    is_time_over = "支付超时";
                    //}
                    //else if (getResult.OrderStatusId == (int)EnumHelp.OrderStatus.已付款)
                    //{
                    //    is_time_over = "已支付";
                    //}
                    //else
                    //{
                    //    is_time_over = "待支付";
                    //}
                    var orderDetail = new OrderDetailDTO()
                    {
                        business_id = getResult.BusinessInfoId,
                        order_no = getResult.OrderNo,
                        order_id = getResult.OrderId,
                        order_real_price = getResult.RealAmount.ToString(),
                        order_time = getResult.OrderTime.ToString(),
                        order_status = getResult.OrderStatus == null ? "" : getResult.OrderStatus.Name,
                        business_img_id = getResult.BusinessInfo.BaseImageId.ToString(),
                        business_img_path = getResult.BusinessInfo.BaseImage == null ? "" : getResult.BusinessInfo.BaseImage.Source + getResult.BusinessInfo.BaseImage.Path,
                        business_name = getResult.BusinessInfo.Name,
                        business_mobile = getResult.BusinessInfo.Mobile == null ? "" : getResult.BusinessInfo.Mobile,
                        order_discount_price = (getResult.OrignAmount - getResult.RealAmount).ToString(),
                        order_orign_price = getResult.OrignAmount.ToString(),
                        activity_type = getResult.ActivityType,
                        discount_remark = getResult.DiscountRemark,
                        pay_type = ((EnumHelp.PayType)payDetail.PayType).ToString(),
                        pay_status = ((EnumHelp.PayStatus)payDetail.PayStatus).ToString(),
                        remark_message = getResult.Remark,
                        seat_no = getResult.SeatNo,
                        longitude = getResult.BusinessInfo == null ? "0" : getResult.BusinessInfo.Longitude.ToString(),
                        latitude = getResult.BusinessInfo == null ? "0" : getResult.BusinessInfo.Latitude.ToString(),
                        address = getResult.BusinessInfo == null ? "" : getResult.BusinessInfo.Address,
                        is_time_over = is_time_over
                    };
                    switch (getResult.BusinessInfo.BusinessTypeId)
                    {
                        case (int)EnumHelp.BusinessTypeEnum.食:
                            if (IsAdd == (int)EnumHelp.IsAdd.是)
                            {
                                orderDetail.dishes_list = (orderDetailList != null) ? OrderDetailToDishesDTO(orderDetailList.Where(c => c.OrderTime == OrderTime).ToList()) : null;
                            }
                            else
                            {
                                orderDetail.dishes_list = (orderDetailList != null) ? OrderDetailToDishesDTO(orderDetailList) : null;
                            }
                            break;
                        case (int)EnumHelp.BusinessTypeEnum.乐:
                            orderDetail.product_list = (orderDetailList != null) ? OrderDetailToProductDTO(orderDetailList) : null;
                            break;
                        case (int)EnumHelp.BusinessTypeEnum.酒店:
                            orderDetail.roomdto = (orderDetailList != null) ? OrderDetailToRoomDTO(orderDetailList.FirstOrDefault()) : null;
                            break;
                        case (int)EnumHelp.BusinessTypeEnum.景点:
                            orderDetail.ticketdto = (orderDetailList != null) ? OrderDetailToTicketDTO(orderDetailList.FirstOrDefault(), getResult.OrderCustomerList) : null;
                            break;
                        default:
                            break;
                    }
                    result.data = orderDetail;
                    result.total_count = 1;
                }
                else
                {
                    result.total_count = 0;
                    result.message = "订单不存在";
                }
            }
            else
            {
                result.message = "参数有误";
                result.error_code = Result.ERROR;
            }
            return result;
        }
        #endregion
    }
}
