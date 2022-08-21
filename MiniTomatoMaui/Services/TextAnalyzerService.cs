using Azure;
using Azure.AI.TextAnalytics;
namespace MiniTomatoMaui.Services;

public class TextAnalyzerService
{
    private TextAnalyticsClient SentimentClient { get; set; }

    public TextAnalyzerService()
    {
        var endpoint = "https://moodanalyzer.cognitiveservices.azure.com/";
        var apiKey = "e26a9d4c01a64468bf1600f5cb104a12";

        //AnalyzeSentimentOptions options = new AnalyzeSentimentOptions()
        //{
        //    IncludeStatistics = true,
        //    IncludeOpinionMining = true
        //};

        SentimentClient = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(apiKey));
    }

    public async Task<DocumentSentiment> AnalyzeText(string text)
    {
        var response = await SentimentClient.AnalyzeSentimentAsync(text);
        return response.Value;
    }
}