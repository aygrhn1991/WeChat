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
    public class JSApiTicket
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public string ticket { get; set; }
        public int expires_in { get; set; }
        public DateTime get_time { get; set; }
    }
}
