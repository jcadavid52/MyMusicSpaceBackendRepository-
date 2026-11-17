namespace MyMusicSpace.Data.Persistence.Models
{
    public class Album : BaseModel
    {
        public Guid Id { get; set; }
        public string ProviderId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public ICollection<Song> Songs { get; set; } = [];
    }
}
