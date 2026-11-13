namespace MyMusicSpace.Business.Services.Dtos
{
    public record TablatureDto(
        Guid Id,
        string TextChord,
        IEnumerable<string> ImagesChords);
}
