using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BernardoATJohn.Domain.Entities;
using BernardoATJohn.MVC.ApiClientHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BernardoATJohn.MVC.Controllers
{
    public class AuthorController : Controller
    {
        ApiClient _apiClient = new ApiClient();

        // GET: AuthorController
        public async Task<ActionResult> Index()
        {
            //1. Lista de autores que será retornada da api:
            List<Author> authors = new List<Author>();

            //2. Consumir a API - Leitura usando o método GetAsync
            HttpResponseMessage response = await _apiClient.Client.GetAsync("api/authorapi");

            //3. Caso a resposta seja OK (código 200)
            if (response.IsSuccessStatusCode)
            {
                //Ler os conteúdos da resposta
                var results = response.Content.ReadAsStringAsync().Result;
                // Interpretar a string resultante com um objeto json
                authors = JsonConvert.DeserializeObject<List<Author>>(results);
            }

            //4. Retornar a view com a lista de autores (preenchida ou vazia)
            return View(authors);
        }

        // GET: AuthorController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Author author = new Author();
            HttpResponseMessage res = await _apiClient.Client.GetAsync($"api/authorapi/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                author = JsonConvert.DeserializeObject<Author>(result);
            }
            return View(author);
        }

        // GET: AuthorController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            var post = _apiClient.Client.PostAsJsonAsync<Author>("api/authorApi", author);
            post.Wait();

            var result = post.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: AuthorController/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            Author author = new Author();
            HttpResponseMessage res = await _apiClient.Client.GetAsync($"api/authorapi/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                author = JsonConvert.DeserializeObject<Author>(result);
            }
            return View(author);
        }

        // POST: AuthorController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Author author)
        {
            var post = _apiClient.Client.PutAsJsonAsync<Author>($"api/authorapi/{author.AuthorId}", author);
            post.Wait();

            var result = post.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: AuthorController/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            Author author = new Author();
            HttpResponseMessage res = await _apiClient.Client.GetAsync($"api/authorapi/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                author = JsonConvert.DeserializeObject<Author>(result);
            }
            return View(author);
        }

        // POST: AuthorController/Delete/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Author author)
        {
            var delete = await _apiClient.Client.DeleteAsync($"api/authorapi/{author.AuthorId}");
            return RedirectToAction("Index");
        }
    }
}
