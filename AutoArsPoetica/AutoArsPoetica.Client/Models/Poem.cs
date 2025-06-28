namespace AutoArsPoetica.Client.Models;

public class Poem
{
    public string Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public long Epoch { get; set; }
}