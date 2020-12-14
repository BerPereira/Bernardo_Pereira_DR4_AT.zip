using BernardoATJohn.Domain.Entities;
using BernardoATJohn.MVC.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BernardoATJohn.Data.Repository
{
    public class Repository
    {
        private ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> BuscarTodosAsync()
        {
            var books = await _context.Set<Book>().ToListAsync();
            return books;
        }

        public async Task<Book> BuscarAsync(int id)
        {
            var book = await _context.Set<Book>().FindAsync(id);
            return book;
        }

        public async Task<Book> IserirAsync(Book id)
        {
            var book = await _context.Set<Book>().AddAsync(id);
            await _context.SaveChangesAsync();
            return book.Entity;
        }

        public async Task<Book> BookDelete(int id)
        {
            var book = await _context.Books.FindAsync(id);

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<Book> BookEdit(Book id)
        {

            _context.Entry(id).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return _context.Set<Book>().Find(id);
        }
    }
}
