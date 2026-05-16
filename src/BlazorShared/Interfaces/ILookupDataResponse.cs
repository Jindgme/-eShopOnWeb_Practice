using BlazorShared.Models;

namespace BlazorShared.Interfaces
{
    // 这个接口定义了一个通用的目录查找数据响应，适用于任何继承自 LookupData 的类型。
    public interface ILookupDataResponse<TLookupData> where TLookupData : LookupData
    {
        List<TLookupData> List { get; set; }
    }
}
