using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLibrary.Entity.Filters.Paging
{
    public class FormFilter : BasePagingFilter
    {
        public string InstanceCode { get; set; }

        public string ApplyEmpCode { get; set; }

        public DateTime? ApplyDateFrom { get; set; }

        public DateTime? ApplyDateTo { get; set; }
    }
}
