using ScrabbleCodingChallenge.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ScrabbleCodingChallenge.Services.Interfaces;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddSingleton<IScrabbleGameService, ScrabbleGameService>();
    })
    .Build();
var scrabbleGameService = host.Services.GetRequiredService<IScrabbleGameService>();

scrabbleGameService.RunGame();
