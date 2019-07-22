using System;
using System.Collections.Generic;
using System.Text;

namespace cms_webservice.IDAL
{
    public interface IPageMoudle
    {
        System.Data.DataTable CurrentData { get; }
        int CurrentPage { get; set; }
        int PageCount { get; }
        int TotalCount { get; set; }
        int PageSize { get; set; }
    }
}
