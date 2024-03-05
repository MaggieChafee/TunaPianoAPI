using Microsoft.EntityFrameworkCore;
using TunaPianoAPI.Models;

public class TunaPianoDbContext : DbContext
{
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Song> Songs { get; set; }
    public DbSet<Genre> Genres { get; set; }

    public TunaPianoDbContext(DbContextOptions<TunaPianoDbContext> context) : base(context)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>().HasData(new Artist[]
        {
            new Artist { Id = 1, Name = "Ariana Grande", Age = 30, Bio = "Ariana Grande-Butera is an American singer, songwriter, and actress. Regarded as a pop icon, she is noted as an influential figure in popular music and as one of the most prominent vocalists of her generation for her four-octave vocal range and signature whistle register." },
            new Artist { Id = 2, Name = "Carole King", Age = 82, Bio = "Carole King Klein is an American singer-songwriter and musician who has been active since 1958. One of the most successful female songwriters of the latter half of the 20th century in the US, she wrote or co-wrote 118 pop hits on the Billboard Hot 100."}
        });

        modelBuilder.Entity<Song>().HasData(new Song[]
        {
            new Song { Id = 1, ArtistId = 1, Title = "shut up", Album = "Positions", Length = 3},
            new Song { Id = 2, ArtistId = 1, Title = "thank u, next", Album = "thank u, next", Length = 2},
            new Song { Id = 3, ArtistId = 2, Title = "I Fell the Earth Move", Album = "Tapestry", Length = 4},
            new Song { Id = 4, ArtistId = 2, Title = "It's Too Late", Album = "Tapestry", Length = 4}
        });

        modelBuilder.Entity<Genre>().HasData(new Genre[] 
        { 
            new Genre { Id = 1, Description = "Pop"},
            new Genre { Id = 2, Description = "Classic"}
        });
    }
}

