using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AP002_34.Models
{
    public class NegociosController : Controller
    {
        Negocios2023Entities db = new Negocios2023Entities();
        // GET: Negocios
        public ActionResult Index()
        {
            return View(db.usp_ProductoListar().ToList());
        }
    }
}