using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaiduHelper
{
    public class BaiduParameters
    {
        public string APIKey { get; set; }
        public string SecretKey { get; set; }
        public string Note { get; set; }
    }
    public class AccessToken
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public string session_key { get; set; }
        public string session_secret { get; set; }
    }
    public class UserInfo
    {
        public string userid { get; set; }
        public string username { get; set; }
        public string realname { get; set; }
        public string portrait { get; set; }
        public string userdetail { get; set; }
        public string birthday { get; set; }
        public string marriage { get; set; }
        public string sex { get; set; }
        public string blood { get; set; }
        public string figure { get; set; }
        public string constellation { get; set; }
        public string education { get; set; }
        public string trade { get; set; }
        public string job { get; set; }
    }
    public enum BaiduAuthScopes
    {
        basic,
        super_msg,
        netdisk,
    }
    public enum RequestMethod
    {
        GET,
        POST,
    }
}
