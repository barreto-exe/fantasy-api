namespace FantasyApi.Data.Base.Dtos
{
    public class CreationDto<T>
    {
        public string Message { get; set; }
        public T Item { get; set; }
    }
}
