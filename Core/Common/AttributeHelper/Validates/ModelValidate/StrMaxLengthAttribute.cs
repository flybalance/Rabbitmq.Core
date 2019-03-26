using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AttributeHelper.Validates.ModelValidate
{
    public class StrMaxLengthAttribute : _ValidateAttribute
    {
        private int _maxLength = -1;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxLength"></param>
        public StrMaxLengthAttribute(int maxLength)
            : this(maxLength, string.Format("{0}的长度应大于等于{1}", "{0}", maxLength))
        { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="maxLength"></param>
        /// <param name="errMsg"></param>
        public StrMaxLengthAttribute(int maxLength, string errMsg)
        {
            _maxLength = maxLength;
            this.errMsg = errMsg;
        }
        /// <summary>
        /// 校验数据是否合法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool Validate(object value)
        {
            if (value == null)
            {
                return false;
            }
            return value.ToString().Length <= _maxLength;
        }
    }
}
