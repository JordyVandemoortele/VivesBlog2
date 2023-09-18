using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace VivesBlog.Controllers
{
	public class HomeController : Controller
	{
        private readonly VivesBlogDbContext _context;
        public HomeController(VivesBlogDbContext database)
        {
            _context = database;
        }
        public IActionResult Index()
		{
			var articles = _context.Articles
				.Include(a => a.Author)
				.ToList();
			return View(articles);
		}

		public IActionResult Details(int id)
		{
			var article = _context.Articles
				.Include(a => a.Author)
				.SingleOrDefault(a => a.Id == id);

			return View(article);
		}
	}
}
