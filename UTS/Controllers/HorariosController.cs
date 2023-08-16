using Microsoft.AspNetCore.Mvc;
using UTS.Datos;
using UTS.Models;

namespace UTS.Controllers
{
    public class HorariosController : Controller
    {
        HorarioDatos _horarioDatos = new HorarioDatos();
        public IActionResult Listar()
        {
            var lista = _horarioDatos.Listar();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Insertar() 
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Insertar(horario_agendaModel model)
        {
            if(!ModelState.IsValid)
            {
                return RedirectToAction("Listar");
            }
            bool respuesta = _horarioDatos.InsertarApartado(model);
            if(respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Eliminar(int idhorario)
        {
            var _horario = _horarioDatos.ConsultarHorario(idhorario);
            return View(_horario);
        }
        [HttpPost]
        public IActionResult Eliminar(horario_agendaModel model)
        {
            var respuesta = _horarioDatos.EliminarHorario(model.idhorario);
            if(respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Editar(int idhorario)
        {
            horario_agendaModel _contacto = _horarioDatos.ConsultarHorario(idhorario);
            return View(_contacto);
        }
        [HttpPost]
        
        public IActionResult Editar(horario_agendaModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var respuesta = _horarioDatos.EditarApartado(model);
            if(respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
            {
                return View();
            }
        }
    }



}
