namespace Library.Communication.Results
{
    public record ValidationError : Error
    {
        public List<Error> Errors;

        public ValidationError(List<Error> errors) : base(ErrorType.Validation)
        {
            Errors = errors;
        }
    }
}