using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mission09_espilker.Models;
using Mission09_espilker.Models.View_Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission09_espilker.Controllers
{
    public class HomeController : Controller
    {
        private IBookstoreRepository repo;

        public HomeController (IBookstoreRepository temp)
        {
            repo = temp;
        }
        public IActionResult Index(string bookCategory, int pageNum = 1)
        {

            int pageSize = 10;

            var x = new BooksViewModel
            {
                Books = repo.Books
                    .Where(b => b.Category == bookCategory || bookCategory == null)
                    .OrderBy(b => b.Title)
                    .Skip((pageNum - 1) * pageSize) // .Take(5) Only shows five results .Skip(5) would skip 5 entries
                    .Take(pageSize),

                PageInfo = new PageInfo
                {
                    TotalNumBooks = (bookCategory == null 
                        ?repo.Books.Count()
                        : repo.Books.Where(x => x.Category == bookCategory).Count()),
                    BooksPerPage = pageSize,
                    CurrentPage = pageNum
                }
            };

            return View(x);
        }

    }
}
