namespace FitLife.Shared.Infrastucture.Response
{
    public interface IBaseResponse
    {
        bool Success { get; set; }
        string[] Errors { get; set; }
    }
}
