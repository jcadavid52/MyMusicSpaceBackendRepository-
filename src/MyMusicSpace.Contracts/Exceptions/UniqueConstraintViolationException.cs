namespace MyMusicSpace.Contracts.Exceptions
{
    public sealed class UniqueConstraintViolationException : Exception
    {
        public UniqueConstraintViolationException(
            string? constraintName,
            string message,
            Exception innerException)
            : base(message, innerException)
        {
            ConstraintName = constraintName;
        }

        public string? ConstraintName { get; }
    }
}
