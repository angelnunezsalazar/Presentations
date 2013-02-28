using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WebinarTests
{
    using Webinar.Controllers;
    using System.Web.Mvc;

    [TestFixture]
    public class PostsControllerTests
    {
        [Test]
        public void MuestraUnaVistaParaRegistrarUnPost() {
            PostsController controller=new PostsController(null);

            var view = controller.Create() as ViewResult;

            Assert.AreEqual(String.Empty, view.ViewName);
        }
    }
}
