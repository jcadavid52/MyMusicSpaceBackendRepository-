namespace MyMusicSpace.Data.Models
{
    public class Artist : BaseModel
    {
        public Guid Id { get; set; } 
        public string ProviderId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public IReadOnlyCollection<ArtistSong> ArtistSongs { get; set; } = [];
    }
}
