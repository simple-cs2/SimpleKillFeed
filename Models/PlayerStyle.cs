// Models/PlayerStyle.cs
namespace SimpleKillFeed;

/// <summary>
/// Represents a player's custom kill feed style preferences.
/// Stored in skf_player_styles table.
/// </summary>
public class PlayerStyle
{
	public ulong SteamID            { get; set; }
	public bool AlwaysHeadshot      { get; set; } = false;
	public bool AlwaysWallbang      { get; set; } = false;
	public bool AlwaysNoscope       { get; set; } = false;
	public bool AlwaysSmoke         { get; set; } = false;
	public bool AlwaysBlind         { get; set; } = false;
	public bool AlwaysAir			{ get; set; } = false;
}