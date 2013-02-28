using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webinar.Data
{
    using System.Web.Mvc;

    using Webinar.Models;

    public interface IPostsRepository
    {
        List<Post> All();
    }

    public class PostsRepository : IPostsRepository
    {
        Blog blogContext = new Blog();

        public List<Post> All()
        {
            return blogContext.Posts.ToList();
        }
    }
}