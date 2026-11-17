using MyMusicSpace.Business.Services.Dtos;
using MyMusicSpace.Data.Persistence.Models;

namespace MyMusicSpace.Business.Mappers
{
    public static class AlbumMapper
    {
        public static Album ToAlbum(AlbumCreateRequestDto request, string userName)
        {
            var album = new Album
            {
                Id = Guid.NewGuid(),
                CreationDate = DateTime.UtcNow,
                UserName = userName,
                ImageUrl = request.ImageUrl ?? string.Empty,
                Name = request.Name,
                ProviderId = request.ProviderId
            };

            return album;
        }
    }
}
