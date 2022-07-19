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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }


        // GET: Book
        public IActionResult Index()
        {
            IEnumerable<BookViewModel> Books = _db.Books;
            return View(Books);
        }

        // GET: Book/Details/5
        public IActionResult Details(int id)
        {
            var book = _db.Books.Find(id);
            if(book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Book/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookViewModel obj, IFormFile? file)
        {
            if(file != null)
            {
                obj.Image = SaveImagePath(obj, file);
            }
            if(ModelState.IsValid)
            {
                _db.Books.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Book is Added Successfully!";
                return RedirectToAction("Index");
            }
            return View();
        }

        private string SaveImagePath(BookViewModel obj, IFormFile file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Guid.NewGuid().ToString();
            var uploads = Path.Combine(wwwRootPath, @"Images");
            var extension = Path.GetExtension(file.FileName);
            using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return @"\Images\" + fileName + extension;
        }
        

        // GET: Book/Edit/5
        public IActionResult Edit(int id)
        {
            var book = _db.Books.Find(id);
            if(book == null)
            {
                TempData["error"] = "Data Not Found!";
                return View("Index");
            }
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BookViewModel obj, IFormFile? file)
        {
            var path = "0";
            if (file != null)
            {
                path = SaveImagePath(obj, file);
            }

            if (ModelState.IsValid)
            {
                _db.Books.Update(obj);
                var book = _db.Books.Find(obj.Id);
                book.Image = path;
                _db.SaveChanges();
                TempData["success"] = "Data is Edited Successfully!";
                return RedirectToAction("Details",obj);
            }
            return View();
        }

        // GET: Book/Delete/5
        public IActionResult Delete(int id)
        {
            if(id == null || id == 0)
            {
                TempData["error"] = "Data not Found!";
                return View("Index");
            }
            var book = _db.Books.Find(id);
            
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(BookViewModel obj)
        {
            try
            {
                _db.Books.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Data is deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
