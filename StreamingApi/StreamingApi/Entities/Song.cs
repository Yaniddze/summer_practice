namespace StreamingApi.Entities
{
    public class Song: Entity
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public byte[] Content { get; set; }
    }
}