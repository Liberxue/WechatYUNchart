using System;
using System.Collections.Generic;

using System.Text;
using YUNkefu.Core.Entity;

namespace YUNkefu.Core.ReplyStrategy
{
    /// <summary>
    /// 回复内容策略上下文
    /// </summary>
    public class ReplyContext
    {
        private IReplyLogic logic = null;       
        public  ReplyContext(IReplyLogic logic)
        {
            this.logic = logic;
        }

        public SendMsg MakeContent(WXMsg msg)
        {
            if (this.logic != null)
            {
                return logic.MakeContent(msg);
            }
            else
                return new SendMsg();
        }        
    }

    public class ReplyFactory
    {
        public static ReplyContext Create(WXMsg msg)
        {
            ReplyContext context = new ReplyContext(null);
            switch (msg.Type)
            {
                case 10000:
                    //进群，退群，红包
                    context = new ReplyContext(MakeSysReplyLogic(msg.Msg));
                    break;
                case 1:
                    //文本内容
                    context = new ReplyContext(new ChatLogic());
                    break;
            }
            return context;
        }


        private static IReplyLogic MakeSysReplyLogic(string msg)
        {
            if (msg.Contains("加入了群聊") || msg.Contains("加入群聊"))
            {
                return new WelcomeLogic();
            }
            if (msg.Contains("移出了群聊"))
            {
                return new QuitGroupLogic();
            }
            if (msg.Contains("收到红包"))
            {
                return new RedWalletLogic();
            }
            return null;
        }
    }
}
