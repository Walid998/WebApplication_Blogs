using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_Blogs.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication_Blogs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly BlogDBContext _context = null;
        public BlogsController(BlogDBContext context)
        {
            _context = context;
        }
        // GET: api/<BlogsController>
        [HttpGet]
        [Authorize]
        public IEnumerable<Blog> Get()
        {
            return _context.Blogs.ToList();
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            var blog = _context.Blogs.Find(id);
            if (blog == null)
                return BadRequest("Blog not found!");
            return Ok(blog);
        }

        [HttpPost]
        [Authorize]
        public Blog Post(Blog blog)
        {
            Blog newBlog = new Blog()
            {
                Title = blog.Title,
                Content = blog.Content,
                Image=blog.Image
            };
            _context.Blogs.Add(newBlog);
            _context.SaveChanges();
            return newBlog;
        }

        [HttpPut("{id}")]
        [Authorize]
        public IActionResult Put(int id, [FromBody] Blog updated_blog)
        {
            var blog = _context.Blogs.Find(id);
            if (blog == null)
            {
                return BadRequest("Blog not found to be updated!");
            }
            blog.Title = updated_blog.Title;
            blog.Content = updated_blog.Content;
            _context.SaveChanges();
            return Ok(blog);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            var blog = _context.Blogs.Find(id);
            if (blog == null)
            {
                return BadRequest("Blog not found to be deleted!");
            }
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
            return Ok("blog \""+blog.Title+"\" deleted successfully");
        }
    }
}
