using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatHelper
{
    public class WeChatParameters
    {
        public string AppID { get; set; }
        public string AppSecret { get; set; }
        public string Token { get; set; }
        public string mch_id { get; set; }
        public string mch_key { get; set; }
        public string Note { get; set; }
    }
    public class AccessToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public DateTime get_time { get; set; }
    }
    public class CodeModel
    {
        public string code { get; set; }
        public string state { get; set; }
    }
    public class AccessToken_UserInfo
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }
        public string unionid { get; set; }
    }
    public class snsapi_UserInfo
    {
        public string openid { get; set; }
        public string nickname { get; set; }
        public string sex { get; set; }
        public string language { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string headimgurl { get; set; }
        public string[] privilege { get; set; }
        public string unionid { get; set; }
    }
    public class JSApiTicket
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string ticket { get; set; }
        public int expires_in { get; set; }
        public DateTime get_time { get; set; }
    }
    public class UserInfo
    {
        public int subscribe { get; set; }
        public string openid { get; set; }
        public string nickname { get; set; }
        public int sex { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string province { get; set; }
        public string language { get; set; }
        public string headimgurl { get; set; }
        public string subscribe_time { get; set; }
        public string unionid { get; set; }
        public string remark { get; set; }
        public string groupid { get; set; }
    }
    public enum RequestMethod
    {
        GET,
        POST,
    }
    public enum snsapi_scope
    {
        snsapi_base,
        snsapi_userinfo,
    }
}
