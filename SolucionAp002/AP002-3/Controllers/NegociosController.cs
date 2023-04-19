using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AP002_3.Models;

namespace AP002_3.Controllers
{
    public class NegociosController : Controller
    {
        // GET: Negocios

        Negocios2023Entities db = new Negocios2023Entities();
        public ActionResult Index()
        {
            return View(db.usp_ProductoListar().ToList());
        }
    }
}