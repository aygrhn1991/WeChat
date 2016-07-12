using System;
using System.Collections.Generic;
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
        public void CheckConfiguration()
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
                HttpContext.Current.Response.Write(echostr);
        }
    }
}
