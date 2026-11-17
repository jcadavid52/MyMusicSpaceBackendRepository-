namespace MyMusicSpace.Contracts.Exceptions
{
    public class ProviderMusicSongNotFoundException : ProviderMusicException
    {
        public ProviderMusicSongNotFoundException(string providerId)
            : base($"Song with provider id '{providerId}' was not found.")
        {
            ProviderId = providerId;
        }

        public string ProviderId { get; }
    }
}
