namespace PublicApi.CorrelationMessage
{
    /// <summary>
    ///  API消息基类，包含一个CorrelationId属性，用于跟踪消息的生命周期和关联不同的消息。
    /// </summary>
    public abstract class BaseMessage
    {
        protected Guid _correlationId=Guid.NewGuid();
        public Guid CorrelationId()=>_correlationId;
    }
}
