using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Text;

namespace WeChat.WeChatLoginHelper
{
    public class WeChatMiddleware : OwinMiddleware
    {
        public WeChatMiddleware(OwinMiddleware next) : base(next)
        {

        }
        public override Task Invoke(IOwinContext context)
        {
            //throw new NotImplementedException();

            if (Next != null && context.Request.Path.Value.StartsWith("/wechat-scan"))
            {
                var hubid = context.Request.Query["hubId"];
                var hub = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<WeChatHub>().Clients.Client(hubid);
                hub.callback("waiting-authorize");
                WeChatHelper.WeChatHelper helper = new WeChatHelper.WeChatHelper();
                string redirect_uri = context.Request.Uri.Scheme + "://" + context.Request.Host + "/wechat-getcode?hubId=" + hubid;
                string url = helper.RequestCodeUrl(redirect_uri, hubid, WeChatHelper.snsapi_scope.snsapi_userinfo);
                context.Response.Redirect(url);
                //return Task.FromResult<int>(0);
                return Task.FromResult(0);
            }
            else if (Next != null && context.Request.Path.Value.StartsWith("/wechat-getcode"))
            {
                var hubid = context.Request.Query["hubId"];
                var hub = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<WeChatHub>().Clients.Client(hubid);
                hub.callback("getting-userinfo");
                WeChatHelper.WeChatHelper helper = new WeChatHelper.WeChatHelper();
                var userinfo = helper.GetUserInfoBy_snsapi_userinfo();
                //根据获取的信息进行相关业务操作，以下为示例
                if (userinfo == null)
                {
                    hub.callback("failed");
                }
                else
                {
                    //查询数据库，看userinfo.openid是否为已有用户
                    if (userinfo.openid == "ofX-_vnQH2aFLclfxkkh24JLXkgI")
                    {
                        hub.callback("success");
                        //用户存在，登录
                    }
                    else
                    {
                        hub.callback("newuser");
                        //添加新用户
                    }
                }
                context.Response.Write("<html lang=\"zh-CN\"><head></head><body><script>function onBridgeReady(){WeixinJSBridge.call('closeWindow');}if(typeof WeixinJSBridge == \"undefined\"){document.addEventListener('WeixinJSBridgeReady', onBridgeReady, false);}else{ onBridgeReady();}</script></body></html>");
                return Task.FromResult(0);
            }
            else
                return Next.Invoke(context);
        }
    }
}