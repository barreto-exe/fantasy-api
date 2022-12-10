namespace FantasyApi.Data.Base.Requests
{
    public class BaseRequest
    {
        public int? Page { get; set; } = 1;
        public int? Size { get; set; } = 10;
    }
}
