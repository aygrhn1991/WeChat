using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WeChatHelper
{
    public class WeChatHelper
    {
        Helper helper = new Helper();

        #region 服务器配置
        public string CheckConfiguration()
        {
            string signature = HttpContext.Current.Request["signature"];
            string timestamp = HttpContext.Current.Request["timestamp"];
            string nonce = HttpContext.Current.Request["nonce"];
            string echostr = HttpContext.Current.Request["echostr"];
            string token = helper.GetWeChatParameters().Token;

            string[] tempArray = { token, timestamp, nonce };
            Array.Sort(tempArray);
            string tempString = string.Join("", tempArray);

            SHA1 sha1 = SHA1.Create();
            byte[] tempBytes = Encoding.UTF8.GetBytes(tempString);
            tempBytes = sha1.ComputeHash(tempBytes);
            sha1.Clear();
            string resultSring = BitConverter.ToString(tempBytes).Replace("-", "").ToLower();
            if (resultSring == signature)
                //HttpContext.Current.Response.Write(echostr);
                return echostr;
            return "error";
        }
        #endregion

        #region 基础accesstoken
        private string GetAccessToken()
        {
            string path = HttpContext.Current.Server.MapPath("~/access_token.json");
            if (File.Exists(path))
            {
                string tempString = helper.ReadFromFile(path);
                if (string.IsNullOrWhiteSpace(tempString))
                {
                    RequestAccessToken();
                }
                else
                {
                    AccessToken accesstoken = helper.GetObject(new AccessToken());
                    if ((DateTime.Now - accesstoken.get_time).TotalSeconds > accesstoken.expires_in)
                    {
                        RequestAccessToken();
                    }
                }
            }
            else
            {
                RequestAccessToken();
            }
            return helper.GetObject(new AccessToken()).access_token;
        }
        private void RequestAccessToken()
        {
            WeChatParameters parameters = helper.GetWeChatParameters();
            string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + parameters.AppID + "&secret=" + parameters.AppSecret;
            string response = helper.HttpHelper(url, RequestMethod.GET);
            response = response.Replace("}", ",\"get_time\":\"" + DateTime.Now.ToString() + "\"}");
            string path = HttpContext.Current.Server.MapPath("~/access_token.json");
            helper.WriteToFile(response, path);
        }
        #endregion

        #region 网页授权获取用户信息
        public string RequestCodeUrl(string unEncodedRediretUri, string state, snsapi_scope scope)
        {
            WeChatParameters parameters = helper.GetWeChatParameters();
            return "https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + parameters.AppID + "&redirect_uri=" + HttpUtility.UrlEncode(unEncodedRediretUri) + "&response_type=code&scope=" + scope.ToString() + "&state=" + state + "#wechat_redirect";
        }
        public string GetOpenIdBy_snsapi_base()
        {
            string code = GetCode().code;
            return RequestInfoAccessToken(code).openid;
        }
        public snsapi_UserInfo GetUserInfoBy_snsapi_userinfo()
        {
            string code = GetCode().code;
            AccessToken_UserInfo accesstoken_userinfo = RequestInfoAccessToken(code);
            string url = "https://api.weixin.qq.com/sns/userinfo?access_token=" + accesstoken_userinfo.access_token + "&openid=" + accesstoken_userinfo.openid + "&lang=zh_CN";
            string response = helper.HttpHelper(url, RequestMethod.GET);
            return helper.GetObject(new snsapi_UserInfo(), response);
        }
        private CodeModel GetCode()
        {
            CodeModel codemodel = new CodeModel();
            codemodel.code = HttpContext.Current.Request["code"];
            codemodel.state = HttpContext.Current.Request["state"];
            return codemodel;
        }
        private AccessToken_UserInfo RequestInfoAccessToken(string code)
        {
            WeChatParameters parameters = helper.GetWeChatParameters();
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + parameters.AppID + "&secret=" + parameters.AppSecret + "&code=" + code + "&grant_type=authorization_code";
            string response = helper.HttpHelper(url, RequestMethod.GET);
            return helper.GetObject(new AccessToken_UserInfo(), response);
        }
        #endregion






    }
}
