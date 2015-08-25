using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeiXin.IBLL;
using WeiXin.Model;

namespace WeiXin.BLL
{
    public class DoWei : IDoWei
    {
        public WeiXin.IDAL.IWeiXinTools DALWei = new WeiXin.DAL.WeiXinTools();

        //---------------------------------------------------------------------------------
        #region 接收普通消息
        //接收到文本消息  处理
        public void DoText(Dictionary<string, string> model)
        {

            SText mT = new SText();
            string text = ReadXml.ReadModel("Content", model).Trim();
            mT.FromUserName = ReadXml.ReadModel("ToUserName", model);
            mT.ToUserName = ReadXml.ReadModel("FromUserName", model);
            mT.CreateTime = long.Parse(ReadXml.ReadModel("CreateTime", model));
            if (text == "?" || text == "？" || text == "帮助")
            {
                mT.Content = mT.Content = ReadXml.Menu();
                mT.MsgType = "text";
                ReadXml.ResponseToEnd(DALWei.SendText(mT));
            }
            else
            {
                SNews mN = new SNews();
                mN.FromUserName = ReadXml.ReadModel("ToUserName", model);
                mN.ToUserName = ReadXml.ReadModel("FromUserName", model);
                mN.CreateTime = long.Parse(ReadXml.ReadModel("CreateTime", model));
                mN.MsgType = "news";

                //   以下为文章内容，  实际使用时，此处应该是一个跟数据库交互的方法，查询出文章
                //文章条数，  文章内容等   都应该由数据库查询出来的数据决定   这里测试，就模拟几条

                mN.ArticleCount = 3;
                List<ArticlesModel> listNews = new List<ArticlesModel>();
                for (int i = 0; i < 3; i++)
                {
                    ArticlesModel ma = new ArticlesModel();
                    ma.Title = "这是第" + (i + 1).ToString() + "篇文章";
                    ma.Description = "-描述-" + i.ToString() + "-描述-";
                    ma.PicUrl = "http://qxw1193030272.my3w.com/images/Microsoft .png";
                    ma.Url = "http://www.cnblogs.com/ypfnet/";
                    listNews.Add(ma);
                }
                mN.Articles = listNews;
                ReadXml.ResponseToEnd(DALWei.SendNews(mN));
            }
        }








        //接收到图片消息
        public void DoImg(Dictionary<string, string> model)
        {

        }

        //接收到语音消息
        public void DoVoice(Dictionary<string, string> model)
        {

        }
        //接收到视频消息
        public void DoVideo(Dictionary<string, string> model)
        {

        }


        //接收到地理位置信息
        public void DoLocation(Dictionary<string, string> model)
        {

        }
        //接收到链接消息
        public void DoLink(Dictionary<string, string> Model)
        {

        }
        #endregion
        //----------------------------------------------------------------------------

        //----------------------------------------------------------------------------
        #region 接收事件消息
        //普通关注
        public void DoOn(Dictionary<string, string> model)
        {
            SText mT = new SText();
            mT.Content = ReadXml.Menu();
            mT.FromUserName = ReadXml.ReadModel("ToUserName", model);
            mT.ToUserName = ReadXml.ReadModel("FromUserName", model);
            mT.MsgType = "text";
            mT.CreateTime = long.Parse(ReadXml.ReadModel("CreateTime", model));
            ReadXml.ResponseToEnd(DALWei.SendText(mT));
        }
        //取消关注
        public void DoUnOn(Dictionary<string, string> model)
        {

        }
        //未关注用户扫描二维码参数
        public void DoOnCode(Dictionary<string, string> model)
        {
            SText mT = new SText();
            mT.Content = ReadXml.Menu();
            mT.FromUserName = ReadXml.ReadModel("ToUserName", model);
            mT.ToUserName = ReadXml.ReadModel("FromUserName", model);
            mT.MsgType = "text";
            mT.CreateTime = long.Parse(ReadXml.ReadModel("CreateTime", model));
            ReadXml.ResponseToEnd(DALWei.SendText(mT));
        }
        //已经关注的用户扫描二维码参数
        public void DoSubCode(Dictionary<string, string> model)
        {
            SText mT = new SText();
            mT.Content = ReadXml.Menu();
            mT.FromUserName = ReadXml.ReadModel("ToUserName", model);
            mT.ToUserName = ReadXml.ReadModel("FromUserName", model);
            mT.MsgType = "text";
            mT.CreateTime = long.Parse(ReadXml.ReadModel("CreateTime", model));
            ReadXml.ResponseToEnd(DALWei.SendText(mT));
        }





        //用户提交地理位置
        public void DoSubLocation(Dictionary<string, string> Model)
        {
            throw new NotImplementedException();
        }
        //自定义菜单点击
        public void DoSubClick(Dictionary<string, string> Model)
        {
            throw new NotImplementedException();
        }
        //自定义菜单跳转
        public void DoSubView(Dictionary<string, string> Model)
        {
            throw new NotImplementedException();
        }

        #endregion
        //-------------------------------------------------------------------------------------

            /// <summary>
            /// 消息回复处理
            /// </summary>
            /// <param name="mode"></param>
        public void SendHandler(Dictionary<string, string> mode)
        {
            if (mode.Count > 0)
            {
                string msgType = ReadXml.ReadModel("MsgType", mode);
                #region 判断消息类型
                switch (msgType)
                {
                    case "text":
                        DoText(mode);//文本消息
                        break;
                    case "image":
                        DoImg(mode);//图片
                        break;
                    case "voice": //声音
                        DoVoice(mode);
                        break;

                    case "video"://视频
                        DoVideo(mode);
                        break;

                    case "location"://地理位置
                        DoLocation(mode);
                        break;
                    case "link"://链接
                        DoLink(mode);
                        break;
                    #region 事件
                    case "event":
                        switch (ReadXml.ReadModel("Event", mode))
                        {
                            case "subscribe":
                                if (ReadXml.ReadModel("EventKey", mode).IndexOf("qrscene_") >= 0)
                                {
                                    DoOnCode(mode);//带参数的二维码扫描关注
                                }
                                else
                                {
                                    DoOn(mode);//普通关注
                                }
                                break;
                            case "unsubscribe":
                                DoUnOn(mode);//取消关注
                                break;

                            case "SCAN":
                                DoSubCode(mode);//已经关注的用户扫描带参数的二维码
                                break;
                            case "LOCATION"://用户上报地理位置
                                DoSubLocation(mode);
                                break;
                            case "CLICK"://自定义菜单点击
                                DoSubClick(mode);
                                break;
                            case "VIEW"://自定义菜单跳转事件
                                DoSubView(mode);
                                break;
                        };
                        break;
                        #endregion
                }
                #endregion
            }
        }
    }
}
