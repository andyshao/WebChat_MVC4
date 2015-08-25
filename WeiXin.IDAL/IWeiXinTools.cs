using WeiXin.Model;

namespace WeiXin.IDAL
{
    public interface IWeiXinTools
    {
        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string SendText(SText model);
        /// <summary>
        /// 发送图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string SendImg(SImg model);
        /// <summary>
        /// 发送语音
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string SendVoice(SVoice model);
        /// <summary>
        /// 发送视频
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string SendVideo(SVideo model);
        /// <summary>
        /// 发送音乐
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string SendMusic(Smusic model);
        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        string SendNews(SNews model);

    }
}
