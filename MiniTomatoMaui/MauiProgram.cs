using Microsoft.AspNetCore.Components.WebView.Maui;
using MiniTomatoMaui.Data;
using Microsoft.Azure.Cosmos;
using MiniTomatoMaui.CosmosDB;
using Microsoft.EntityFrameworkCore;
using MiniTomatoMaui.Services;

namespace MiniTomatoMaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        builder.Services
            .AddDbContextFactory<UserRecordDbContext>(options =>
            options.UseCosmos("AccountEndpoint=https://mood-users.documents.azure.com:443/;AccountKey=MXEkdkP6kvzk3FM1CNjLwHHWLQc5Fk06w0g2WkEtaWVRcIgP2PwXkSr4wZEgCvBmkYNXsG3Zh3iq9iMiiOLG0A==;", "mood-user-db")
            );

        builder.Services.AddSingleton<TextAnalyzerService>();


        return builder.Build();
    }
}
