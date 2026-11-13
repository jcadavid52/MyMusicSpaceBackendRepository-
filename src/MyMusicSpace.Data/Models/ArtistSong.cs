namespace MyMusicSpace.Data.Models
{
    public class ArtistSong : BaseModel
    {
        public Guid ArtistId { get; set; }
        public Guid SongId { get; set; }
        public Song Song { get; set; } = new();
        public Artist Artist { get; set; } = new();
    }
}
