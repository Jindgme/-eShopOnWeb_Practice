using System.Text.Json.Serialization;

namespace ApplicationCore.Extensions
{
    // 配置返回结果类型
    public class Result<T>
    {
        protected Result() { }
        public Result(T value)
        {
            Value = value;
        }
        /// <summary>
        /// 将实体类型<T>隐式转换成 Result<T>,返回的时候可以直接返回 value,而不用返回 new Result<T>(value)。 
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator Result<T>(T value) => new Result<T>(value);
        /// <summary>
        /// 将Result<T>隐式转换成 <T> 类型，调用接口取值的时候直接返回 t ，否则需要返回 t.Value 才能获取到实体类型 T 的值。
        /// *************** 后面会用隐式转换标记位置 ******************
        /// 建议注释掉直接用t.Value,加深印象
        /// </summary>
        /// <param name="result"></param>
        //public static implicit operator T(Result<T> result) => result.Value;


        [JsonInclude]
        public ResultStatus Status { get; protected set; } = ResultStatus.Ok;
        [JsonInclude]
        public IEnumerable<string> Errors { get; protected set; } = [];
        [JsonInclude]
        public T Value { get; init; }
        [JsonInclude]
        public string SuccessMessage { get; protected set; } = string.Empty;
        public static Result<T> NotFound()
        {
            return new Result<T> { Status = ResultStatus.NotFound };
        }
        public static Result<T> NotFound(params string[] errorMessage)
        {
            return new Result<T> { Status = ResultStatus.NotFound, Errors = errorMessage };
        }
        public static Result<T> Success(T value)
        {
            return new Result<T> { Value = value };
        }
        public static Result<T> Success(T value, string successMessage)
        {
            return new Result<T> { Value = value, SuccessMessage = successMessage };
        }
    }
}
