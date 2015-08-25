using WeiXin.BLL;
using WeiXin.DAL;
using System.Web.Mvc;
using System.Collections.Generic;

namespace WebChat_MVC4.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            WeiXin.BLL.DoWei dw = new DoWei();
            if (Request.HttpMethod.ToLower().Equals("get"))//获取get请求
            {
                //微信校验
                return Content(WeiXinTools.ValidateUrl(HttpContext));
            }
            else if (Request.HttpMethod.ToLower().Equals("post"))//获取post请求
            {
               Dictionary<string,string> mode= WeiXinTools.WeiXinHandler(HttpContext);
                dw.SendHandler(mode);
            }
            else
            {
                Response.End();
                return View();
            }
            return View();
        }
    }
}