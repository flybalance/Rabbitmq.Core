using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AttributeHelper.Validates.ModelValidate
{
    public class RangeAttribute : _ValidateAttribute
    {
        private int _min = -1;
        private int _max = -1;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public RangeAttribute(int min, int max)
            : this(min, max, string.Format("{0}应在{1}到{2}之间", "{0}", min, max))
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="errMsg">校验失败提示信息</param>
        public RangeAttribute(int min, int max, string errMsg)
        {
            _min = min;
            _max = max;
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

            decimal v;
            if (!decimal.TryParse(value.ToString(), out v))
            {
                return false;
            }

            return v >= _min && v <= _max;
        }
    }
}
