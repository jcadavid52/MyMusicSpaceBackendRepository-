namespace MyMusicSpace.Business.Services.Dtos
{
    public record SongCreateRequestDto(
        string Name,
        string Lyrics,
        IEnumerable<ArtistCreateRequestDto> Artists,
        AlbumCreateRequestDto Album,
        string ProviderId,
        string ProviderUrlTrack,
        long Duration,
        TablatureCreateRequestDto Tablature,
        string? Traslation);
}
