using System;
using System.Collections.Generic;

using System.Text;

namespace YUNkefu.Core.Entity
{
    public class EnumContainer
    {
        public enum SerializType
        {
            /// <summary>
            /// 联系人
            /// </summary>
            contact,
            /// <summary>
            /// 所有联系人
            /// </summary>
            bat_contact,
            /// <summary>
            /// 登录
            /// </summary>
            pass_ticket,
            /// <summary>
            /// 通讯key
            /// </summary>
            sync_key,
            /// <summary>
            /// 在线机器人列表
            /// </summary>
            user_online,
            /// <summary>
            /// 当前群用户信息
            /// </summary>
            curr_group_user,
            /// <summary>
            /// 缓存
            /// </summary>
            cookie,
        }

        /// <summary>
        /// 关键词匹配模式
        /// </summary>
        public enum MatchingModeEnum
        {
            完全匹配 = 0,
            模糊匹配 = 1,
        }

        /// <summary>
        /// 微信回复内容类型
        /// </summary>
        public enum WeChatContentTypeEnum
        {
            文本 = 0,
            图片 = 1,
            链接 = 2
        }

        /// <summary>
        /// 广告分类枚举
        /// </summary>
        public enum AdverCategoryEnum
        {
            公告语 = 0,
        }

        /// <summary>
        /// 发送模式
        /// </summary>
        public enum SendModeEnum
        {
            每隔多少分钟 = 0,
            指定具体时间 = 1,
            每天某一时刻 = 2
        }

        /// <summary>
        /// 通用发送开关枚举
        /// </summary>
        public enum CommonSwichEnum
        {
            通用语句 = 0,
            关闭不发送 = 1,
            自定义语句 = 2
        }
    }
}
