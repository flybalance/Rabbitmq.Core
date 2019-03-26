using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AttributeHelper.Validates.ModelValidate
{
    public class StrLengthAttribute : _ValidateAttribute
    {
        private int _max = -1;
        private int _min = -1;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public StrLengthAttribute(int min, int max)
            : this(min, max, string.Format("{0}的长度应在{1}和{2}之间", "{0}", min.ToString(), max.ToString())) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="errMsg"></param>
        public StrLengthAttribute(int min, int max, string errMsg)
        {
            _max = max;
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
            string v = value.ToString();

            return v.Length <= _max && v.Length >= _min;
        }
    }
}
