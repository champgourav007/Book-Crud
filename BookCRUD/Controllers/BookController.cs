using BookAPI.Data;
using BookAPI.Models;
using BookCRUD.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace BookCRUD.Controllers
{
    public class BookController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        HttpClient client = new HttpClient();

        // GET: Book
        public IActionResult Index()
        {
            List<BookAPIDb> books = new List<BookAPIDb>();
            client.BaseAddress = new Uri("https://localhost:7152/api/BookAPI");
            var response = client.GetAsync("BookAPI");
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<BookAPIDb>>();
                display.Wait();
                books = display.Result;
            }

            return View(books);
        }

        //GET: Book/Details/5
        public IActionResult Details(int id)
        {
            //BookAPIDb book = new BookAPIDb();
            client.BaseAddress = new Uri(String.Format("https://localhost:7152/api/BookAPI/{0}", id));
            var response = client.GetAsync("BookAPI");
            response.Wait();
            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<BookAPIDb>();
                display.Wait();
                var book = display.Result;
                return View(book);
            }
            else
            {
                return NotFound();
            }
            
        }

        // GET: Book/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookAPIDb obj, IFormFile? file)
        {
            if (file != null)
            {
                obj.Image = SaveImagePath(file);
            }


            client.BaseAddress = new Uri("https://localhost:7152/api/BookAPI");
            var response = client.PostAsJsonAsync<BookAPIDb>("BookAPI", obj);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                TempData["success"] = "Data is Successfully Added";
                return RedirectToAction("Index");
                
            }

            TempData["error"] = "Some Error Occured Please Re-Enter the Data";
            return View();
        }

        private string SaveImagePath(IFormFile file)
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


        //// GET: Book/Edit/5
        public IActionResult Edit(int id)
        {
            //var book = _db.Books.Find(id);
            //if (book == null)
            //{
            //    TempData["error"] = "Data Not Found!";
            //    return View("Index");
            //}
            //return View(book);
        }

        // POST: Book/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit(BookViewModel obj, IFormFile? file)
        //{
        //    var path = "0";
        //    if (file != null)
        //    {
        //        path = SaveImagePath(obj, file);
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        _db.Books.Update(obj);
        //        var book = _db.Books.Find(obj.Id);
        //        book.Image = path;
        //        _db.SaveChanges();
        //        TempData["success"] = "Data is Edited Successfully!";
        //        return RedirectToAction("Details",obj);
        //    }
        //    return View();
        //}

        // GET: Book/Delete/5
        //public IActionResult Delete(int id)
        //{
        //    if(id == null || id == 0)
        //    {
        //        TempData["error"] = "Data not Found!";
        //        return View("Index");
        //    }
        //    var book = _db.Books.Find(id);

        //    return View(book);
        //}

        // POST: Book/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Delete(BookViewModel obj)
        //{
        //    try
        //    {
        //        _db.Books.Remove(obj);
        //        _db.SaveChanges();
        //        TempData["success"] = "Data is deleted successfully!";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
