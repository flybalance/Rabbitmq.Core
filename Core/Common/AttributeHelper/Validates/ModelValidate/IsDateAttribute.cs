using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AttributeHelper.Validates.ModelValidate
{
    public class IsDateAttribute: _ValidateAttribute
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public IsDateAttribute() : this(string.Format("{0}是必填的", "{0}"))
        { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="errMsg"></param>
        public IsDateAttribute(string errMsg)
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
            DateTime v;
            bool ret = DateTime.TryParse(value.ToString(), out v);
            if (!ret)
                return false;

            if (v.ToString("yyyy-MM-dd HH:mm:ss").Equals("0001-01-01 00:00:00"))
                return false;
            return ret;
        }
    }
}
