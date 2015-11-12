using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Helpers;
using AKrepo;

namespace AutoKurt_MVC.Controllers
{
    public class BrugerController : Controller
    {
        BrugerFac bf = new BrugerFac();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginResult()
        {
            string email = Request["Email"].Trim();
            string adgangskode = Crypto.Hash(Request["Password"].Trim());

            Bruger b = bf.Login(email,adgangskode);

            if (b.ID > 0)
            {
                FormsAuthentication.SetAuthCookie(b.ID.ToString(), true);
                Session["UserID"] = b.ID;
                Session["UserName"] = b.Navn;
                Session["Type"] = b.Type;
                Session.Timeout = 120;

                if (!string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                {
                    Response.Redirect(Request.QueryString["ReturnUrl"]);
                }

                return View("Secret");
            }
            else
            {
                ViewBag.MSG = "Brugeren blev ikke fundet!";

                return View("Login");
            }
        }

        [Authorize]
        public ActionResult Secret()
        {
            return View();
        }

        public ActionResult Logud()
        {
            FormsAuthentication.SignOut();
            Session.Contents.RemoveAll();
            return RedirectToAction("Bruger", "Login");
        }
    }
}