using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AttributeHelper.Validates.ModelValidate
{
    public class MaxAttribute: _ValidateAttribute
    {
        private int _max = -1;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="max"></param>
        public MaxAttribute(int max)
            : this(max, string.Format("{0}应小于等于{1}", "{0}", max))
        { }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="max">最大值</param>
        /// <param name="errMsg"></param>
        public MaxAttribute(int max, string errMsg)
        {
            this._max = max;
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
            int v;
            if (!int.TryParse(value.ToString(), out v))
            {
                return false;
            }
            return v <= _max;
        }
    }
}
