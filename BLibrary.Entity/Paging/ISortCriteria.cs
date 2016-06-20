using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLibrary.Entity.Paging
{
    public interface ISortCriteria<T>
    {
        string Direction { get; set; }
        IOrderedQueryable<T> ApplyOrdering(IQueryable<T> query, bool useThenBy);
    }
}
