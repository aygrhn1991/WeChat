using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BaiduHelper
{
    public class BaiduHelper
    {
        Helper helper = new Helper();
        public string RequestCodeUrl(string unEncodedRediretUri, BaiduAuthScopes scope)
        {
            BaiduParameters parameters = helper.GetBaiduParameters();
            return "http://openapi.baidu.com/oauth/2.0/authorize?response_type=code&client_id=" + parameters.APIKey + "&redirect_uri=" + HttpUtility.UrlEncode(unEncodedRediretUri) + "&scope=" + scope.ToString();
        }
        public UserInfo GetUserInfo(string unEncodedRediretUri)
        {
            string accessToken = RequestAccessToken(unEncodedRediretUri).access_token;
            string url = "https://openapi.baidu.com/rest/2.0/passport/users/getInfo?access_token=" + accessToken;
            string response = helper.HttpHelper(url, RequestMethod.GET);
            return helper.GetObject(new UserInfo(), response);
        }
        private string GetCode()
        {
            string code = HttpContext.Current.Request["code"];
            return code;
        }
        private AccessToken RequestAccessToken(string unEncodedRediretUri)
        {
            string code = GetCode();
            BaiduParameters parameters = helper.GetBaiduParameters();
            string url = "https://openapi.baidu.com/oauth/2.0/token?grant_type=authorization_code&code=" + code + "&client_id=" + parameters.APIKey + "&client_secret=" + parameters.SecretKey + "&redirect_uri=" + HttpUtility.UrlEncode(unEncodedRediretUri);
            string response = helper.HttpHelper(url, RequestMethod.GET);
            AccessToken accessToken = helper.GetObject(new AccessToken(), response);
            return accessToken;
        }


    }
}
