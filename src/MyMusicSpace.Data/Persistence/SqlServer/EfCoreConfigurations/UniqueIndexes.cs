namespace MyMusicSpace.Data.Persistence.SqlServer.EfCoreConfigurations
{
    public static class UniqueIndexes
    {
        public const string SongUserProvider = "IX_Songs_UserName_ProviderId";
        public const string AlbumUserProvider = "IX_Albums_UserName_ProviderId";
        public const string ArtistUserProvider = "IX_Artists_UserName_ProviderId";
    }
}
