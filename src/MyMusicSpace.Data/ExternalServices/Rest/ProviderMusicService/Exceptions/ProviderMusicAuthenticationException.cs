namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Exceptions
{
    public class ProviderMusicAuthenticationException : ProviderMusicException
    {
        public ProviderMusicAuthenticationException(string message)
            : base(message)
        {
        }

        public ProviderMusicAuthenticationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
