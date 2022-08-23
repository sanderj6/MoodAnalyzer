using Azure;

using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using MiniTomatoMaui.Data;

namespace MiniTomatoMaui.Services;

public class TextAnalyzerService
{
    //private TextAnalyticsClient SentimentClient { get; set; }

    public TextAnalyzerService()
    {
        var endpoint = "https://moodanalyzer.cognitiveservices.azure.com/";
        var apiKey = "e26a9d4c01a64468bf1600f5cb104a12";

        //AnalyzeSentimentOptions options = new AnalyzeSentimentOptions()
        //{
        //    IncludeStatistics = true,
        //    IncludeOpinionMining = true
        //};

        //SentimentClient = new TextAnalyticsClient(new Uri(endpoint), new AzureKeyCredential(apiKey));
    }

    //public async Task<Azure.AI.TextAnalytics.DocumentSentiment> AnalyzeText(string text)
    //{
    //    var response = await SentimentClient.AnalyzeSentimentAsync(text);
    //    return response.Value;
    //}

    public async Task<DocumentSentiment> AnalyzeText(string text)
    {
        try
        {
            // Text Request
            TextAnalyzerRequest textRequest = new()
            {
                documents = new[] {
                    new DocumentRequest() {
                        id = "1",
                        text = text
                    }
                }
            };

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://moodanalyzer.cognitiveservices.azure.com/text/analytics/v3.0/sentiment");
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Serialize Data
            var myContent = JsonConvert.SerializeObject(textRequest);
            StringContent data = new StringContent(myContent, Encoding.UTF8, "application/json");

            // Request
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://moodanalyzer.cognitiveservices.azure.com/text/analytics/v3.0/sentiment"),
                Content = data
            };

            // Headers
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Ocp-Apim-Subscription-Key", "e26a9d4c01a64468bf1600f5cb104a12");

            var response = await httpClient.SendAsync(request);
            var item = response.Content.ReadAsStringAsync();
            var sentiments = Newtonsoft.Json.JsonConvert.DeserializeObject<TextAnalyzerResponse>(item.Result);

            var sentiment = sentiments.documents.FirstOrDefault();

            return sentiment;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Could not send HTTP request: {ex}");

            return null;
        }
    }
}