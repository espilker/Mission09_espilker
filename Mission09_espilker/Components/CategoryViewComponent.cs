using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mission09_espilker.Models;

namespace Mission09_espilker.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        private IBookstoreRepository repo { get; set; }

        public CategoryViewComponent (IBookstoreRepository temp)
        {
            repo = temp;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedType = RouteData?.Values["bookCateogry"];
            var categories = repo.Books
                    .Select(x => x.Category)
                    .Distinct()
                    .OrderBy(x => x);

            return View(categories);
        }
    }
}
