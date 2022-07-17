using BookCRUD.Data;
using BookCRUD.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BookCRUD.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }


        // GET: BookController
        public IActionResult Index()
        {
            IEnumerable<BookViewModel> Books = _db.Books;
            return View(Books);
        }

        // GET: BookController/Details/5
        public IActionResult Details(int id)
        {
            var book = _db.Books.Find(id);
            if(book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: BookController/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookViewModel obj)
        {
            if(ModelState.IsValid)
            {
                _db.Books.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        

        // GET: BookController/Edit/5
        public IActionResult Edit(int id)
        {
            var book = _db.Books.Find(id);
            if(book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookViewModel obj)
        {
            if (ModelState.IsValid)
            {
                _db.Books.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Details",obj);
            }
            return View();
        }

        // GET: BookController/Delete/5
        public IActionResult Delete(int id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var book = _db.Books.Find(id);
            
            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(BookViewModel obj)
        {
            try
            {
                _db.Books.Remove(obj);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
