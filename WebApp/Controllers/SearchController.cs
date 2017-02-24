using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;
using WebLogic.Framework;
using WebLogic.Services;
using WebLogic.ViewModels;

namespace WebApp.Controllers
{
    public class SearchController : BaseController
    {
        // GET: Search
        public ActionResult Index(string searchText)
        {
            SearchViewModel viewModel = GeneralService.GetSearchService().GetSearchResults(searchText);
            viewModel.NumberOfItemsPerRow = Request.Browser.ScreenPixelsWidth >= 800 ? 4 : 3;
            return View(viewModel);
        }

        public ActionResult RecentList()
        {
            SearchViewModel viewModel = GeneralService.GetSearchService().GetRecentList();
            viewModel.NumberOfItemsPerRow = Request.Browser.ScreenPixelsWidth >= 800 ? 4 : 3;
            return PartialView("SearchList", viewModel);
        }
    }
}