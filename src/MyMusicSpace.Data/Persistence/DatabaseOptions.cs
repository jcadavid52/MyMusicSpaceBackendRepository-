namespace MyMusicSpace.Data.Persistence
{
    public class DatabaseOptions
    {
        public const string SectionName = "DatabaseOptions";
        public bool AutomaticMigrations { get; set; }
        public bool AutomaticSeedData { get; set; }
    }
}
