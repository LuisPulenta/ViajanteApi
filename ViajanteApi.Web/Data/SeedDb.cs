﻿using System.Threading.Tasks;
namespace ViajanteApi.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        
        public SeedDb(DataContext context)
        {
            _context = context;
                }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

        }
            
    }
}