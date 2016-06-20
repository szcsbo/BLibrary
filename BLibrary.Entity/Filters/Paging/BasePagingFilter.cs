using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLibrary.Entity.Filters
{
    public class BasePagingFilter : BaseFilter
    {
        //public string sEcho { get; set; }

        //public string sSearch { get; set; }

        //public int iDisplayLength { get; set; }

        //public int iDisplayStart { get; set; }

        public int Skip
        {
            get;
            set;
        }

        public int PageIndex
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }

        //public List<string> Fields
        //{
        //    get;
        //    set;
        //}

        //public int SortFieldIndex
        //{
        //    get;
        //    set;
        //}

        public string SortDirection
        {
            get;
            set;
        }

        //public string SortColumn
        //{
        //    get
        //    {
        //        if (Fields != null && Fields.Count > 0)
        //        {
        //            return Fields[SortFieldIndex];
        //        }
        //        return string.Empty;
        //    }
        //}
    }
}
