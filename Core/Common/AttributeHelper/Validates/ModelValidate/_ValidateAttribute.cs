using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AttributeHelper.Validates.ModelValidate
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true)]
    public abstract class _ValidateAttribute : Attribute
    {
        /// <summary>
        /// 校验不通过提示信息
        /// </summary>
        protected string errMsg { get; set; }

        /// <summary>
        /// 校验数据是否合法
        /// </summary>
        /// <param name="value">待校验的值</param>
        /// <returns></returns>
        public abstract bool Validate(object value);

        /// <summary>
        /// 获取检验不通过提示信息
        /// </summary>
        /// <param name="name">字段名称</param>
        /// <returns></returns>
        public string GeterrMsg(string name = "")
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "该字段";
            }

            return string.Format(this.errMsg, name);
        }
    }
}
