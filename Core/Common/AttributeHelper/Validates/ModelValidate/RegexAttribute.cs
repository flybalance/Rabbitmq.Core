using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.AttributeHelper.Validates.ModelValidate
{
    public class RegexAttribute : _ValidateAttribute
    {
        private string _regex = null;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="regex">正则</param>
        public RegexAttribute(string regex)
            : this(regex, "{0}格式错误")
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="regex">正则</param>
        /// <param name="errMsg">校验失败提示信息</param>
        public RegexAttribute(string regex, string errMsg)
        {
            _regex = regex;
            this.errMsg = errMsg;
        }

        /// <summary>
        /// 校验数据是否合法
        /// </summary>
        /// <param name="value">待校验的值</param>
        /// <returns></returns>
        public override bool Validate(object value)
        {
            if (value == null)
            {
                return false;
            }
            return new Regex(_regex).IsMatch(value.ToString());
        }
    }
}
