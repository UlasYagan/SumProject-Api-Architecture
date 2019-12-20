namespace Sum.Model.Options
{
    public class DataPagingOptions
    {
        public DataPagingOptions(int? pageSize, int? pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}