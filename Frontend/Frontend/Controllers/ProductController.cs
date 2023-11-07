using Frontend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Collections.Generic;
using static System.Net.WebRequestMethods;

namespace Frontend.Controllers
{
    public class ProductController : Controller
    {


        private readonly IConfiguration _configuration;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        string BaseUrl = "https://localhost:44316/";

        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            
            List<Product> ProdInfo = new List<Product>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await client.GetAsync("api/Product");

                if (Res.IsSuccessStatusCode)
                {
                    var PrResponse = await Res.Content.ReadAsStringAsync();

                    ProdInfo = JsonConvert.DeserializeObject<List<Product>>(PrResponse);
                }
            }
            return View(ProdInfo);
        }


        // GET: ProductController/Details/5
      /*  public ActionResult Details(int id)
        {
            return View();
        }*/

        public async Task<ActionResult> Details(int id)
        {
            var product = new Product();

            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync($"api/Product/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                product = JsonConvert.DeserializeObject<Product>(responseContent);
            }

            return View(product);
        }



        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Create(Product viewModel)
        {
            var product = new Product
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
                ProductCategory = new Category
                {
                    Name = "string",
                    Description = "string",
                }

            };

            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var customerJson = JsonConvert.SerializeObject(product);
            var content = new StringContent(customerJson, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/Product", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Redirect to the customer list or another appropriate action
            }
            else
            {
                // Handle the error, possibly by displaying an error message or returning an error view
                return View("Error");
            }

            return View(viewModel);
        }






        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Product viewModel)
        {
            var product = new Product
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
                ProductCategory = new Category
                {
                    Id = 0,
                    Name = "string",
                    Description = "string",
                }
            };

            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Serialize the modified customer object to JSON and send it in the request body
            var customerJson = JsonConvert.SerializeObject(product);
            var content = new StringContent(customerJson, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"api/Product/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Redirect to the customer list or another appropriate action
            }

            // Handle the case where the update failed or ModelState is not valid
            return View(product);
        }


        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.DeleteAsync($"api/Product/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index"); // Redirect to the order list or another appropriate action
            }
            else
            {
                return View("Error");
            }
        }






    }
}
