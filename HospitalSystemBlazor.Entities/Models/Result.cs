namespace HospitalSystemBlazor.Entities.Models
{
    public class Result<T>
    {

        public bool isSucces { get; set; }
        public T Value { get; set; }
        public string Error { get; set; }
        public Result(bool isSucces, T value, string error)
        {
            this.isSucces = isSucces;
            Value = value;
            Error = error;
        }

        public static Result<T> Succes(T value)
        {
            return new Result<T>(true, value, null);
        }
        public static Result<T> Failure(string error)
        {
            return new Result<T>(false, default, error);
        }
    }
}
