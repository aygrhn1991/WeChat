using BaiduHelper;
using QQHelper;
using SinaHelper;
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
        WeChatHelper.WeChatHelper wechat = new WeChatHelper.WeChatHelper();
        QQHelper.QQHelper qq = new QQHelper.QQHelper();
        SinaHelper.SinaHelper sina = new SinaHelper.SinaHelper();
        BaiduHelper.BaiduHelper baidu = new BaiduHelper.BaiduHelper();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult WeChat()
        {
            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + "/Home/WeChatCallBack";
            string red_url = wechat.RequestCodeUrl(url, "aaa", snsapi_scope.snsapi_userinfo);
            return Redirect(red_url);
        }
        public ActionResult WeChatCallBack()
        {            
            return View();
        }
        public ActionResult QQ()
        {
            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + "/Home/QQCallBack";
            string red_url = qq.RequestCodeUrl(url, "aaa", new QQAuthScopes[] { });
            return Redirect(red_url);
        }
        public ActionResult QQCallBack()
        {
            string url = HttpContext.Request.Url.AbsoluteUri;
            string token = qq.GetAccessToken(url);
            string openid = qq.GetOpenId(token);
            var sss = qq.GetUserInfo(openid, token);
            ViewBag.img = sss.figureurl_1;
            ViewBag.name = sss.nickname;
            return View();
        }        
        public ActionResult Sina()
        {
            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + "/Home/SinaCallBack";
            string red_url = sina.RequestCodeUrl(url, "aaa", new SinaAuthScopes[] { });
            return Redirect(red_url);
        }
        public ActionResult SinaCallBack()
        {
            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + "/Home/SinaCallBack";
            SinaHelper.AccessToken token = sina.GetAccessToken(url);
            string uid = token.uid;        
            var sss = sina.GetUserInfo(uid, token.access_token);
            ViewBag.img = sss.avatar_large;
            ViewBag.name = sss.screen_name;
            return View();
        }
        public ActionResult Baidu()
        {
            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + "/Home/BaiduCallBack";
            string red_url = baidu.RequestCodeUrl(url, BaiduAuthScopes.basic);
            return Redirect(red_url);
        }
        public ActionResult BaiduCallBack()
        {
            string url = HttpContext.Request.Url.Scheme + "://" + HttpContext.Request.Url.Host + "/Home/BaiduCallBack";
            BaiduHelper.UserInfo info = baidu.GetUserInfo(url);
            ViewBag.img = info.portrait;
            ViewBag.name = info.username;
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult CheckConfiguration()
        {
            string str = wechat.CheckConfiguration();
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