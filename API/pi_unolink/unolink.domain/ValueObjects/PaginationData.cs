using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unolink.domain.ValueObjects
{
    public class PaginationData
    {
        public int Page { get; }
        public int PageSize { get;}

        public PaginationData(int page, int pageSize)
        {
            if (page < 1) throw new ArgumentException("Page must be at least 1");
            if (pageSize < 1) throw new ArgumentException("Page Size must be at least 1");


            Page = page;
            PageSize = pageSize;
        }
        public int Offset => (Page - 1) * PageSize;
    }
}
