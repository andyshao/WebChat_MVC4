using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiXin.IBLL
{
    public interface IDoWei
    {
        //-------------------------
        #region 接收普通消息
        /// <summary>
        /// 接收文本消息
        /// </summary>
        /// <param name="model"></param>
        void DoText(Dictionary<string, string> model);

        /// <summary>
        /// 接收到图片消息
        /// </summary>
        /// <param name="model"></param>
        void DoImg(Dictionary<string, string> model);

        /// <summary>
        /// 接收到语音消息
        /// </summary>
        /// <param name="model"></param>
        void DoVoice(Dictionary<string, string> model);

        /// <summary>
        /// 接收到视频消息
        /// </summary>
        /// <param name="model"></param>
        void DoVideo(Dictionary<string, string> model);

        /// <summary>
        /// 接收到地理位置信息
        /// </summary>
        /// <param name="model"></param>
        void DoLocation(Dictionary<string, string> model);

        /// <summary>
        /// 接收到链接消息
        /// </summary>
        /// <param name="Model"></param>
        void DoLink(Dictionary<string, string> Model);

        #endregion
        //-------------------------


        //-------------------------
        #region 接收事件消息

        /// <summary>
        /// 普通关注
        /// </summary>
        /// <param name="Model"></param>
        void DoOn(Dictionary<string, string> Model);

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <param name="Model"></param>
        void DoUnOn(Dictionary<string, string> Model);

        /// <summary>
        /// 未关注用户扫描二维码参数
        /// </summary>
        /// <param name="Model"></param>
        void DoOnCode(Dictionary<string, string> Model);

        /// <summary>
        /// 已经关注的用户扫描二维码参数
        /// </summary>
        /// <param name="Model"></param>
        void DoSubCode(Dictionary<string, string> Model);

        /// <summary>
        /// 用户提交地理位置
        /// </summary>
        /// <param name="Model"></param>
        void DoSubLocation(Dictionary<string, string> Model);

        /// <summary>
        /// 自定义菜单点击
        /// </summary>
        /// <param name="Model"></param>
        void DoSubClick(Dictionary<string, string> Model);

        /// <summary>
        /// 自定义菜单跳转
        /// </summary>
        /// <param name="Model"></param>
        void DoSubView(Dictionary<string, string> Model);

        #endregion
        //-------------------------
    }
}
