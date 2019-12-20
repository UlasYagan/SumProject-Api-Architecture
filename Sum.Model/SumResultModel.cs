using System.Collections.Generic;

namespace Sum.Model
{
    public class SumResultModel<T>
    {
        public int TotalRecordCount { get; set; }
        public ICollection<T> Data { get; set; }
        public T SingleData { get; set; }
        public bool Success { get; set; }
        public string ReadableMessage { get; set; }
    }
}