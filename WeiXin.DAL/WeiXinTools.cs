using Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Security;
using WeiXin.IDAL;
using WeiXin.Model;

namespace WeiXin.DAL
{
    public class WeiXinTools : IWeiXinTools
    {
        #region 微信校验

        /// <summary>
        /// 微信校验
        /// </summary>
        /// <param name="context">上下文对象</param>
        public static string ValidateUrl(HttpContextBase context)
        {
            string myToken = ConfigurationManager.AppSettings["WeixinToken"];//从配置文件获取Token
            //获取验证参数
            string signature = context.Request["signature"];
            string timestamp = context.Request["timestamp"];
            string nonce = context.Request["nonce"];
            string echostr = context.Request["echostr"];
            string token = myToken;


            //创建参数数组
            string[] temp1 = { token, timestamp, nonce };
            //按字母排序
            Array.Sort(temp1);
            //将数组转成字符串
            string temp2 = string.Join("", temp1);
            //字符串进行sha1加密
            string temp3 = FormsAuthentication.HashPasswordForStoringInConfigFile(temp2, "SHA1");
            //对比
            if (temp3.ToLower().Equals(signature))
            {
                //context.Response.Write(echostr);
                return echostr;
            }
            else
            {
                return null;
            }

        }

        #endregion

        #region 微信数据接收处理
        /// <summary>
        /// 微信数据接收处理
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Dictionary<string, string> WeiXinHandler(HttpContextBase context)
        {
            string xmlData = string.Empty;
            Dictionary<string, string> Model = new Dictionary<string, string>();

            using (Stream stream = context.Request.InputStream)
            {
                Byte[] byteData = new Byte[stream.Length];
                stream.Read(byteData, 0, (Int32)stream.Length);
                xmlData = Encoding.UTF8.GetString(byteData);
            }
            if ((xmlData + "").Length > 0)//将接收到的xml数据转成字典数据
            {
                try
                {
                    Model = ReadXml.XmlModel(xmlData);
                }
                catch
                {
                    //未能正确处理 给微信服务器回复默认值
                    ReadXml.ResponseToEnd("");
                }
            }
            return Model;
        } 
        #endregion


        public string SendText(SText model)
        {
            return string.Format(@"<xml>
                        <ToUserName><![CDATA[{0}]]></ToUserName>
                        <FromUserName><![CDATA[{1}]]></FromUserName>
                        <CreateTime>{2}</CreateTime>
                        <MsgType><![CDATA[{3}]]></MsgType>
                        <Content><![CDATA[{4}]]></Content>
                     </xml>", model.ToUserName, model.FromUserName, model.CreateTime, model.MsgType, model.Content);
        }



        public string SendImg(SImg model)
        {
            return string.Format(@"<xml>
                                    <ToUserName><![CDATA[{0}]]></ToUserName>
                                    <FromUserName><![CDATA[{1}]]></FromUserName>
                                    <CreateTime>{2}</CreateTime>
                                    <MsgType><![CDATA[{3}]]></MsgType>
                                    <Image>
                                    <MediaId><![CDATA[{4}]]></MediaId>
                                    </Image>
                                    </xml>", model.ToUserName, model.FromUserName, model.CreateTime, model.MsgType, model.MediaId);
        }

        public string SendVoice(SVoice model)
        {
            return string.Format(@"<xml>
                                    <ToUserName><![CDATA[{0}]]></ToUserName>
                                    <FromUserName><![CDATA[{1}]]></FromUserName>
                                    <CreateTime>{2}</CreateTime>
                                    <MsgType><![CDATA[{3}]]></MsgType>
                                    <Voice>
                                    <MediaId><![CDATA[{4}]]></MediaId>
                                    </Voice>
                                    </xml>", model.ToUserName, model.FromUserName, model.CreateTime, model.MsgType, model.MediaId);
        }

        public string SendVideo(SVideo model)
        {
            return string.Format(@"<xml>
                                    <ToUserName><![CDATA[{0}]]></ToUserName>
                                    <FromUserName><![CDATA[{1}]]></FromUserName>
                                    <CreateTime>{2}</CreateTime>
                                    <MsgType><![CDATA[{3}]]></MsgType>
                                    <Video>
                                    <MediaId><![CDATA[{4}]]></MediaId>
                                    <Title><![CDATA[{5}]]></Title>
                                    <Description><![CDATA[{6}]]></Description>
                                    </Video> 
                                    </xml>", model.ToUserName, model.FromUserName, model.CreateTime, model.MsgType, model.MediaId, model.Title, model.Description);

        }

        public string SendMusic(Smusic model)
        {
            return string.Format(@"<xml>
                                    <ToUserName><![CDATA[{0}]]></ToUserName>
                                    <FromUserName><![CDATA[{1}]]></FromUserName>
                                    <CreateTime>{2}</CreateTime>
                                    <MsgType><![CDATA[{3}]]></MsgType>
                                    <Music>
                                    <Title><![CDATA[{4}]]></Title>
                                    <Description><![CDATA[{5}]]></Description>
                                    <MusicUrl><![CDATA[{6}]]></MusicUrl>
                                    <HQMusicUrl><![CDATA[{7}]]></HQMusicUrl>
                                    <ThumbMediaId><![CDATA[{8}]]></ThumbMediaId>
                                    </Music>
                                    </xml>", model.ToUserName, model.FromUserName,
                                           model.CreateTime, model.MsgType,
                                           model.Title, model.Description,
                                           model.MusicURL, model.HQMusicUrl, model.ThumbMediaId);


        }

        public string SendNews(SNews model)
        {
            string art = "";
            foreach (var one in model.Articles)
            {

                art += string.Format(@"
                                    <item>
                                    <Title><![CDATA[{0}]]></Title>
                                    <Description><![CDATA[{1}]]></Description>
                                    <PicUrl><![CDATA[{2}]]></PicUrl>
                                    <Url><![CDATA[{3}]]></Url>
                                    </item>", one.Title, one.Description, one.PicUrl, one.Url);


            }

            return string.Format(@"<xml>
                                    <ToUserName><![CDATA[{0}]]></ToUserName>
                                    <FromUserName><![CDATA[{1}]]></FromUserName>
                                    <CreateTime>{2}</CreateTime>
                                    <MsgType><![CDATA[{3}]]></MsgType>
                                    <ArticleCount>{4}</ArticleCount>
                                    <Articles>{5}</Articles>
                                    </xml> ", model.ToUserName, model.FromUserName, model.CreateTime, model.MsgType,
                                            model.ArticleCount, art);
        }
    }
}
