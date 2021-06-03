using System;
using System.Collections.Generic;

namespace WebApplication_Blogs.Models
{
    public partial class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
    }
}
