using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OrderingSystem.Core.Utils
{
    public static class CheckInputHelper
    {
        public const string EmailPattern = @"^(?![\.@])(""([^""\r\\]|\\[""\r\\])*""|([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
           + @"@([a-z0-9][\w-]*\.)+[a-z]{2,}$";


        /// <summary>
        /// 检查输入是否是格式正确的邮箱
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsEmail(string text)
        {

            return RegexCheck(EmailPattern, text);
        }

        private static bool RegexCheck(string strReg, string input)
        {
            var reg = new Regex(strReg);

            return reg.IsMatch(input);

        }

        /// <summary>
        /// 验证输入手机号是否正确
        /// </summary>
        /// <param name="recNum"></param>
        /// <returns></returns>
        public static bool RegexPhone(string recNum)
        {
            string RegexStr = @"^1[34578]\d{9}$";
            if (Regex.IsMatch(recNum, RegexStr))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool RegexCardNo(string cardNo)
        {
            string Card18 = @"^[1-9]\d{5}(18|19|([23]\d))\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\d{3}[0-9Xx]$";
            string Card15 = @"^[1-9]\d{5}\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\d{2}[0-9Xx]$";
            if (Regex.IsMatch(cardNo, Card18)||Regex.IsMatch(cardNo, Card15))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
