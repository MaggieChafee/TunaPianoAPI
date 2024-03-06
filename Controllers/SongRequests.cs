using TunaPianoAPI.DTOs;

namespace TunaPianoAPI.Controllers
{
    public class SongRequests
    {
        public static void Map(WebApplication app)
        {
            app.MapPost("/songs/assign-genre", (TunaPianoDbContext db, SongGenreDto songGenreDto) =>
            {
                var songToAssign = db.Songs.SingleOrDefault(s => s.Id == songGenreDto.SongId);
                var addGenre = db.Genres.SingleOrDefault(g => g.Id == songGenreDto.GenreId);
                if (songToAssign == null ||  addGenre == null)
                {
                    return Results.NotFound();
                }
                songToAssign.Genres.Add(addGenre);
                db.SaveChanges();
                return Results.Ok();
            });
        }   
    }
}
