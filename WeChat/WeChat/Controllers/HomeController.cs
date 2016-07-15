using QQHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeChatHelper;

namespace WeChat.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        WeChatHelper.WeChatHelper helper = new WeChatHelper.WeChatHelper();
        QQHelper.QQHelper help = new QQHelper.QQHelper();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + "/Home/Index";
            string aaa = helper.RequestCodeUrl(url, "sss", snsapi_scope.snsapi_userinfo);
            return Redirect(aaa);
            //return View();
        }
        public ActionResult Index3()
        {
            string url = HttpContext.Request.Url.AbsoluteUri;
            string token = help.GetAccessToken(url);
            string openid = help.GetOpenId(token);
            var sss = help.GetUserInfo(openid, token);
            ViewBag.img = sss.figureurl_1;
            ViewBag.name = sss.nickname;
            return View();
        }
        public ActionResult QQLogin()
        {
            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + "/Home/Index3";
            string aaa = help.RequestCodeUrl(url, "123", new QQAuthScopes[] { });
            return Redirect(aaa);
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult LoginSuccess()
        {
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult CheckConfiguration()
        {
            string str = helper.CheckConfiguration();
            if (str == "error")
            {
                return null;
            }
            else
            {
                HttpContext.Response.Write(str);
                return null;
            }
        }
    }
}