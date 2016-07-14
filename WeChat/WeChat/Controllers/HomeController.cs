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