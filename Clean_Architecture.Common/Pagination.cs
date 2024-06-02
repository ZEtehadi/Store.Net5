using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Common
{
    public static class Pagination
    {
        public static IEnumerable<TSource> ToPaged<TSource>(this IEnumerable<TSource> sources, int Page, int PageSize, out int rowsCount)
        {
            rowsCount = sources.Count();
            return sources.Skip((Page - 1) * PageSize).Take(PageSize);
        }
    }
}
