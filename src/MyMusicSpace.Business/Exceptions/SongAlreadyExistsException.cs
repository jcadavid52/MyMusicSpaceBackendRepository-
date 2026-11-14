namespace MyMusicSpace.Business.Exceptions
{
    public sealed class SongAlreadyExistsException : Exception
    {
        public SongAlreadyExistsException(string providerId, Exception? innerException = null)
            : base($"Ya existe una canción con ProviderId '{providerId}'.", innerException)
        {
        }
    }
}
