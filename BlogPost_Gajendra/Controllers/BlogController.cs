using BlogPost_Gajendra.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BlogPost_Gajendra.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogDbContext _dbContext;
        public BlogController(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAllBlog()
        {
            var blogs = _dbContext.BlogPosts.ToList();
            if (blogs != null)
            {
                return Json(blogs);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult GetBlogById(int Id)
        {
            var blog = _dbContext.BlogPosts.Find(Id);
            if (blog != null)
            {
                return Json(blog);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(BlogPost model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.BlogPosts.Add(model);
                _dbContext.SaveChanges();
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EditPost(int Id)
        {
            var blog = _dbContext.BlogPosts.Find(Id);
            if(blog != null)
            {
                return View(blog);
            }
            return NotFound();
        }

         [HttpPost]
        public IActionResult EditPost(BlogPost model)
        {
            if (ModelState.IsValid)
            {
                _dbContext.BlogPosts.Add(model);
                _dbContext.SaveChanges();
            }

            return View(model);
        }
        [HttpDelete]
        public IActionResult DeletePost(int Id)
        {
            var blog = _dbContext.BlogPosts.Find(Id);
            if( blog != null )
            {
                _dbContext.BlogPosts.Remove(blog);
                _dbContext.SaveChanges();

            }
            return RedirectToAction();
        }

    }
}
