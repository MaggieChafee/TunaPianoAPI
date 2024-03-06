using TunaPianoAPI.DTOs;
using TunaPianoAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace TunaPianoAPI.Controllers
{
    public class SongRequests
    {
        public static void Map(WebApplication app)
        {
            app.MapGet("/songs", (TunaPianoDbContext db) =>
            {
                var allSongs = db.Songs.ToList();
                if (allSongs == null) 
                {
                    return Results.NotFound();
                }
                return Results.Ok(allSongs);
            });

            app.MapGet("/songs/{id}", (TunaPianoDbContext db, int id) =>
            {
                Song song = db.Songs.Include(s => s.Genres).SingleOrDefault(s => s.Id == id);
                if (song == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(song);
            });

            app.MapPost("/songs", (TunaPianoDbContext db, PostSongDto dto) =>
            {
                Song newSong = new()
                {
                    Title = dto.Title,
                    ArtistId = dto.ArtistId,
                    Album = dto.Album,
                    Length = dto.Length,
                };
                
                db.Songs.Add(newSong);
                db.SaveChanges();
                return Results.Created();
            });

            app.MapPut("/songs/{id}", (TunaPianoDbContext db, int Id, Song song) =>
            {
                Song songToUpdate = db.Songs.FirstOrDefault(s => s.Id == Id);
                if (songToUpdate == null)
                {
                    return Results.NotFound();
                }
                songToUpdate.Length = song.Length;
                songToUpdate.Album = song.Album;
                songToUpdate.ArtistId = song.ArtistId;
                songToUpdate.Title = song.Title;
                db.SaveChanges();
                return Results.Ok();
            });

            app.MapDelete("/songs/{id}", (TunaPianoDbContext db, int id) =>
            {
                Song songToDelete = db.Songs.FirstOrDefault(s => s.Id ==id);
                if (songToDelete == null)
                {
                    return Results.NotFound();
                }
                db.Songs.Remove(songToDelete);
                db.SaveChanges();
                return Results.NoContent();
            });

            app.MapPost("/songs/assign-genre", (TunaPianoDbContext db, SongGenreDto dto) =>
            {
                var songToAssign = db.Songs.SingleOrDefault(s => s.Id == dto.SongId);
                var addGenre = db.Genres.SingleOrDefault(g => g.Id == dto.GenreId);
                if (songToAssign == null || addGenre == null)
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
