using Microsoft.AspNetCore.Mvc;

/*vamos hacer referencia a model*/
using AP001.Models;
using System.Security.AccessControl;

namespace AP001.Controllers
{
    public class AlumnoController : Controller
    {
        /*static--> puede ser accedido por el nombre de la clase, sin necesidad de instanciarla*/
        public static List<Alumno> listaAlumno = new List<Alumno>();

        public static int cuenta = 0;

        /*para agregar la Vista, en cualquier parte del cuerpo colocamos click derecho*/
        public IActionResult Index()
        {
            if(cuenta == 0) {
                /*esta lista tendrá 4 objetos*/
                listaAlumno = new List<Alumno>
                {
                    /*Creando un objeto alumno*/
                    new Alumno()
                    {
                        IdAlumno = 1,   ApellidoAlumno = "Sotelo",NombreAlumno = "Dolly",EdadAlumno = 22
                    },
                    new Alumno()
                    {
                        IdAlumno = 2,   ApellidoAlumno = "Sotelo",NombreAlumno = "Dolly",EdadAlumno = 22
                    }, 
                    new Alumno()
                    {
                        IdAlumno = 3,   ApellidoAlumno = "Sotelo",NombreAlumno = "Dolly",EdadAlumno = 22
                    },                
                    new Alumno()
                    {
                        IdAlumno = 4,   ApellidoAlumno = "Sotelo",NombreAlumno = "Dolly",EdadAlumno = 22
                    }
                }; cuenta++; /*lo de la cuenta es: para que ya no se vuelva a cargar ya que primero vale 0*/
            }
            return View(listaAlumno);
        }

        public IActionResult Create()
        {
            /*por defecto es un get, nos retornará una clase vacía, los datos estan vacíos*/
            return View(new Alumno());
        }

        /*ActionResult representa el resultado de una acción del controlador, ese Alumno es lo que 
         se necesita para crear*/
        /*esta solicitud solo funcionará si se le envía una solicitud*/
        [HttpPost]
        public IActionResult Create(Alumno objeto)
        {
            int cant = listaAlumno.Count() + 1;

            /*instanciando la clase Alumno*/
            Alumno obj = new Alumno();
            /*porque cant? porque la lista botará cuantos alumnos hay (id) +1
             asi se ira incrementando*/
            obj.IdAlumno = cant;
            obj.ApellidoAlumno = objeto.ApellidoAlumno;
            obj.NombreAlumno = objeto.NombreAlumno;
            obj.EdadAlumno = objeto.EdadAlumno;

            listaAlumno.Add(obj);

            /*la vista irá al index cuando se carge*/
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            /*se buca en la lista alumnos el id que sea igual al parametro mostrado
             y después devuelve el primer elemento de la lista que es el id*/
            /*x representa cada elemento de la lista alumno*/
            /*devolverá el primer objeto de la lista*/
            var alu = listaAlumno.Where(x => x.IdAlumno == id).FirstOrDefault();
            return View(alu);
        }

        /*acción que viene de una página*/
        /*get es directo*/
        [HttpPost] 
        public IActionResult Edit(Alumno objeto)
        {
            var alu = listaAlumno.Where(x => x.IdAlumno == objeto.IdAlumno).FirstOrDefault();
            alu.ApellidoAlumno = objeto.ApellidoAlumno;
            alu.NombreAlumno = objeto.NombreAlumno;
            alu.EdadAlumno = objeto.EdadAlumno;

            return RedirectToAction("Index");
        }

        public IActionResult Eliminar(int id)
        {
            var alu = listaAlumno.Where(x => x.IdAlumno == id).FirstOrDefault();
            listaAlumno.Remove(alu);
            return RedirectToAction("Index");
        }

    }
}
