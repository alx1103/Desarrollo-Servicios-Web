using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APP_003Entity.Models;

namespace APP_003Entity.Controllers
{
    public class NegociosController : Controller
    {
        // GET: Negocios

        Negocios2023Entities db = new Negocios2023Entities();
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ProductosListar()
        {
            /*p representa cada objeto, select p quiere decir que incluira todas las propiedades*/
            var lista = from p in db.usp_ProductoListar() select p;
            return View(lista);
        }

        public ActionResult ProductoNombre(string nombre)
        {
            if (nombre == null)
            {
                nombre = string.Empty;
            }         
            ViewBag.nombre = nombre;
            var lista = from p in db.usp_ProductoListar() where p.NomProducto.Contains(nombre) select p;
            return View(lista);
        }

        public ActionResult PedidosCliente(string id)
        {
            if (id == null)
            {   
                id = string.Empty;
            }
            /*lo que quiero capturar es el idCliente, y lo que quiero mostrar es NomCliente*/
            /*obtendra el nomCliente como visible, y el idCliente como oculto*/
            /*SelectList es una lista desplegable*/
            ViewBag.clientes = new SelectList(db.clientes.ToList(),"idCliente", "NomCliente");

            var lista = from p in db.pedidoscabe where p.IdCliente == id select p;
            return View(lista);
        }

    }
}