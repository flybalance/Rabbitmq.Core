using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AttributeHelper.Validates.ModelValidate
{
    public class StrMinLengthAttribute : _ValidateAttribute
    {
        private int _min = -1;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="min"></param>
        public StrMinLengthAttribute(int min)
            : this(min, string.Format("{0}的长度应小于等于{1}", "{0}", min.ToString())) { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="min"></param>
        /// <param name="errMsg"></param>
        public StrMinLengthAttribute(int min, string errMsg)
        {
            _min = min;
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
            return value.ToString().Length >= _min;
        }
    }
}
