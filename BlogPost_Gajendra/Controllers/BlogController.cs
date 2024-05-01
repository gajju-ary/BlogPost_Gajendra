using BlogPost_Gajendra.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NuGet.Protocol;

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
            var blogs = _dbContext.BlogPosts.ToList();
            if (blogs != null)
            {
                return View(blogs);
            }
            return View();
        }
        //For API Use
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
                return View(blog);
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

            if (model != null)
            {
                model.DateCreated = DateTime.UtcNow; //DateTime.ParseExact(myString, "MM-dd-yyyy",null);
                model.DateModified = DateTime.UtcNow;
                _dbContext.BlogPosts.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EditPost(int Id)
        {
            var blog = _dbContext.BlogPosts.Find(Id);
            if (blog != null)
            {
                return View(blog);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditPost(BlogPost model)
        {
            if (model != null)
            {
                model.DateCreated = DateTime.SpecifyKind(model.DateCreated, DateTimeKind.Utc); 
                model.DateModified = DateTime.UtcNow;
                _dbContext.BlogPosts.Update(model);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
        
        public IActionResult DeletePost(int Id)
        {
            var blog = _dbContext.BlogPosts.Find(Id);
            if (blog != null)
            {
                _dbContext.BlogPosts.Remove(blog);
                _dbContext.SaveChanges();

            }
            return RedirectToAction();
        }

        [HttpGet]
        public IActionResult Search(string text)
        {
            var blog = _dbContext.BlogPosts.Where(e => e.Content.Contains(text) || e.Title.Contains(text)).ToList();
            if (blog.Count <= 0 )
            {
                return NotFound();
               
            }
            return Ok(blog);
        }


    }
}
