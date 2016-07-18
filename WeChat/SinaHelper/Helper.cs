﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SinaHelper
{
   public class Helper
    {
        public SinaParameters GetSinaParameters()
        {
            string path = HttpContext.Current.Server.MapPath("~/sina_parameters.json");
            string str = ReadFromFile(path);
            using (StringReader stringReader = new StringReader(str))
            {
                JsonSerializer serializer = new JsonSerializer();
                object obj = serializer.Deserialize(new JsonTextReader(stringReader), typeof(SinaParameters));
                //QQParameters result = (QQParameters)obj;
                SinaParameters result = obj as SinaParameters;
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
