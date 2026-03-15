// Database/Database.cs
using MySqlConnector;
using Dapper;

namespace SimpleKillFeed;

public class Database
{
    private readonly string _connectionString;
    public PlayerStyleRepository Styles { get; }

    public Database(string connectionString)
    {
        _connectionString = connectionString;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        Styles = new PlayerStyleRepository(connectionString);
    }

    public async Task InitializeAsync()
    {
        using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        await connection.ExecuteAsync(@"
            CREATE TABLE IF NOT EXISTS skf_player_styles (
                steam_id        BIGINT UNSIGNED NOT NULL PRIMARY KEY,
                always_headshot TINYINT(1)      NOT NULL DEFAULT 0,
                always_wallbang TINYINT(1)      NOT NULL DEFAULT 0,
                always_noscope  TINYINT(1)      NOT NULL DEFAULT 0,
                always_smoke    TINYINT(1)      NOT NULL DEFAULT 0,
                always_blind    TINYINT(1)      NOT NULL DEFAULT 0,
                always_air      TINYINT(1)      NOT NULL DEFAULT 0
            );
        ");
    }
}