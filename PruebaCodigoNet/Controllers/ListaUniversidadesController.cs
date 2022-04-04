using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RESTCountries.Models;
using RESTCountries.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using universidades.shared;

namespace PruebaCodigoNet.Controllers
{


    public class ListaUniversidadesController : Controller
    {

        private testcoyoteContext _Context;



        public ListaUniversidadesController(testcoyoteContext context)
        {
            _Context = context;

        }
        // GET: ListaUniversidadesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ListaUniversidadesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ListaUniversidadesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListaUniversidadesController/Create
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

        // GET: ListaUniversidadesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ListaUniversidadesController/Edit/5
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

        // GET: ListaUniversidadesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListaUniversidadesController/Delete/5
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

        [HttpGet]
        public ActionResult Ingresar(string nombre,string country,string web_pages, string Domains , string Imagen, string favorito)
        {

            if (bool.Parse(favorito))
            {
                universidades.shared.Universidade uni = new universidades.shared.Universidade();
                uni.Name = nombre;
                uni.WebPages = web_pages;
                uni.Imagen = Imagen;
                uni.favorito = bool.Parse(favorito);
                uni.Domains = Domains;
                uni.Country = country;
                _Context.Universidades.Add(uni);
                _Context.SaveChanges();
                return View();

            }
            else
            {
                var dato = _Context.Universidades.Where(x => x.Name == nombre).FirstOrDefault();
                _Context.Universidades.Remove(dato);
                _Context.SaveChanges();
                    return View();
            }

        }

        [HttpGet]

      public async Task<Object> SearchUniCountry(DataSourceLoadOptions loadOptions)
        {
            List<Country> countries = await RESTCountriesAPI.GetAllCountriesAsync();
            List<PruebaCodigoNet.Models.Country> namecount = new List<PruebaCodigoNet.Models.Country>();
            foreach (var name in countries)
            {
                namecount.Add(new Models.Country { Name=name.Name });
            }

            var queryable = namecount.AsQueryable();

            return DataSourceLoader.Load(queryable, loadOptions);

        }


        [HttpGet]
        public async Task<Object> ConsultaPais (DataSourceLoadOptions loadOptions, string Pais)
        {

            try
            {
                string html = string.Empty;
                string url = @"http://universities.hipolabs.com/search?country=" + Pais;

        
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }

                var list = JsonConvert.DeserializeObject<List<Models.Universidades>>(html);
                
                var dbuniversidad = _Context.Universidades.Where(x => x.Country == Pais);
                List<Models.Universidades> list2 = new List<Models.Universidades>();
                list2.AddRange(list);
                foreach (var lis in list2)
                {
                    
                    foreach (var uni in dbuniversidad)
                    {
                        List<string> web = new List<string>();
                        web.Add(uni.WebPages);
                        List<string> domai = new List<string>();
                        domai.Add(uni.Domains);
                        if (lis.Name==uni.Name && lis.Country == uni.Country)
                        {
                            int pos = list.IndexOf(lis);
                            list.Remove(lis);
                            list.Insert(pos,(new Models.Universidades { Name = uni.Name, Country = uni.Country, web_pages = web, Domains = domai, Imagen = uni.Imagen, favorito = uni.favorito }));

                        }

                    }
                }
      

                var queryable = list.AsQueryable();

                return DataSourceLoader.Load(queryable, loadOptions);

            }
            catch (Exception ex)
            {

            }
            return BadRequest();
        }



    }
}
