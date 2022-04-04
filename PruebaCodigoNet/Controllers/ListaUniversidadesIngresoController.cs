using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using universidades.shared;

namespace PruebaCodigoNet.Controllers
{
    public class ListaUniversidadesIngresoController : Controller
    {

        private testcoyoteContext _Context;



        public ListaUniversidadesIngresoController(testcoyoteContext context)
        {
            _Context = context;

        }
        // GET: ListaUniversidadesIngresoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ListaUniversidadesIngresoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ListaUniversidadesIngresoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListaUniversidadesIngresoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ListaUniversidadesIngresoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ListaUniversidadesIngresoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ListaUniversidadesIngresoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListaUniversidadesIngresoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(universidades.shared.Universidade universidad)
        {
            if (ModelState.IsValid)
            {

                universidad.favorito = true;
                _Context.Universidades.Add(universidad);
                _Context.SaveChanges();
                return View();
            }

            return View(universidad);
        }



        [HttpPost]
        public async Task<ActionResult> Cargarjson(IFormFile txtjson)
        {
            try
            {
                List<universidades.shared.Universidade> unive = new List<Universidade>();
                string fileContent = null;
                using (var reader = new StreamReader(txtjson.OpenReadStream()))
                {
                    fileContent = reader.ReadToEnd();
                }
                var result = JsonConvert.DeserializeObject<List<Models.Universidades>>(fileContent);
                foreach(var dato in result)
                {
                    unive.Add(new universidades.shared.Universidade { Name=dato.Name,Country=dato.Country,Domains= string.Join(",", dato.Domains),WebPages= string.Join(",", dato.web_pages),favorito=true});
                }
                _Context.Universidades.AddRange(unive);
                await _Context.SaveChangesAsync();
                return Redirect("Home");
            }
            catch(Exception ex){

            }

            return BadRequest();


        }



    }
}
