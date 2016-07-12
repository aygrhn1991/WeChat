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
            string path = HttpContext.Current.Server.MapPath("~/wechat_parameters.json");
            string str = ReadFromFile(path);
            using (StringReader reader = new StringReader(str))
            {
                JsonSerializer serializer = new JsonSerializer();
                object obj = serializer.Deserialize(new JsonTextReader(reader), typeof(WeChatParameters));
                WeChatParameters wechatparameters = obj as WeChatParameters;
                return wechatparameters;
            }
        }
        private string ReadFromFile(string absolutePath)
        {
            using (StreamReader reader = new StreamReader(absolutePath))
            {
                string str = reader.ReadToEnd();
                return str;
            }
        }
        //public T GetModel<T>(T model)
        //{
        //    string path;
        //    if (model is WeChatParameters)
        //    {
        //        path = HttpContext.Current.Server.MapPath("~/wechat_parameters.json");
        //    }
        //    if(model is AccessToken)
        //    {
        //        path = HttpContext.Current.Server.MapPath("~/wechat_parameters.json");
        //    }
        //    if (model is JSApiTicket)
        //    {
        //        path = HttpContext.Current.Server.MapPath("~/wechat_parameters.json");
        //    }

        //    JsonSerializer serializer = new JsonSerializer();
        //    using (StringReader stringReader = new StringReader(str))
        //    {
        //        object obj = serializer.Deserialize(new JsonTextReader(stringReader), typeof(WeChatParameters));
        //        WeChatParameters wechatparameters = obj as WeChatParameters;
        //        return wechatparameters;
        //    }






        //    if (model is T)
        //    {
        //        result = (T)(object)model; //或 (T)((object)model);
        //    }
        //    return result;
        //}
    }
}
