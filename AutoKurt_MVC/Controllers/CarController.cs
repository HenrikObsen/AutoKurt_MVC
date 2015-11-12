using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AKrepo;


namespace AutoKurt_MVC.Controllers
{
    public class CarController : Controller
    {
        
        public ActionResult Soeg()
        {
           
            return View();
        }

        BilFac bf = new BilFac();
        [HttpPost]
        public ActionResult SoegResult(string keyWord)
        {
            return View("Soeg", bf.Search(keyWord));
        }

        public ActionResult EnBilMedBilleder()
        {
            return View(bf.GetWithImages(2));
        }

        public ActionResult FlereBilerMedBilleder()
        {
            return View(bf.GetAllWithImages());
        }

        public ActionResult HentBilerMedProducent()
        {
            return View(bf.GetAllBilMedProducent());
        }

        ProducentFac pf = new ProducentFac();
        SoegViewModel svm = new SoegViewModel();
        public ActionResult AdvSoeg()
        {
            svm.Biler = null;
            svm.Producenter = pf.GetAll();
            return View(svm);
        }

        [HttpPost]
        public ActionResult AdvSoeg(string maxpris, string producent, string keyword)
        {
            svm.Biler = bf.AdvSearch(producent, maxpris, keyword);
            svm.Producenter = pf.GetAll();
            return View(svm);
        }
    }
}


