using BlazorShared.Models;

namespace BlazorShared.Interfaces
{
    // 这个接口定义了一个通用的目录查找数据服务，适用于任何继承自 LookupData 的类型。
    public interface ICatalogLookupDataService<TLookupData> where TLookupData : LookupData
    {
        Task<List<TLookupData>> List();
    }
}
