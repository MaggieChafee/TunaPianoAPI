using Microsoft.EntityFrameworkCore;
using TunaPianoAPI.DTOs;
using TunaPianoAPI.Models;

namespace TunaPianoAPI.Controllers
{
    public class ArtistRequests
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/artists", (TunaPianoDbContext db) =>
            {
                var allArtists = db.Artists.ToList();
                if (allArtists == null) 
                {
                    return Results.NotFound();
                }
                return Results.Ok(allArtists);
            });

            app.MapGet("/artists/{id}", (TunaPianoDbContext db, int id) =>
            {
                var singleArtist = db.Artists.Include(s => s.Songs).SingleOrDefault(a => a.Id == id);
                if (singleArtist == null) 
                {
                    return Results.NotFound();
                }
                return Results.Ok(singleArtist);
            });

            app.MapPost("/artists", (TunaPianoDbContext db, PostArtistDto dto) =>
            {
                Artist newArtist = new()
                {
                    Name = dto.Name,
                    Age = dto.Age,
                    Bio = dto.Bio,
                };
                db.Artists.Add(newArtist);
                db.SaveChanges();
                return Results.Created($"/artists/{newArtist.Id}", newArtist);
            });

            app.MapPut("/artists/{id}", (TunaPianoDbContext db, int id, PostArtistDto dto) => 
            { 
                Artist artistToUpdate = db.Artists.Where(a => a.Id == id).FirstOrDefault();
                if (artistToUpdate == null)
                {
                    return Results.NotFound();
                }
                artistToUpdate.Name = dto.Name;
                artistToUpdate.Age = dto.Age;
                artistToUpdate.Bio = dto.Bio;
                db.SaveChanges();
                return Results.Ok(artistToUpdate);
            });

            app.MapDelete("/artists/{id}", (TunaPianoDbContext db, int id) =>
            {
                Artist artistToDelete = db.Artists.FirstOrDefault(a => a.Id == id);
                if (artistToDelete == null)
                {
                    return Results.NotFound();
                }
                db.Artists.Remove(artistToDelete); 
                db.SaveChanges();
                return Results.NoContent();
            });
        }
    }
}
