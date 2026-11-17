namespace MyMusicSpace.Contracts.Exceptions
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
