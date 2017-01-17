using System;
using System.Collections.Generic;
using YUNkefu.Http;
using Newtonsoft.Json.Linq;

namespace YUNkefu.Core.Entity
{
    /// <summary>
    /// 微信联系人相关内容
    /// </summary>
    public class WxContact
    {       
        private string _uin = string.Empty;
        private WXService wx = new WXService();
        /// <summary>
        /// 微信群
        /// </summary>
        private Dictionary<string, WxGroup> group_dic = new Dictionary<string, WxGroup>();   

        public WxContact(string uin)
        {
            _uin = uin;
            var entity = LoginCore.GetPassTicket(this._uin);
            if (entity != null)
            {
                wx.Sid = entity.WxSid;
                wx.Uin = this._uin;
            }
        }

        /// <summary>
        /// 初始化联系人
        /// </summary>
        public void InitContact(List<WXUser> partUsers)
        {
            var groupNames = new List<string>();
            var contact_result = wx.GetContact();
            var _allUser =new Dictionary<string,WXUser>();
            if (partUsers != null)
            {
                foreach (var u in partUsers)
                {
                    if (!_allUser.ContainsKey(u.UserName))
                    {
                        _allUser.Add(u.UserName,u);
                    }
                }
            }
            if (contact_result != null)
            {               
                foreach (JObject contact in contact_result["MemberList"])  //完整好友名单
                {
                    WXUser user = new WXUser();
                    user = Convert(contact);
                    if (!_allUser.ContainsKey(user.UserName))
                    {
                        _allUser.Add(user.UserName, user);
                    }
                }
            }

            foreach (var u in _allUser)
            {
                if (u.Key.Contains("@@"))
                {
                    groupNames.Add(u.Key);
                }
            }

            //获取所有群内联系人信息
            var bat_contact_result = wx.BatGetContact(groupNames);

            if (bat_contact_result != null)
            {
                var dic = new Dictionary<string, WxGroup>();
                foreach (JObject bat_contact in bat_contact_result["ContactList"])
                {
                    //群号
                    var roomID = bat_contact["EncryChatRoomId"].ToString();
                    //群用户名
                    var userName = bat_contact["UserName"].ToString();
                    var nickName = bat_contact["NickName"].ToString();

                    var userNameArray = new List<GroupWxUser>();
                    foreach (JObject c in bat_contact["MemberList"])
                    {
                        var m_username = c["UserName"].ToString();
                        var m_displayName = c["DisplayName"].ToString();
                        var m_nickName = c["NickName"].ToString();
                        var m_attrStatus = c["AttrStatus"].ToString();
                        if (string.IsNullOrEmpty(m_displayName)) m_displayName = m_nickName;
                        userNameArray.Add(new GroupWxUser()
                        {
                            UserName=m_username,
                            NickName=m_nickName,
                            DisplayName=m_displayName,
                            AttrStatus = m_attrStatus
                        });
                    }
                    WxGroup g = new WxGroup()
                    {
                        UserName = userName,
                        MemberUserNames = userNameArray,
                        NickName = nickName,
                        RoomID = roomID
                    };
                    dic.Add(userName, g);
                }
                WxSerializable s_g = new WxSerializable(this._uin, EnumContainer.SerializType.curr_group_user);
                s_g.Serializable(dic);  //序列化 所有群内成员 
            }
            WxSerializable s = new WxSerializable(this._uin, EnumContainer.SerializType.contact);
            s.Serializable(_allUser);  //序列化 所有联系人
            
        }

        public List<string> GetGroupUserNames()
        {
            var _allusers = getAllUsers();
            var ret =new List<string>();
            foreach (var u in _allusers)
            {
                if (u.Value.UserName.StartsWith("@@"))
                {
                    ret.Add(u.Value.UserName);
                }
            }
            return ret;
        }

        private Dictionary<string, WXUser> getAllUsers()
        {
            WxSerializable s = new WxSerializable(this._uin, EnumContainer.SerializType.contact);
            var _allUser = (Dictionary<string, WXUser>)s.DeSerializable();
            return _allUser;
        }
       

