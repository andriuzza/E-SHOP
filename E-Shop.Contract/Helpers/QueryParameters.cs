using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Contract.Helpers
{
    public class QueryParameters
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 8;

        public string SearchField { get; set; } = string.Empty;

        public static QueryParameters Create(int page, int size, string searchField)
        {
            return new QueryParameters
            {
                PageSize = size,
                Page = page,
                SearchField = searchField
            };
        }

        public int TotalCount { get; set; }
    }
}
