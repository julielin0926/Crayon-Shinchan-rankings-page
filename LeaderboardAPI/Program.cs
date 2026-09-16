using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    "Server=localhost;Database=HTMLRankPractice;Integrated Security=True;TrustServerCertificate=True;";

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLiveServer", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowLiveServer");

app.MapGet("/api/test", () => "API OK");

app.MapGet("/api/rankings", () =>
{
    using var connection = new SqlConnection(connectionString);

    connection.Open();

    var command = new SqlCommand("""
        SELECT
            ROW_NUMBER() OVER (
                ORDER BY Level DESC
            ) AS Ranking,
            PlayerName,
            Academy,
            Level
        FROM Player;
        """, connection);

    using var reader = command.ExecuteReader();

    var rankings = new List<object>();

    while (reader.Read())
    {
        rankings.Add(new
        {
            Ranking = reader["Ranking"],
            PlayerName = reader["PlayerName"],
            Academy = reader["Academy"],
            Level = reader["Level"]
        });
    }

    return rankings;
});

app.Run();