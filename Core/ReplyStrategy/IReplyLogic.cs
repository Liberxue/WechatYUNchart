using YUNkefu.Core.Entity;

namespace YUNkefu.Core.ReplyStrategy
{
    /// <summary>
    /// 接收内容逻辑
    /// </summary>
    public interface IReplyLogic
    {
        /// <summary>
        /// 构造回复内容
        /// </summary>
        /// <returns></returns>
        SendMsg MakeContent(WXMsg msg);
    }
}
