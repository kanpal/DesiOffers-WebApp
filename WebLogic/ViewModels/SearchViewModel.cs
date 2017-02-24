using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLogic.ViewModels
{
    public class SearchViewModel
    {
        public string SearchText { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }

        public int NumberOfItemsPerRow = 3;
    }
}