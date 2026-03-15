// Database/PlayerStyleRepository.cs
using Dapper;
using MySqlConnector;

namespace SimpleKillFeed;

public class PlayerStyleRepository
{
    private readonly string _connectionString;
    private MySqlConnection CreateConnection() => new(_connectionString);

    public PlayerStyleRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    /// <summary>Returns player style, or null if not found.</summary>
    public async Task<PlayerStyle?> GetAsync(ulong steamId)
    {
        using var connection = CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<PlayerStyle>(@"
            SELECT * FROM skf_player_styles
            WHERE steam_id = @SteamId",
            new { SteamId = steamId }
        );
    }

    /// <summary>Saves or updates player style.</summary>
    public async Task SaveAsync(PlayerStyle style)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync(@"
            INSERT INTO skf_player_styles (steam_id, always_headshot, always_wallbang, always_noscope, always_smoke, always_blind, always_air)
            VALUES (@SteamID, @AlwaysHeadshot, @AlwaysWallbang, @AlwaysNoscope, @AlwaysSmoke, @AlwaysBlind, @AlwaysAir)
            ON DUPLICATE KEY UPDATE
                always_headshot = @AlwaysHeadshot,
                always_wallbang = @AlwaysWallbang,
                always_noscope  = @AlwaysNoscope,
                always_smoke    = @AlwaysSmoke,
                always_blind    = @AlwaysBlind,
                always_air      = @AlwaysAir",
            style
        );
    }

    /// <summary>Deletes player style (resets to default).</summary>
    public async Task DeleteAsync(ulong steamId)
    {
        using var connection = CreateConnection();
        await connection.ExecuteAsync(@"
            DELETE FROM skf_player_styles
            WHERE steam_id = @SteamId",
            new { SteamId = steamId }
        );
    }
}