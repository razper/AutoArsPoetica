public class Poem
{
    public Poem(string content, DateTime createdAt)
    {
        Id = Guid.NewGuid().ToString();
        Content = content;
        CreatedAt = createdAt;
        Epoch = new DateTimeOffset(CreatedAt).ToUnixTimeSeconds();
    }

    public string Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public long Epoch { get; set; }
}
