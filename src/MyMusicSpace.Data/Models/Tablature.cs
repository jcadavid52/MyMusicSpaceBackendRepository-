namespace MyMusicSpace.Data.Models
{
    public class Tablature
    {
        public Guid Id { get; set; }
        public string TextChord { get; set; } = string.Empty;
        public IEnumerable<string> ImagesChords { get; set; } = [];
        public Guid SongId { get; set; }
        public Song Song { get; set; } = new();
    }
}
