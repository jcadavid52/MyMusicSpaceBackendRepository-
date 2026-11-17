namespace MyMusicSpace.Data.Persistence.Models
{
    public class ArtistSong : BaseModel
    {
        public Guid ArtistId { get; set; }
        public Guid SongId { get; set; }
        public Song Song { get; set; } = null!;
        public Artist Artist { get; set; } = null!;
    }
}
