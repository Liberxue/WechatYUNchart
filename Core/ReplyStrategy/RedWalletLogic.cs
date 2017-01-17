using YUNkefu.Core.Entity;

namespace YUNkefu.Core.ReplyStrategy
{
    /// <summary>
    /// 接收发送红包消息
    /// </summary>
    public class RedWalletLogic:IReplyLogic
    {
        public SendMsg MakeContent(WXMsg msg)
        {
            SendMsg m = new SendMsg();
            m.type = 1;
            m.context="云客服提醒：恭喜发财，红包拿来";
            return m;            
        }
    }
}
