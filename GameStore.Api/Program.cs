using GameStore.Api.Dtos;

namespace GameStore.Api;

public class Program
{
    public static void Main(string[] args)
    {
        const string GetGameEndPointName = "GetGame";

        List<GameDto> games = [
            new (
                1,
                "Street Figther II",
                "Fighting",
                19.99M,
                new DateOnly(1992, 7, 15)
            ),
            new (
                2,
                "Final Fantasy XIV",
                "Roleplaying",
                59.99M,
                new DateOnly(2010, 9, 30)
            ),
            new (
                3,
                "FIFA 23",
                "Sports",
                69.99M,
                new DateOnly(2022, 9, 27)

            )
        ];

        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        // app.MapGet("/", () => "Hello World!");

        // GET /games
        app.MapGet("/games", () => games);

        // GET /games/{id}
        app.MapGet("/games/{id}",
                    (int id) => games.Find((game) => game.Id == id))
                    .WithName(GetGameEndPointName);

        // POST /games
        app.MapPost("/games", (CreateGameDto newGame) => {
            GameDto game = new (
                games.Count + 1,
                newGame.Name,
                newGame.Genre,
                newGame.Price,
                newGame.ReleaseDate
            );

            games.Add(game);
            return Results.CreatedAtRoute(GetGameEndPointName,
                    new {id = game.Id}, game);
        });

        app.Run();
    }
}
