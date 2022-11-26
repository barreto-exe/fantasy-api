namespace FantasyApi.Data.Base.Dtos
{
    public class CreationDto<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Item { get; set; }
    }
}
