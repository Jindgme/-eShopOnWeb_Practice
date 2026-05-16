namespace Web.ViewModels
{
    // 分页信息的视图模型
    public class PaginationInfoViewModel
    {
        public int TotalItems { get; set; } // 总记录数
        public int ItemsPerPage { get; set; } // 每页显示的记录数
        public int ActualPage { get; set; }  // 当前页码
        public int TotalPages { get; set; }  // 总页数
        public string? Previous { get; set; }  // 上一页的链接，若无上一页则为null
        public string? Next { get; set; }    // 下一页的链接，若无下一页则为null
    }
}
