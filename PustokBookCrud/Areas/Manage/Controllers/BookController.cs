using Microsoft.AspNetCore.Mvc;
using PustokBookCrud.Models;
using PustokBookCrud.DAL;
using Microsoft.EntityFrameworkCore;

namespace PustokBookCrud.Areas.Manage.Controllers
{
    [Area("manage")]
    public class BookController : Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Book> books = _context.Books.ToList();

            return View(books);
        }
        public IActionResult Create()
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book book)
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            if(!ModelState.IsValid)
            {
                return NotFound();
            }
            if(!_context.Authors.Any(a=>a.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author Not found");
                return View();
            }
            if(!_context.Genres.Any(g=>g.Id == book.GenreId))
            {
                ModelState.AddModelError("GenreId", "genre Not Found");
                return View();
            }

            bool check = false;
            if (book.TagIds != null)
            {
                foreach (var tagId in book.TagIds)
                {
                    if (!_context.Tags.Any(x => x.Id == tagId))
                    {
                        check = true;
                        break;
                    }
                }
            }
            if (check)
            {
                ModelState.AddModelError("TagId", "Tag not found");
                return View();
            }
            else
            {
                if (book.TagIds != null)
                {
                    foreach (var tagId in book.TagIds)
                    {
                        BookTag booktag = new BookTag
                        {
                            Book = book,
                            TagId = tagId,

                        };
                        _context.BookTags.Add(booktag);
                    }
                }
            }


            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            Book existbook=_context.Books.Include(x=>x.BookTags).FirstOrDefault(x => x.Id == id);

            if (existbook == null)
            {
                return View();
            }
            existbook.TagIds = existbook.BookTags.Select(bt=>bt.TagId).ToList();
            return View(existbook);
        }
        [HttpPost]
        public IActionResult Update(Book book)
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            if (!_context.Authors.Any(a => a.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author Not found");
                return View();
            }
            if (!_context.Genres.Any(g => g.Id == book.GenreId))
            {
                ModelState.AddModelError("GenreId", "genre Not Found");
                return View();
            }

            var existBook = _context.Books.Include(x=>x.BookTags).FirstOrDefault(x=>x.Id == book.Id);
            if (existBook == null)
            {
                return NotFound();
            }
            // eli ve nino => 1 
            // formdan gelen => 1, 2, 4, 5

            existBook.BookTags.RemoveAll(bt=> !book.TagIds.Contains(bt.TagId));

            foreach (var tagId in book.TagIds.Where(t=> !existBook.BookTags.Any(bt=>bt.TagId == t)))
            {
                BookTag bookTag = new BookTag
                {
                    TagId = tagId
                };
                existBook.BookTags.Add(bookTag);
            }

            existBook.Name = book.Name;
            existBook.Description = book.Description;
            existBook.CostPrice = book.CostPrice;
            existBook.SalePrice = book.SalePrice;
            existBook.DiscountPercent = book.DiscountPercent;
            existBook.Code = book.Code;
            existBook.IsAvailable = book.IsAvailable;
            existBook.AuthorId = book.AuthorId;
            existBook.GenreId = book.GenreId;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {

            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            if (id == null) return NotFound();

            Book book = _context.Books.FirstOrDefault(b => b.Id == id);

            if (book == null) return NotFound();


            return View(book);
        }

        [HttpPost]
        public IActionResult Delete(Book book)
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();

            Book wantedBook = _context.Books.FirstOrDefault(b => b.Id == book.Id);

            if (wantedBook == null)
            {
                return NotFound();
            }

            _context.Books.Remove(wantedBook);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
