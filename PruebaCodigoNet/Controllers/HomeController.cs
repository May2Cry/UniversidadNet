using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using universidades.shared;

namespace PruebaCodigoNet.Controllers
{
    public class HomeController : Controller
    {

        private testcoyoteContext _Context;



        public HomeController(testcoyoteContext context)
        {
            _Context = context;

        }
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            var favoritos = _Context.Universidades.Where(x => x.favorito == true);

            return Json(await DataSourceLoader.LoadAsync(favoritos, loadOptions));
            //   return DataSourceLoader.Load(SampleData.Orders, loadOptions);
        }

        [HttpDelete]
        public void DeleteOrder(string key)
        {
            var order = _Context.Universidades.First(o => o.Id.ToString() == key);
            _Context.Universidades.Remove(order);
            _Context.SaveChanges();
        }

        [HttpPut]
        public ActionResult UpdateOrder(string key, string values)
        {
            var order = _Context.Universidades.First(o => o.Id.ToString() == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _Context.SaveChanges();

            return Ok(order);
        }

    }
}
