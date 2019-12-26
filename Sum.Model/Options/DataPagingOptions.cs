namespace Sum.Model.Options
{
    public class DataPagingOptions
    {
        public DataPagingOptions(int? pageSize, int? pageNumber)
        {
            PageSize = pageSize ?? 10;
            PageNumber = pageNumber ?? 1;
        }

        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}