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
                    return Results.NotFound();
                }
                return Results.Ok(allGenres);
            });

            app.MapGet("/genres/{id}", (TunaPianoDbContext db, int id) =>
            {
                var singleGenre = db.Genres.SingleOrDefault(g => g.Id == id);
                if (singleGenre == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(singleGenre);
            });

            app.MapPost("/genres", (TunaPianoDbContext db, Genre newGenre) =>
            {
                db.Genres.Add(newGenre);
                db.SaveChanges();
                return Results.Created($"/genres/{newGenre.Id}", newGenre);
            });

            app.MapPut("/genres/{id}", (TunaPianoDbContext db, Genre genre, int id) =>
            { 
                Genre genreToUpdate = db.Genres.SingleOrDefault(gen => gen.Id == id);
                if (genreToUpdate == null)
                {
                    return Results.NotFound();
                }
                genreToUpdate.Description = genre.Description;
                db.SaveChanges();
                return Results.Ok(genre);
            });

            app.MapDelete("/genres/{id}", (TunaPianoDbContext db, int id) =>
            {
                Genre genreToDelete = db.Genres.SingleOrDefault(gen => gen.Id == id);
                if (genreToDelete == null)
                {
                    return Results.NotFound();
                }
                db.Genres.Remove(genreToDelete);
                db.SaveChanges();
                return Results.NoContent();
            });
        }
    }
}
