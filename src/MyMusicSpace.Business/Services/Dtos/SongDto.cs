namespace MyMusicSpace.Business.Services.Dtos
{
    public record SongDto(
        Guid Id,
        string ProviderId,
        string ProviderUrlTrack,
        string Name,
        string Lyrics,
        string? Translation,
        long Duration,
        IEnumerable<ArtistDto> Artists,
        AlbumDto Album,
        TablatureDto? Tablature);
}
