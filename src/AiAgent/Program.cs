using DotNetEnv;
using Google.GenAI;
using Google.GenAI.Types;

Env.TraversePath().Load();

var apiKey = System.Environment.GetEnvironmentVariable("GEMINI_API_KEY");

if (string.IsNullOrEmpty(apiKey))
{
    throw new InvalidOperationException("API Key of the LLM is not founded");
}

var verbose = args.Contains("--verbose");
var userPrompt = args.FirstOrDefault(arg => arg != "--verbose");

if (userPrompt is null)
{
    Console.Error.WriteLine("usage: ai-agent [--verbose] <user_prompt>");
    return 2;
}

using var client = new Client(apiKey: apiKey);

var messages = new List<Content>
{
    new() { Role = "user", Parts = [new Part { Text = userPrompt }] },
};

var response = await client.Models.GenerateContentAsync(
    model: "gemini-2.5-flash",
    contents: messages);

if (verbose)
{
    if (response.UsageMetadata is null)
    {
        throw new InvalidOperationException("Failed to get the metadata");
    }

    Console.WriteLine($"User prompt: {userPrompt}");
    Console.WriteLine($"Prompt tokens: {response.UsageMetadata.PromptTokenCount}");
    Console.WriteLine($"Response tokens: {response.UsageMetadata.CandidatesTokenCount}");
}

Console.WriteLine(response.Text);

return 0;
