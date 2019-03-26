using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AttributeHelper.Validates.ModelValidate
{
    public class MinAttribute : _ValidateAttribute
    {
        private int _min;

        public MinAttribute(int min)
            : this(min, string.Format("{0}应大于等于{1}", "{0}", min.ToString()))
        { }

        public MinAttribute(int min, string errMsg)
        {
            _min = min;
            this.errMsg = errMsg;
        }

        public override bool Validate(object value)
        {
            if (value == null)
            {
                return false;
            }
            int v;
            if (!int.TryParse(value.ToString(), out v))
            {
                errMsg = "数据不是int类型";
                return false;
            }
            return v >= _min;
        }
    }
}
