using Core;
using Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Linq;

namespace VivesBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly VivesBlogDbContext _context;
        private readonly ArticleHelper _articleHelper;
        public BlogController(VivesBlogDbContext database)
        {
            _context = database;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var articles = _context.Articles
                .Include(a => a.Author)
                .ToList();
            return View(articles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var articleModel = CreateArticleModel();

            return View(articleModel);
        }

        [HttpPost]
        public IActionResult Create(Article article)
        {
            if (!ModelState.IsValid)
            {
                var articleModel = CreateArticleModel(article);
                return View(articleModel);
            }

            article.CreatedDate = DateTime.Now;

            _context.Articles.Add(article);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var article = _context.Articles.SingleOrDefault(p => p.Id == id);

            var articleModel = CreateArticleModel(article);

            return View(articleModel);
        }
        [HttpPost]
        public IActionResult Edit(Article article)
        {
            if (!ModelState.IsValid)
            {
                var articleModel = CreateArticleModel(article);
                return View(articleModel);
            }

            var dbArticle = _context.Articles.SingleOrDefault(p => p.Id == article.Id);

            dbArticle.Title = article.Title;
            dbArticle.Description = article.Description;
            dbArticle.Content = article.Content;
            dbArticle.AuthorId = article.AuthorId;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var article = _context.Articles
                .Include(a => a.Author)
                .SingleOrDefault(p => p.Id == id);

            return View(article);
        }
        [HttpPost("Blog/Delete/{id:int}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var dbArticle = _context.Articles.SingleOrDefault(p => p.Id == id);

            _context.Articles.Remove(dbArticle);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        private ArticleModel CreateArticleModel(Article article = null)
        {
            article ??= new Article();

            var authors = _context.People
                .OrderBy(a => a.FirstName)
                .ThenBy(a => a.LastName)
                .ToList();

            var articleModel = new ArticleModel
            {
                Article = article,
                Authors = authors
            };

            return articleModel;
        }
    }
}
