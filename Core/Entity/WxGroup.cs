using System;
using System.Collections.Generic;

namespace YUNkefu.Core.Entity
{
    /// <summary>
    /// 微信群
    /// </summary>
    [Serializable]
    public class WxGroup
    {
        /// <summary>
        /// 群用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 群房间号
        /// </summary>
        public string RoomID { get; set; }

        /// <summary>
        /// 群昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 群成员用户名
        /// </summary>
        public List<GroupWxUser> MemberUserNames { get; set; }
    }
}
