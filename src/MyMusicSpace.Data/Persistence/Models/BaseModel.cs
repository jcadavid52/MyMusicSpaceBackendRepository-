namespace MyMusicSpace.Data.Persistence.Models
{
    public abstract class BaseModel
    {
        public DateTime CreationDate { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}
