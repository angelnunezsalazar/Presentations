using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Webinar.Models
{
    public class Blog : DbContext
    {
        public DbSet<Post> Posts { get; set; }
    }
}