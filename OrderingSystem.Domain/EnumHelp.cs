using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Domain
{
    public class EnumHelp
    {

        /// <summary>
        /// 启用状态枚举
        /// </summary>
        public enum EnabledEnum
        {
            无效 = 0,
            有效 = 1,
        }

        public enum IsDeleteEnum
        {
            已删除 = 1,
            有效 = 0,
        }

        public enum BusinessImageTypeEnum
        {
            /// <summary>
            /// 顶部轮播图
            /// </summary>
            展示图 = 1,
            /// <summary>
            /// 可以旋转的全景图
            /// </summary>
            全景图 = 2,
            /// <summary>
            /// 商家店铺实景图片
            /// </summary>
            实景图 = 3
        }

        /// <summary>
        /// 是否新增菜品
        /// </summary>
        public enum IsAdd
        {
            是 = 1,
            否 = 0
        }
        public enum BusinessTypeEnum
        {
            食 = 1,
            衣 = 2,
            乐 = 3,
            酒店 = 4,
            景点 = 5,
            娱乐模块 = 6//包含 3 4 5
        }

        /// <summary>
        /// 用户角色别枚举
        /// </summary>
        public enum RoleTypeEnum
        {
            系统管理员 = 1000,
            商家 = 1001,
        }
        /// <summary>
        /// 短信发送模块
        /// </summary>
        public enum SmsLogModuleEnum
        {
            前端 = 1,
            后台 = 2,
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        public enum OrderStatus
        {
            未付款 = 1, 已付款 = 2, 已取消 = 3, 退款中 = 4, 已退款 = 5, 退款失败 = 6, 已使用 = 7, 已结算 = 8,
        }

        /// <summary>
        /// 支付状态
        /// </summary>
        public enum PayStatus
        {
            未支付 = 1, 已支付 = 2, 支付超时 = 3
        }

        public enum PayType
        {
            微信 = 1, 支付宝 = 2,商家付款=9
        }
        /// <summary>
        /// 是否匿名
        /// </summary>
        public enum IsAnonymous
        {
            未匿名 = 0, 匿名 = 1
        }

        /// <summary>
        /// 活动类型
        /// </summary>
        public enum DiscountType
        {
            满减 = 1, 折扣 = 2
        }

        /// <summary>
        /// 评论等级
        /// </summary>
        public enum Level
        {
            推荐 = 1,
            满意 = 2,
            一般 = 3,
            差 = 4
        }

        /// <summary>
        /// 枚举排序优先级
        /// </summary>
        public enum PriorityType
        {
            距离优先 = 0,
            好评优先 = 1,
            低价优先 = 2,
            高价优先 = 3
        }

        public enum IsUpdRoomEnum
        {
            是 = 1,
            否 = 0
        }

        public enum IdCardType
        {
            身份证 = 1,
            护照 = 2,
            学生证 = 3
        }

        public enum IsTop
        {
            是 = 1,
            否 = 0
        }

        public enum IsLike
        {
            点赞 = 1,
            未点赞 = 0
        }

        public static Dictionary<int, string> HotelPrice = new Dictionary<int, string>() {
            { 1,"0-100"},
            { 2,"100-300"},
            { 3,"300-500"},
            { 4,"500-100000"}
        };

        public static Dictionary<int, string> HotelGrade = new Dictionary<int, string>() {
            { 1,"0-3"},
            { 2,"3-4"},
            { 3,"4-5"},
            { 4,"5-6"}
        };

        public static int[] hotelCategoryId = { 1, 2, 3, 4, 5, 6 };

        /// <summary>
        /// 拼饭性别类型
        /// </summary>
        public enum RiceDateSexType
        {
            男士 = 1, 女士 = 2, 皆可 = 3
        }

        /// <summary>
        /// 拼饭用户申请状态
        /// </summary>
        public enum RiceDateApplyStatus
        {
            申请中 = 1,
            申请通过 = 2,
            被驳回 = 3,
            取消申请 = 4,
        }

        public enum RiceDateStatus
        {
            //我发布的约饭状态
            组团中 = 1,
            组团成功 = 2,
            已过期 = 3,


            //我报名的约饭状态
            被投诉 = 4,
            等待确认 = 5,
            愉快用餐 = 6
        }
    }
}
