namespace MyMusicSpace.Business.Services.Dtos
{
    public record SongDto(
        Guid Id,
        string ProviderId,
        string ProviderUrlTrack,
        string Name,
        string Lyrics,
        string? Traslation,
        string Duration,
        IEnumerable<ArtistDto> Artists,
        AlbumDto Album,
        TablatureDto Tablature);
}
