using MyMusicSpace.Business.Services.Dtos;
using MyMusicSpace.Data.Models;

namespace MyMusicSpace.Business.Mappers
{
    public static class ArtistMapper
    {
        public static Artist toArtist(ArtistCreateRequestDto request, string userName)
        {
            var artist = new Artist
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.UtcNow,
                UserName = userName,
                Name = request.Name,
                ProviderId = request.ProviderId
            };

            return artist;
        }
    }
}
