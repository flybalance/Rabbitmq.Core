using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AttributeHelper.Validates.ModelValidate
{
    public class RequiredAttribute : _ValidateAttribute
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public RequiredAttribute() : this(string.Format("{0}是必填的", "{0}"))
        { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="errMsg"></param>
        public RequiredAttribute(string errMsg)
        {
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
            return !string.IsNullOrEmpty(value.ToString());
        }
    }
}
