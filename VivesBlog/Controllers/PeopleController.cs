using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VivesBlog.Models;

namespace VivesBlog.Controllers
{
    public class PeopleController : Controller
    {
        private readonly VivesBlogDbContext _context;
        public PeopleController(VivesBlogDbContext database)
        {
            _context = database;
        }
        public IActionResult Index()
        {
            var people = _context.People.ToList();
            return View(people);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }
            _context.People.Add(person);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var person = _context.People.Single(p => p.Id == id);

            return View(person);
        }

        [HttpPost]
        public IActionResult Edit(Person person)
        {
            if (!ModelState.IsValid)
            {
                return View(person);
            }

            var dbPerson = _context.People.Single(p => p.Id == person.Id);

            dbPerson.FirstName = person.FirstName;
            dbPerson.LastName = person.LastName;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var person = _context.People.Single(p => p.Id == id);

            return View(person);
        }

        [HttpPost("People/Delete/{id:int}")]
        public IActionResult PeopleDeleteConfirmed(int id)
        {
            var dbPerson = _context.People.Single(p => p.Id == id);

            _context.People.Remove(dbPerson);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
