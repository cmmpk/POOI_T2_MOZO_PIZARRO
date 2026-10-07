using Newtonsoft.Json;
using POOI_T2_MOZO_PIZARRO.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace POOI_T2_MOZO_PIZARRO.Controllers
{
    public class AlumnoController : Controller
    {
        static string jlistAlumnos = @"[]";
        // Ruta de archivo externo
        private string RutaArchivo => Server.MapPath("~/App_Data/alumnos.json");
        // Guardar en Archivo externo
        private void GuardarEnArchivoJson(List<Alumno> lista)
        {
            jlistAlumnos = JsonConvert.SerializeObject(lista, Formatting.Indented);
            string directorio = Path.GetDirectoryName(RutaArchivo);
            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }
            System.IO.File.WriteAllText(RutaArchivo, jlistAlumnos);
        }
        // GET: Alumno
        public ActionResult Index()
        {
            try
            {
                if (System.IO.File.Exists(RutaArchivo))
                {
                    jlistAlumnos = System.IO.File.ReadAllText(RutaArchivo);
                }
                List<Alumno> listIntermedia = JsonConvert.DeserializeObject<List<Alumno>>(jlistAlumnos);
                return View(listIntermedia);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        // GET: Alumno/Details
        public ActionResult Details(string id)
        {
            try
            {
                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jlistAlumnos);
                Alumno alumnoEncontrado = temporal.Find(a => a.dni == id);
                return View(alumnoEncontrado);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        // GET: Alumno/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Alumno/Create
        [HttpPost]
        public ActionResult Create(Alumno alumno)
        {
            try
            {
                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jlistAlumnos);

                // Validación de DNI
                if (temporal.Exists(a => a.dni == alumno.dni))
                {
                    ViewBag.Mensaje = "El DNI ingresado ya existe en la lista.";
                    ViewBag.Exito = false;
                    return View(alumno);
                }

                temporal.Add(alumno);
                GuardarEnArchivoJson(temporal);
                ViewBag.Mensaje = "Alumno agregado con éxito. Redireccionando al listado en 5 segundos...";
                ViewBag.Exito = true;

                return View(alumno);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                ViewBag.Exito = false;
                return View(alumno);
            }
        }

        // GET: Alumno/Edit
        public ActionResult Edit(string id)
        {
            try
            {
                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jlistAlumnos);
                Alumno alumnoEncontrado = temporal.Find(a => a.dni == id);
                return View(alumnoEncontrado);
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View();
            }
        }

        // POST: Alumno/Edit/
        [HttpPost]
        public ActionResult Edit(Alumno alumno)
        {
            try
            {
                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jlistAlumnos);
                int index = temporal.FindIndex(a => a.dni == alumno.dni);

                if (index != -1)
                {
                    temporal[index] = alumno;
                    GuardarEnArchivoJson(temporal);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return View(alumno);
            }
        }

        // POST: Alumno/Delete
        [HttpPost]
        public ActionResult Delete(string id)
        {
            try
            {
                List<Alumno> temporal = JsonConvert.DeserializeObject<List<Alumno>>(jlistAlumnos);
                Alumno alumnoEncontrado = temporal.Find(a => a.dni == id);

                if (alumnoEncontrado != null)
                {
                    temporal.Remove(alumnoEncontrado);
                    GuardarEnArchivoJson(temporal);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
