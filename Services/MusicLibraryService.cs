using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiectMPD.Data;
using ProiectMPD.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace ProiectMPD.Services
{ 
    public class MusicLibraryService
    {
        private readonly ApplicationDbContext _context;

        public MusicLibraryService(ApplicationDbContext context)
        {
            _context = context;
        }
    public async Task<bool> AddReleaseToLibrary(string userId, int releaseId)
    {
        var library = await _context.MusicLibraries.FirstOrDefaultAsync(l => l.UserId == userId);


        if (library.Releases == null)
        {
            library.Releases = new List<Release>();
        }

        if (!library.Releases.Any(r => r.ID == releaseId))
        {
            var releaseToAdd = await _context.Releases.FindAsync(releaseId);
            if (releaseToAdd == null)
            {
                return false;
            }

            library.Releases.Add(releaseToAdd);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
        public async Task<bool> RemoveReleaseFromLibrary(string userId, int releaseId)
        {
            var library = await _context.MusicLibraries
                .Include(l=> l.Releases)
                .FirstOrDefaultAsync(l => l.UserId == userId);



            if (library.Releases.Any(r => r.ID == releaseId))
            {
                var releaseToRemove = await _context.Releases.FindAsync(releaseId);
                library.Releases.Remove(releaseToRemove);
                await _context.SaveChangesAsync();
                return true;
            }
            return false; // Nu exista in library

        }
    }
}
