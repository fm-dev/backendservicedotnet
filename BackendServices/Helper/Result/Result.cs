namespace BackendServices.Helper.Result
{
    public class Result<T> : ResultBase
    {
        public T? Data { get; set; }
    }
}
