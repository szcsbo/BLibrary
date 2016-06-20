using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLibrary.Entity.Paging
{
    public class PagedListResult<T> where T : class
    {
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }

        /// <summary>
        /// Total number of rows that could be possibly be retrieved.
        /// </summary>
        public long Count { get; set; }

        /// <summary>
        /// Result of the query.
        /// </summary>
        public List<T> Records
        {
            get;
            set;
        }

        public int PageSize { get; set; }

        public long TotalOfPages
        {
            get
            {
                var pageSize = PageSize;
                if (pageSize == 0)
                {
                    return 0;
                }
                return (Count / pageSize) + (((Count % pageSize) == 0) ? 0 : 1);
            }
        }

        public IEnumerable<T> Entities { get; set; }
    }
}
