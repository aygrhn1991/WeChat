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
            //string url = helper.RequestCodeUrl("http://cyf.ngrok.cc/Home/Index2", "aaa", snsapi_scope.snsapi_userinfo);
            //return Redirect(url);
        }
        public ActionResult Index2()
        {
            var s = helper.GetUserInfo("ofX-_vnQH2aFLclfxkkh24JLXkgI");
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