using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApplication_Blogs.Models;


namespace WebApplication_Blogs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly BlogDBContext _context = null;
        private static IWebHostEnvironment _environment;
        public BlogsController(BlogDBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [HttpGet]
        [Authorize(Roles = "admin,user")]
        public IEnumerable<Blog> Get()
        {
            return _context.Blogs.OrderByDescending(b => b.Id).ToList();
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="admin,user")]
        public IActionResult Get(int id)
        {
            var blog = _context.Blogs.Find(id);
            if (blog == null)
                return BadRequest("Blog not found!");
            return Ok(blog);
        }

        [HttpPost]
        [Authorize(Roles ="admin,user")]
        public IActionResult Post([FromForm]BlogBody blog)
        {
            string imageName = null;
            if (blog.Image != null)
                imageName = ImageHandler.AddImage(_environment.WebRootPath,blog.Image);
            Blog newBlog = new Blog()
            {
                Title = blog.Title,
                Content = blog.Content,
                Image= imageName
            };
            _context.Blogs.Add(newBlog);
            _context.SaveChanges();
            return Ok(new {blog = newBlog });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Put(int id, [FromForm] BlogBody updated_blog)
        {
            var blog = _context.Blogs.Find(id);
            if (blog == null)
            {
                return BadRequest("Blog not found to be updated!");
            }
            string newImage = null;
            if (updated_blog.Image != null)
            {
                newImage = ImageHandler.UpdateIamge(_environment.WebRootPath, blog.Image, updated_blog.Image);
            }
            blog.Title = updated_blog.Title;
            blog.Content = updated_blog.Content;
            if (newImage == null)
                blog.Image = blog.Image;
            else
                blog.Image = newImage;
            _context.SaveChanges();
            return Ok(blog);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            var blog = _context.Blogs.Find(id);
            if (blog == null)
            {
                return BadRequest("Blog not found to be deleted!");
            }

            ImageHandler.DeleteIamge(_environment.WebRootPath,blog.Image);
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
            return Ok("blog \""+blog.Title+"\" deleted successfully");
        }
    }
}
