using Microsoft.AspNetCore.Mvc.RazorPages;
using ProiectMPD.Data;

namespace ProiectMPD.Models
{
    public class ReleaseGenresPageModel:PageModel
    {
        public List<AssignedGenreData> AssignedGenreDataList;
        public void PopulateAssignedGenreData(ApplicationDbContext context, Release release)
        {
            var allGenres = context.Genres;
            var releaseGenres = new HashSet<int>(
            release.ReleaseGenres.Select(c => c.GenreID)); //
            AssignedGenreDataList = new List<AssignedGenreData>();
            foreach (var gen in allGenres)
            {
                AssignedGenreDataList.Add(new AssignedGenreData 
                {
                    GenreID = gen.ID,
                    Name = gen.Name,
                    Assigned = releaseGenres.Contains(gen.ID)
                });
            }
        }
        public void UpdateReleaseGenres(ApplicationDbContext context,
        string[] selectedGenres, Release releasetoUpdate)
        {
            if (selectedGenres == null)
            {
                releasetoUpdate.ReleaseGenres = new List<ReleaseGenre>();
                return;
            }
            var selectedGenresHS = new HashSet<string>(selectedGenres);
            var releaseGenres = new HashSet<int>
            (releasetoUpdate.ReleaseGenres.Select(c => c.Genre.ID));
            foreach (var gen in context.Genres)
            {
                if (selectedGenresHS.Contains(gen.ID.ToString()))
                {
                    if (!releaseGenres.Contains(gen.ID))
                    {
                        releasetoUpdate.ReleaseGenres.Add(
                        new ReleaseGenre 
                        {
                            ReleaseID = releasetoUpdate.ID,
                            GenreID = gen.ID
                        });
                    }
                }
                else
                {
                    if (releaseGenres.Contains(gen.ID))
                    {
                        ReleaseGenre courseToRemove = releasetoUpdate.ReleaseGenres
                   .SingleOrDefault(i => i.GenreID== gen.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
                }
