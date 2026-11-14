namespace MyMusicSpace.Data.Models
{
    public class Song : BaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Lyrics { get; set; } = string.Empty;
        public long Duration { get; set; }
        public string ProviderId { get; set; } = string.Empty;
        public string ProviderUrlTrack { get; set; } = string.Empty;
        public string? Translation { get; set; }
        public Tablature? Tablature { get; set; } = null!;
        public Guid AlbumId { get; set; }
        public Album Album { get; set; } = null!;
        public ICollection<ArtistSong> ArtistSongs { get; set; } = [];

    }
}
