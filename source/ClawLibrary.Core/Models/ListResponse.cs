using System.Collections.Generic;
using System.Linq;

namespace ClawLibrary.Core.Models
{
    public class ListResponse<T>
    {
        public long TotalCount { get; set; }
        public T[] Items { get; set; }

        public ListResponse()
        {
            
        }

        public ListResponse(IEnumerable<T> list)
        {
            var enumerable = list as T[] ?? list.ToArray();
            Items = enumerable.ToArray();
            TotalCount = enumerable.Count();
        }

        public ListResponse(IEnumerable<T> list, long totalCount)
        {
            var enumerable = list as T[] ?? list.ToArray();
            Items = enumerable.ToArray();
            TotalCount = totalCount;
        }

        public ListResponse(T[] array, long totalCount)
        {
            Items = array;
            TotalCount = totalCount;
        }
    }
}
