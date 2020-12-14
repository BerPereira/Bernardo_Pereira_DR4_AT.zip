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
    public class BookController : Controller
    {
        ApiClient _apiClient = new ApiClient();

        // GET: BookController
        public async Task<ActionResult> Index()
        {
            //1. Lista de autores que será retornada da api:
            List<Book> books = new List<Book>();

            //2. Consumir a API - Leitura usando o método GetAsync
            HttpResponseMessage response = await _apiClient.Client.GetAsync("api/bookapi");

            //3. Caso a resposta seja OK (código 200)
            if (response.IsSuccessStatusCode)
            {
                //Ler os conteúdos da resposta
                var results = response.Content.ReadAsStringAsync().Result;
                // Interpretar a string resultante com um objeto json
                books = JsonConvert.DeserializeObject<List<Book>>(results);
            }

            //4. Retornar a view com a lista de autores (preenchida ou vazia)
            return View(books);
        }

        // GET: BookController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Book book = new Book();
            HttpResponseMessage res = await _apiClient.Client.GetAsync($"api/bookapi/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<Book>(result);
            }
            return View(book);
        }

        // GET: BookController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book)
        {
            var post = _apiClient.Client.PostAsJsonAsync<Book>("api/bookApi", book);
            post.Wait();

            var result = post.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: BookController/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            Book book = new Book();
            HttpResponseMessage res = await _apiClient.Client.GetAsync($"api/bookapi/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<Book>(result);
            }
            return View(book);
        }

        // POST: BookController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book)
        {
            var post = _apiClient.Client.PutAsJsonAsync<Book>($"api/bookapi/{book.BookId}", book);
            post.Wait();

            var result = post.Result;
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: BookController/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            Book book = new Book();
            HttpResponseMessage res = await _apiClient.Client.GetAsync($"api/bookapi/{id}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<Book>(result);
            }
            return View(book);
        }

        // POST: BookController/Delete/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Book book)
        {
            var delete = await _apiClient.Client.DeleteAsync($"api/bookapi/{book.BookId}");
            return RedirectToAction("Index");
        }
    }
}
