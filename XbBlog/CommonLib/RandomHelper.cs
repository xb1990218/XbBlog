using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    /// <summary>
    /// 随机数帮助类
    /// </summary>
    public class RandomHelper
    {
        /// <summary>
        /// 获取指定长度的随机密码-由数字大小写字母组成
        /// </summary>
        /// <param name="PasswordLen"></param>
        /// <returns></returns>
        public static string GetRndPassword(int PasswordLen)
        {
            //创建一个StringBuilder对象存储密码
            StringBuilder sb = new StringBuilder();
            //使用for循环把单个字符填充进StringBuilder对象里面变成PasswordLen位密码字符串
            for (int i = 0; i < PasswordLen; i++)
            {
                Random random = new Random();
                //随机选择里面其中的一种字符生成
                switch (random.Next(3))
                {
                    case 0:
                        //调用生成生成随机数字的方法
                        sb.Append(createNum());
                        break;
                    case 1:
                        //调用生成生成随机小写字母的方法
                        sb.Append(createSmallAbc());
                        break;
                    case 2:
                        //调用生成生成随机大写字母的方法
                        sb.Append(createBigAbc());
                        break;
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取指定长度的随机数字码-纯数字
        /// </summary>
        /// <param name="PasswordLen"></param>
        /// <returns></returns>
        public static string GetRndNum(int PasswordLen)
        {
            //创建一个StringBuilder对象存储密码
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < PasswordLen; i++)
            {
                Random random = new Random();
                //调用生成生成随机数字的方法
                sb.Append(createNum());
            }
            return sb.ToString();
        }

        // <summary>
        /// 生成单个随机数字
        /// </summary>
        public static int createNum()
        {
            Random random = new Random();
            int num = random.Next(10);
            return num;
        }

        /// <summary>
        /// 生成单个大写随机字母
        /// </summary>
        public static string createBigAbc()
        {
            //A-Z的 ASCII值为65-90
            Random random = new Random();
            int num = random.Next(65, 91);
            string abc = Convert.ToChar(num).ToString();
            return abc;
        }

        /// <summary>
        /// 生成单个小写随机字母
        /// </summary>
        public static string createSmallAbc()
        {
            //a-z的 ASCII值为97-122
            Random random = new Random();
            int num = random.Next(97, 123);
            string abc = Convert.ToChar(num).ToString();
            return abc;
        }
    }
}
