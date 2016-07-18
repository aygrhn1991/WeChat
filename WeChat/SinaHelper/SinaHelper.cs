using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SinaHelper
{
    public class SinaHelper
    {
        Helper helper = new Helper();
        public string RequestCodeUrl(string unEncodedRediretUri, string state, SinaAuthScopes[] scopes)
        {
            string scope = "";
            if (scopes.Length > 0)
            {
                string scopeStr = string.Join(",", scopes);
                scope = "&scope=" + scopeStr;
            }
            SinaParameters parameters = helper.GetSinaParameters();
            return "https://api.weibo.com/oauth2/authorize?client_id=" + parameters.AppKey + "&redirect_uri=" +HttpUtility.UrlEncode(unEncodedRediretUri) + "&state=" + state + scope + "&response_type=code";
        }
        public AccessToken GetAccessToken(string unEncodedRediretUri)
        {
            SinaParameters parameters = helper.GetSinaParameters();
            string code = GetCode().code;
            string url = "https://api.weibo.com/oauth2/access_token?client_id=" + parameters.AppKey + "&client_secret=" + parameters.AppSecret + "&grant_type=authorization_code&redirect_uri=" + HttpUtility.UrlEncode(unEncodedRediretUri) + "&code=" + code;
            string response = helper.HttpHelper(url, RequestMethod.POST);
            return helper.GetObject(new AccessToken(), response);
        }
        public string GetUId(string unEncodedRediretUri)
        {
            return GetAccessToken(unEncodedRediretUri).uid;
        }
        public UserInfo GetUserInfo(string uid, string accessToken)
        {
            string url = "https://api.weibo.com/2/users/show.json?access_token=" + accessToken + "&uid=" + uid;
            string response = helper.HttpHelper(url, RequestMethod.GET);
            return helper.GetObject(new UserInfo(), response);
        }
        private CodeModel GetCode()
        {
            CodeModel codemodel = new CodeModel();
            codemodel.code = HttpContext.Current.Request["code"];
            codemodel.state = HttpContext.Current.Request["state"];
            return codemodel;
        }

    }
}
