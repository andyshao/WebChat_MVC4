using System.Collections.Generic;
using System.Xml;
using System.Web;

namespace Common
{
    /// <summary>
    /// xml操作
    /// </summary>
    public static class ReadXml
    {


        /// <summary>
        /// 把接收到的XML转为字典
        /// </summary>
        /// <param name="xmlStr">xml字符串</param>
        /// <returns></returns>
        public static Dictionary<string, string> XmlModel(string xmlStr)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlStr);
            Dictionary<string, string> mo = new Dictionary<string, string>();
            var data = doc.DocumentElement.ChildNodes;
            //.SelectNodes("xml");
            for (int i = 0; i < data.Count; i++)
            {
                mo.Add(data.Item(i).LocalName, data.Item(i).InnerText);
            }
            return mo;
        }



        /// <summary>
        /// 从字典中读取指定的值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="model">字典对象</param>
        /// <returns></returns>
        public static string ReadModel(string key, Dictionary<string, string> model)
        {
            string str = "";
            model.TryGetValue(key, out str);
            if (str == null)
            {
                str = "";
            }

            return str;
        }
        /// <summary>
        /// 输出字符串并结束当前页面进程 MVC必须加return
        /// </summary>
        /// <param name="str"></param>
        public static void ResponseToEnd(string str)
        {
            HttpContext.Current.Response.Write(str);
            HttpContext.Current.Response.End();
            return;
        }

        /// <summary>
        /// 输出字符串并结束当前页面进程 MVC必须加return
        /// </summary>
        /// <returns></returns>
        public static string Menu()
        {
            string Content = "";
            Content += "欢迎来到  P了个F /微笑\r\n";
            Content += "输入以下序号开始获取最新信息：\r\n";
            Content += "1,最新IT资讯\r\n";
            Content += "2,电影预告\r\n";
            Content += "3,今日说法\r\n";
            Content += "4,焦点访谈\r\n";
            Content += "5,新闻联播\r\n";
            Content += "<a href='http://www.cnblogs.com/ypfnet'>关注我的博客</a>\r\n";
            Content += "输入?或帮助 可以显示此菜单";
            return Content;
        }


    }
}
