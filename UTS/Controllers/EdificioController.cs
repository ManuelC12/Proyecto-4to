using Microsoft.AspNetCore.Mvc;
using UTS.Datos;
using UTS.Models;

namespace UTS.Controllers
{
    public class EdificioController : Controller
    {
        EdificioDatos _edificioDatos = new EdificioDatos();
        public IActionResult Listar()
        {
            var lista = _edificioDatos.Lista();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Insertar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Insertar(EdificioModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool respuesta = _edificioDatos.GuardarEdificio(model);
            // var respuesta = _instalacionDatos.GuardarInstalacion(model);
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Editar(int numedificio)
        {
            //para obtener y mostrar el contacto a modificar
            EdificioModel _edificio = _edificioDatos.ConsultarEdificio(numedificio);
            return View(_edificio);
        }
        [HttpPost]
        public IActionResult Editar(EdificioModel model)
        {
            //Para obtener los datos que se editaron del formulario y enviarlo en la base de datos
            if (!ModelState.IsValid)
            {
                return View();
            }
            var respuesta = _edificioDatos.EditarEdificio(model);
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }

        //Contraolador de Eliminar
        public IActionResult Eliminar(int numedificio)
        {
            //para obtener y mostrar la instalacion a eliminar
            var _edificio = _edificioDatos.ConsultarEdificio(numedificio);
            return View(_edificio);
        }

        [HttpPost]
        public IActionResult Eliminar(EdificioModel model)
        {
            //Para obtener los datos que se van a eliminar del formulario y enviarlo en la base de datos
            var respuesta = _edificioDatos.EliminarEdificio(model.numedificio);
            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            { return View(); }
        }
    }
}
