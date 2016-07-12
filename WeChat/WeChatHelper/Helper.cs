using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WeChatHelper
{
    public class Helper
    {
        public WeChatParameters GetWeChatParameters()
        {
            return GetObject(new WeChatParameters());
        }
        private string ReadFromFile(string absolutePath)
        {
            using (StreamReader reader = new StreamReader(absolutePath))
            {
                string str = reader.ReadToEnd();
                return str;
            }
        }
        public T GetObject<T>(T model)
        {
            string path;
            if (model is AccessToken)
            {
                path = HttpContext.Current.Server.MapPath("~/access_token.json");
            }
            else if (model is JSApiTicket)
            {
                path = HttpContext.Current.Server.MapPath("~/jsapi_ticket.json");
            }
            else
            {
                path = HttpContext.Current.Server.MapPath("~/wechat_parameters.json");
            }
            string str = ReadFromFile(path);
            using (StringReader stringReader = new StringReader(str))
            {
                JsonSerializer serializer = new JsonSerializer();
                object obj = serializer.Deserialize(new JsonTextReader(stringReader), typeof(T));
                //T result = obj as T;
                T result = (T)obj;
                return result;
            }
        }
    }
}
