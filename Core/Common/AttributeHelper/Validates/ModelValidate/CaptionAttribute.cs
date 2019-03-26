using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AttributeHelper.Validates.ModelValidate
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CaptionAttribute: Attribute
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="name">属性名称</param>
        public CaptionAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name { get; set; }
    }
}
