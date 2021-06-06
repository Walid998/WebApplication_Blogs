using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_Blogs.Models
{
    public class BlogBody
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }

        public IFormFile Image { get; set; }

    }
}
