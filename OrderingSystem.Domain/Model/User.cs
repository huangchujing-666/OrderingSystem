using System;
namespace OrderingSystem.Domain.Model
{
    public class User : IAggregateRoot
    {
        public virtual int UserId { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public virtual string NickName { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        public virtual string PhoneNo { get; set; }
        /// <summary>
        /// 头像图片Id
        /// </summary>
        public virtual int BaseImageId { get; set; }
        /// <summary>
        /// 编辑时间
        /// </summary>
        public virtual DateTime EditTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }

        /// <summary>
        /// 用户图片
        /// </summary>
        public virtual BaseImage BaseImage { get; set; }

        /// <summary>
        /// OpenId
        /// </summary>
        public virtual string OpenId { get; set; }

        public virtual int Status { get; set; }

        public virtual int IsDelete { get; set; }

        public virtual int EditPersonId { get; set; }

        public virtual int CreatePersonId { get; set; }


        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public virtual string CardNo { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int? Sex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public virtual DateTime? BirthDay { get; set; }
    }
}

