using TunaPianoAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace TunaPianoAPI.Controllers
{
    public class GenreRequests
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/genres", (TunaPianoDbContext db) =>
            {
                var allGenres = db.Genres.ToList();
                if (allGenres == null) 
                {
                    return Results.BadRequest();
                }
                return Results.Ok(allGenres);
            });
        }
    }
}
