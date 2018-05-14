using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Shop.Contract.Dto
{
    public class SearchFilter
    {
        public Collection<string> Manufacturers { get; set; }

        public Collection<int> Storages { get; set; }

        public Collection<string> OperatingSystems { get; set; }
    }
}
