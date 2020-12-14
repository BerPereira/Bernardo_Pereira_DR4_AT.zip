using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BernardoATJohn.Domain.Entities;
using BernardoATJohn.MVC.Data;
using BernardoATJohn.Data.Repository;

namespace BernardoATJohn.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAPIController : ControllerBase
    {
        private readonly Repository _context;

        public BookAPIController(Repository context)
        {
            _context = context;
        }

        // GET: api/BookAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var book = await _context.BuscarTodosAsync();
            return Ok(book);
        }

        // GET: api/BookAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.BuscarAsync(id);
            return Ok(book);
        }

        // PUT: api/BookAPI/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            var livro = await _context.BookEdit(book);
            return Ok(livro);
        }

        // POST: api/BookAPI
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            var livro = await _context.IserirAsync(book);
            return Ok(livro);
        }

        // DELETE: api/BookAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            var livro = await _context.BookDelete(id);
            return Ok();
        }
    }
}
