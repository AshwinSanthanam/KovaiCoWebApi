namespace KC.Base.Validators
{
    public interface IValidator<T>
    {
        void Validate<T>(T transientEntity);
    }
}
