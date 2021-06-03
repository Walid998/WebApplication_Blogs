using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_Blogs.Models
{
    public class BlogBody
    {
        public IFormFile image;
        public Blog blog;
    }
}
