namespace Common.AttributeHelper.Validates.Model
{
    using System.ComponentModel;

    public class ValidateResult
    {
        public ValidateResult()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="isSuccess">是否校验通过</param>
        /// <param name="errMsg">检验不通过提示信息</param>
        public ValidateResult(bool isSuccess, string errMsg)
        {
            IsSuccess = isSuccess;
            ErrMsg = errMsg ?? "";
        }

        /// <summary>
        /// 是否校验通过
        /// </summary>
        [DefaultValue(true)]
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 检验不通过提示信息
        /// </summary>
        public string ErrMsg { get; set; }
    }
}
