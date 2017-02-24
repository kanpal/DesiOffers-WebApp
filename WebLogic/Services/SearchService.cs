using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLogic.Interfaces;
using WebLogic.ViewModels;

namespace WebLogic.Services
{
    public class SearchService : IService
    {
        public SearchService()
        {
        }

        public string GetName()
        {
            return "SearchService";
        }    

        public SearchViewModel GetSearchResults(string searchText)
        {
            SearchViewModel viewModel = new SearchViewModel();
            viewModel.SearchText = searchText;
            viewModel.Products = GeneralService.GetProductService().ListMatching(searchText);
            return viewModel;
        }

        public SearchViewModel GetRecentList()
        {
            SearchViewModel viewModel = new SearchViewModel();
            viewModel.SearchText = string.Empty;
            viewModel.Products = GeneralService.GetProductService().ListRecent();
            return viewModel;
        }

    }
}
