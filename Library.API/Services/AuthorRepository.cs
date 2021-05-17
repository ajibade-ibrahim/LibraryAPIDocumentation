using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.API.Contexts;
using Library.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Services
{
    public class AuthorRepository : IAuthorRepository
    {
        public AuthorRepository(LibraryContext context)
        {
            _context = context;
        }

        private readonly LibraryContext _context;

        public async Task<bool> AuthorExistsAsync(Guid authorId)
        {
            return await _context.Authors.AnyAsync(a => a.Id == authorId);
        }

        public async Task<Author> GetAuthorAsync(Guid authorId)
        {
            return authorId == Guid.Empty
                ? throw new ArgumentException(nameof(authorId))
                : await _context.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _context.Authors.ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            // return true if 1 or more entities were changed
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateAuthor(Author author)
        {
            // no code in this implementation
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
    }
}