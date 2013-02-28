using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webinar.Controllers
{
    using Webinar.Data;
    using Webinar.Models;

    public class PostsController : Controller
    {
        private readonly IPostsRepository postsRepository;

        Blog blogContext = new Blog();

        public PostsController(IPostsRepository postsRepository )
        {
            this.postsRepository = postsRepository;
        }

        public ActionResult Index()
        {
            var posts = postsRepository.All();
            return View(posts);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Post post)
        {
            blogContext.Posts.Add(post);
            blogContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var post = blogContext.Posts.First();
            blogContext.Posts.Remove(post);
            blogContext.SaveChanges();
            return Json(string.Empty, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            var blog = blogContext.Posts.First(x => x.Id == id);
            return View(blog);
        }

    }
}
