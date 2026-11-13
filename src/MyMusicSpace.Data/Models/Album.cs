namespace MyMusicSpace.Data.Models
{
    public class Album : BaseModel
    {
        public Guid Id { get; set; }
        public string ProviderId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public Guid SongId { get; set; }
        public Song Song { get; set; } = new();
    }
}
