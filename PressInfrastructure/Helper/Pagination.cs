using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PressInfrastructure.Helper
{
    public class Pagination<T> where T: class
    {
        public Pagination(int pageIndex, int pageSize, int totalCount, IReadOnlyList<T> items)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
            //TotalPages = totalPages;
            Items = items;
        }

        public int PageIndex { get; set; }  
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public IReadOnlyList<T> Items { get; set; }


    }
}
