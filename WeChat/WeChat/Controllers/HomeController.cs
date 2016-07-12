using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WeChat.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            WeChatHelper.WeChatHelper helper = new WeChatHelper.WeChatHelper();
            
            return View();
        }
    }
}