using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace rating_review_example.Utils
{
    public class Util
    {
        public JWT JWT;
        public HashPass HashPass;
        public static Util Instance;

        public Util ()
        {
            JWT = new JWT();
            HashPass = new HashPass();
        }

        public static Util getInstance()
        {
            if(Instance == null)
            {
                Instance = new Util();
            }
            return Instance;
        }


        public int GetPassCode(HttpRequestMessage httpRequestMessage)
        {
            try
            {
                object result;

                if (httpRequestMessage.Properties.TryGetValue("PassCodeId", out result))
                {
                    return (int)result;
                }
                throw new Exception("Phiên đăng nhập không hợp lệ.");
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}