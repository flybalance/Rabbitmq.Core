using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AttributeHelper.GetDescription
{
    [AttributeUsage(AttributeTargets.All)]
    public class DescriptionAttribute: Attribute
    {
        /// <summary>
        /// 返回值
        /// </summary>
        public string Result { get; set; }

        public DescriptionAttribute(string str)
        {
            Result = str;
        }

        public string GetDescription()
        {
            return Result;
        }
    }
}
