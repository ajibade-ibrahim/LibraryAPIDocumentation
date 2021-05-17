using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.API.Contexts;
using Library.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Services
{
    public class BookRepository : IBookRepository
    {
        public BookRepository(LibraryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        private readonly LibraryContext _context;

        public void AddBook(Book bookToAdd)
        {
            if (bookToAdd == null)
            {
                throw new ArgumentNullException(nameof(bookToAdd));
            }

            _context.Add(bookToAdd);
        }

        public async Task<Book> GetBookAsync(Guid authorId, Guid bookId)
        {
            ValidateGuidId(authorId, nameof(authorId));
            ValidateGuidId(bookId, nameof(bookId));

            return await _context.Books.Include(b => b.Author)
                .Where(b => b.AuthorId == authorId && b.Id == bookId)
                .FirstOrDefaultAsync();
        }

        private static void ValidateGuidId(Guid id, string paramName)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException(paramName);
            }
        }

        public async Task<IEnumerable<Book>> GetBooksAsync(Guid authorId)
        {
            return authorId == Guid.Empty
                ? throw new ArgumentException(nameof(authorId))
                : await GetBooksForAuthor(authorId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            // return true if 1 or more entities were changed
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
        }

        protected async Task<List<Book>> GetBooksForAuthor(Guid authorId)
        {
            return await _context.Books.Include(b => b.Author).Where(b => b.AuthorId == authorId).ToListAsync();
        }
    }
}