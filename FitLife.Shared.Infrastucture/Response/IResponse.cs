namespace FitLife.Shared.Infrastucture.Response
{
    public interface IResponse<T> : IBaseResponse
    {
        T Content { get; set; }
    }
}
