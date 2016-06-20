using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLibrary.Entity.Filters.Paging
{
    public class DataTablesFilter : BasePagingFilter
    {
        public string sEcho { get; set; }

        public string sSearch { get; set; }

        public int iDisplayLength { get; set; }

        public int iDisplayStart { get; set; }

        public string Echo
        {
            get;
            set;
        }

        public string Search
        {
            get;
            set;
        }

        public List<string> Fields
        {
            get;
            set;
        }

        public int SortFieldIndex
        {
            get;
            set;
        }

        public string SortColumn
        {
            get
            {
                if (Fields != null && Fields.Count > 0)
                {
                    return Fields[SortFieldIndex];
                }
                return string.Empty;
            }
        }
    }
}
