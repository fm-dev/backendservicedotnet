namespace BackendServices.Helper.Result
{
    public class PaginatedResult<T> : Result<IReadOnlyList<T>>
    {
        public PaginationMeta Pagination { get; set; } = new PaginationMeta();

        public class PaginationMeta
        {
            public int Page { get; set; }
            public int Size { get; set; }
            public long Total { get; set; }
            public int TotalPage { get; set; }
        }
    }
    public class ResultBasePaginated<T> : PaginatedResult<T> { }
}
