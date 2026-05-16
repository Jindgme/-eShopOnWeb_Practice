namespace PublicApi.CorrelationMessage
{
    /// <summary>
    ///  Api响应消息基类，继承自BaseMessage，表示一个API响应消息。它可以包含与请求相关的任何数据或状态信息，以便在处理响应时使用。
    /// </summary>
    public abstract class BaseResponse:BaseMessage
    {
        public BaseResponse(Guid correlationId):base()
        {
            _correlationId = correlationId;
        }
        public BaseResponse()
        {
            
        }
    }
}
