namespace MyMusicSpace.Contracts.Exceptions
{
    public class ProviderMusicException : Exception
    {
        public ProviderMusicException(string message)
            : base(message)
        {
        }

        public ProviderMusicException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
