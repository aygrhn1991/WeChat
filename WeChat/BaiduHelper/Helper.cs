using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BaiduHelper
{
    public class Helper
    {
        public BaiduParameters GetBaiduParameters()
        {
            string path = HttpContext.Current.Server.MapPath("~/baidu_parameters.json");
            string str = ReadFromFile(path);
            using (StringReader stringReader = new StringReader(str))
            {
                JsonSerializer serializer = new JsonSerializer();
                object obj = serializer.Deserialize(new JsonTextReader(stringReader), typeof(BaiduParameters));
                //QQParameters result = (QQParameters)obj;
                BaiduParameters result = obj as BaiduParameters;
                return result;
            }
        }
        public string ReadFromFile(string absolutePath)
        {
            using (StreamReader reader = new StreamReader(absolutePath))
            {
                string str = reader.ReadToEnd();
                return str;
            }
        }
        public string HttpHelper(string url, RequestMethod requestMethod)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = requestMethod.ToString();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader streamReader = new StreamReader(responseStream))
                {
                    string responseString = streamReader.ReadToEnd();
                    return responseString;
                }
            }
        }
        public T GetObject<T>(T model, string str)
        {
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
