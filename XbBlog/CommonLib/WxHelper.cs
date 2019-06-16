using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    /// <summary>
    /// 微信接口处理相关帮助类
    /// </summary>
    public class WxHelper
    {
        /// <summary>
        /// access_token
        /// </summary>
        /// <param name="appid">公众号appid</param>
        /// <param name="secret">公众号秘钥</param>
        /// <returns></returns>
        public static string GetWxToken(string appid, string secret)
        {
            string url = $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={appid}&secret={secret}";
            string jsonStr = HttpHelper.Get(url);
            var jsO = jsonStr.ToJObject();
            if (jsO["access_token"] != null)
            {
                return jsO["access_token"].ToString();
            }
            else
                return null;
        }
    }
}
