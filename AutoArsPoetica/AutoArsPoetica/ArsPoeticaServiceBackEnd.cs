using System.ClientModel;
using OpenAI.Chat;

internal class ArsPoeticaServiceBackEnd : IArsPoeticaService
{
    private readonly ChatClient _chatClient;
    public ArsPoeticaServiceBackEnd(ChatClient chatClient)
    {
        _chatClient = chatClient;
    }
    public async Task<string> GeneratePoemAsync()
    {
        try
        {
            var prompt = @"Create a poem inspired by this song those are the lyrics
 'Make love on the net 
When I expose my love, anyone online can see it 
And I can see theirs
I am no longer ashamed
I share
I share all that is mine on the net
All that is me on the net'
The poem should capture the essence and emotions of the song while being original. keep the first line of the poem as is, and make the rest of the poem a continuation of the first line make it 10 lines exactly  
return only the poem, no other text.";

            ClientResult<ChatCompletion> poem = await _chatClient.CompleteChatAsync(prompt);
            return poem.Value.Content.First().Text;
        }
        catch (Exception ex)
        {
            throw new Exception("Error generating poem", ex);
        }
    }
}
