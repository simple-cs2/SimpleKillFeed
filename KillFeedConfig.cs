// KillFeedConfig.cs
using System.Text.Json.Serialization;
using CounterStrikeSharp.API.Core;

namespace SimpleKillFeed;

/// <summary>
/// Plugin configuration — auto-generated at:
/// configs/plugins/SimpleKillFeed/SimpleKillFeed.json
/// </summary>
public class KillFeedConfig : BasePluginConfig
{
	[JsonPropertyName("Host")]
	public string Host { get; set; } = "127.0.0.1";

	[JsonPropertyName("Port")]
	public int Port { get; set; } = 3306;

	[JsonPropertyName("Database")]
	public string Database { get; set; } = "skf";

	[JsonPropertyName("Username")]
	public string Username { get; set; } = "root";

	[JsonPropertyName("Password")]
	public string Password { get; set; } = "";


	[JsonPropertyName("HeadshotPermission")]
	public string HeadshotPermission { get; set; } = "";

	[JsonPropertyName("WallbangPermission")]
	public string WallbangPermission { get; set; } = "";

	[JsonPropertyName("NoscopePermission")]
	public string NoscopePermission { get; set; } = "";

	[JsonPropertyName("SmokePermission")]
	public string SmokePermission { get; set; } = "";

	[JsonPropertyName("BlindPermission")]
	public string BlindPermission { get; set; } = "";

	[JsonPropertyName("AirPermission")]
	public string AirPermission { get; set; } = "";
}