        /// <summary>
        /// 获取联系人昵称
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetNickName(string userName)
        {
            try
            {
                string nickName = string.Empty;
                var _allUser = getAllUsers();
                if (_allUser.ContainsKey(userName))
                {
                    nickName = _allUser[userName].NickName;
                }
                else
                {
                    var array = new List<string>();
                    array.Add(userName);
                    var bat_contact_result = wx.BatGetContact(array);
                    if (bat_contact_result != null)
                    {
                        foreach (JObject bat_contact in bat_contact_result["ContactList"])
                        {
                            WXUser user = Convert(bat_contact);
                            _allUser.Add(userName, user);
                            nickName = user.NickName;
                            break;
                        }
                        WxSerializable s = new WxSerializable(this._uin, EnumContainer.SerializType.contact);
                        s.Serializable(_allUser);
                    }

                }
                return nickName;
            }
            catch (Exception ex)
            {
                Tools.WriteLog(ex.ToString());
                return "";
            }
        }

        public string GetGMemberNickName(string gUserName, string userName)
        {
            var wxGroup = GetGroupMemberNames(gUserName);
            var item = wxGroup.MemberUserNames.Find(n => n.UserName == userName);
            if (item != null)
                return item.NickName;
            else
                return "";
        }

        public GroupWxUser GetGMember(string gUserName, string userName)
        {
            var wxGroup = GetGroupMemberNames(gUserName);
            if (wxGroup != null)
            {
                var item = wxGroup.MemberUserNames.Find(n => n.UserName == userName);
                return item;
            }
            else
                return new GroupWxUser();
        }

        public WxGroup GetOnLineGroupMember(string gUserName)
        {
            var g_array = new List<string>();
            g_array.Add(gUserName);
            var bat_contact_result = wx.BatGetContact(g_array);
            if (bat_contact_result != null)
            {
                var dic = new Dictionary<string, WxGroup>();
                foreach (JObject bat_contact in bat_contact_result["ContactList"])
                {
                    //群号
                    var roomID = bat_contact["EncryChatRoomId"].ToString();
                    //群用户名
                    var userName = bat_contact["UserName"].ToString();
                    var nickName = bat_contact["NickName"].ToString();
                    var userNameArray = new List<GroupWxUser>();
                    foreach (JObject c in bat_contact["MemberList"])
                    {
                        var m_username = c["UserName"].ToString();
                        var m_displayName = c["DisplayName"].ToString();
                        var m_nickName = c["NickName"].ToString();
                        var m_attrStatus = c["AttrStatus"].ToString();
                        if (string.IsNullOrEmpty(m_displayName)) m_displayName = m_nickName;
                        userNameArray.Add(new GroupWxUser()
                        {
                            UserName = m_username,
                            NickName = m_nickName,
                            DisplayName = m_displayName,
                            AttrStatus = m_attrStatus
                        });
                    }
                    WxGroup g = new WxGroup()
                    {
                        UserName = userName,
                        MemberUserNames = userNameArray,
                        NickName = nickName,
                        RoomID = roomID
                    };
                    return g;
                }
            }            
            return null;
        }

        /// <summary>
        /// 对象转换为User
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private WXUser Convert(JObject obj)
        {
            WXUser _user = new WXUser();
            _user.UserName = obj["UserName"].ToString();
            _user.City = obj["City"].ToString();
            _user.HeadImgUrl = obj["HeadImgUrl"].ToString();
            _user.NickName = obj["NickName"].ToString();
            _user.Province = obj["Province"].ToString();
            _user.PYQuanPin = obj["PYQuanPin"].ToString();
            _user.RemarkName = obj["RemarkName"].ToString();
            _user.RemarkPYQuanPin = obj["RemarkPYQuanPin"].ToString();
            _user.Sex = obj["Sex"].ToString();
            _user.Signature = obj["Signature"].ToString();
            return _user;
        }

        /// <summary>
        /// 获取群内成员信息
        /// </summary>
        /// <param name="groupUserName"></param>
        /// <returns></returns>
        public WxGroup GetGroupMemberNames(string groupUserName)
        {
            WxSerializable s = new WxSerializable(this._uin, EnumContainer.SerializType.curr_group_user);
            var dic = (Dictionary<string, WxGroup>)s.DeSerializable();
            if (dic != null && dic.ContainsKey(groupUserName))
            {
                return dic[groupUserName];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加群内用户
        /// </summary>
        /// <param name="groupUserName"></param>
        /// <param name="?"></param>
        public void Add(string groupUserName,WxGroup group)
        {
            WxSerializable s = new WxSerializable(this._uin, EnumContainer.SerializType.curr_group_user);
            var dic = (Dictionary<string, WxGroup>)s.DeSerializable();
            if (dic != null && dic.ContainsKey(groupUserName))
            {
                dic.Remove(groupUserName);
            }
            dic.Add(groupUserName, group);
            s.Serializable(dic); //序列化
        }


    }
}
