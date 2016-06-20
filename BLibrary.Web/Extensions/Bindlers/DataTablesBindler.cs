using BLibrary.Entity.Filters.Paging;
using BLibrary.Entity.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BLibrary.Web.Extensions.Bindlers
{
    public class DataTablesBindler : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            DataTablesFilter model = base.BindModel(controllerContext, bindingContext) as DataTablesFilter;
            if (model != null)
            {
                model.Echo = GetValue(controllerContext, "sEcho");
                model.Search = GetValue(controllerContext, "sSearch");
                int size = 0;
                if (int.TryParse(GetValue(controllerContext, "iDisplayLength"), out size))
                {
                    model.PageSize = size;
                }
                int index = 0;
                if (int.TryParse(GetValue(controllerContext, "iDisplayStart"), out index))
                {
                    model.PageIndex = index / size + 1;
                }
                int columns = 0;
                model.Fields = new List<string>();
                if (int.TryParse(GetValue(controllerContext, "iColumns"), out columns))
                {
                    for (int i = 0; i < columns; ++i)
                    {
                        model.Fields.Add(GetValue(controllerContext, "mDataProp_" + i));
                    }
                }
                int sortColumn = 0;
                if (int.TryParse(GetValue(controllerContext, "iSortCol_0"), out sortColumn))
                {
                    model.SortFieldIndex = sortColumn;
                }
                model.SortDirection = string.Compare("asc", GetValue(controllerContext, "sSortDir_0")) == 0 ? SortDirection.Ascending : SortDirection.Descending;
            }
            return model;
        }

        private string GetValue(ControllerContext controllerContext, string key)
        {
            return controllerContext.HttpContext.Request.QueryString[key];
        }
    }

    public class DataTablesBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            if (modelType.IsSubclassOf(typeof(DataTablesFilter)))
            {
                return new DataTablesBindler();
            }
            return null;
        }
    }
}