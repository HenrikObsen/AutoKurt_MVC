using System.Web.Mvc;
using AKrepo;

namespace AutoKurt_MVC.Controllers
{
    public class ProducentController : Controller
    {
        ProducentFac pf = new ProducentFac();

        public ActionResult VisProducent()
        {
            return View(pf.Get(1));
        }

        public ActionResult VisProducenter()
        {
            return View(pf.GetAll());

        }


        public ActionResult VisBilEfterProducent()
        {
            return View(pf.HentMedBil(2));
        }


        public ActionResult NyProducent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NyProducent(Producent p)
        {
            pf.Insert(p);

            ViewBag.MSG = "Producenten er nu oprettet!!!";
            return View();
        }


        public ActionResult UpdateProducent()
        {
            return View(pf.Get(2));
        }

        [HttpPost]
        public ActionResult UpdateProducent(Producent p)
        {
            pf.Update(p);

            ViewBag.MSG = "Producenten er nu opdateret!!!";
            return View(pf.Get(2));
        }


        public ActionResult SletProducent()
        {
            return View(pf.GetAll());
        }


        public ActionResult SletProducentSys(int id)
        {
            pf.Delete(id);

            return View("SletProducent", pf.GetAll());
        }

        public ActionResult HentNavn()
        {
            ViewBag.Prod = "<b>" + pf.GetName(2) + "</b>";
            return View();
        }
    }
}