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
        public string? Traslation { get; set; }
        public Guid TablatureId { get; set; }
        public Tablature Tablature { get; set; } = new();
        public IReadOnlyCollection<Album> Albums { get; set; } = [];
        public IReadOnlyCollection<ArtistSong> ArtistSongs { get; set; } = [];

    }
}
