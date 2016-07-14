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
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            string url = HttpContext.Request.Url.Scheme+"://"+HttpContext.Request.Url.Host+"/Home/Index";
           string aaa= helper.RequestCodeUrl(url, "sss", snsapi_scope.snsapi_base);
            return Redirect(aaa);
            //return View();
        }
        public ActionResult Login(int? id)
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