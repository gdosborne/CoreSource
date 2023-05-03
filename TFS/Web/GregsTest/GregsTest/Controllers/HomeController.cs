using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GregsTest.Models;

namespace GregsTest.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            ViewBag.Message = "Lorem ipsum dolor sit amet, ex pri modo decore, id vis inani solet periculis, " +
                "id sint postulant eam. Sint integre in mea. Ridens sapientem democritum et eos. Et similique " +
                "forensibus nam, nonumy scriptorem in per.";
            ViewBag.Message1 = "Pro equidem fuisset ei, et vel graecis " +
                "iracundia.Pri at lobortis voluptatibus, te hinc cotidieque sit.Ei idque iracundia vix? Tollit " +
                "verterem sea ei. Accusata moderatius ullamcorper at nec, habeo partem dissentiunt sea at.Eu " +
                "illum suavitate mel, has eu nostro repudiare?";
            var model = ModelFactory.CreateModel<HomeModel>();
            return View(model);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}