// SimpleKillFeed.cs
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Admin;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;

namespace SimpleKillFeed;

public class SimpleKillFeed : BasePlugin, IPluginConfig<KillFeedConfig>
{
	public override string ModuleName => "SimpleKillFeed";
	public override string ModuleVersion => "0.1.0";
	public override string ModuleAuthor  => "t.me/kotyarakryt";
    public override string ModuleDescription => "A lightweight CS2 plugin that lets players customize their kill feed icons — headshot, wallbang, noscope, smoke, blind and more.";

	public KillFeedConfig Config { get; set; } = new();
	private Database _database = null!;

	// In-memory cache - loaded on connect, saved on change
	private Dictionary<ulong, PlayerStyle> _stylesCache = new();

	public override void Load(bool hotReload)
	{
		// Database -->>
		string connString = $"Server={Config.Host};Port={Config.Port};Database={Config.Database};User={Config.Username};Password={Config.Password};";
		_database = new Database(connString);
		_ = _database.InitializeAsync();


		// Events -->>
		RegisterEventHandler<EventPlayerConnectFull>((@event, info) =>
		{
			_ = OnPlayerConnect(@event, info);
			return HookResult.Continue;
		});

		RegisterEventHandler<EventPlayerDisconnect>((@event, info) =>
		{
			var player = @event.Userid;
			if(player != null)
				_stylesCache.Remove(player.SteamID);
			return HookResult.Continue;
		});

		// HookMode.Pre — modify event BEFORE it's sent to clients
		RegisterEventHandler<EventPlayerDeath>(OnPlayerDeath, HookMode.Pre);


		// Commands -->>
		AddCommand("css_killfeed", "Customize your kill feed", OnKillFeedCommand);
	}

	public void OnConfigParsed(KillFeedConfig config)
	{
		Config = config;
	}

	private async Task OnPlayerConnect(EventPlayerConnectFull @event, GameEventInfo info)
	{
		var player = @event.Userid;
		if(player == null || !player.IsValid) return;

		var style = await _database.Styles.GetAsync(player.SteamID);

		Server.NextFrame(() =>
		{
			if(!player.IsValid) return;

			// Load existing style or create default
			_stylesCache[player.SteamID] = style ?? new PlayerStyle { SteamID = player.SteamID };
		});
	}

	private HookResult OnPlayerDeath(EventPlayerDeath @event, GameEventInfo info)
	{
		var killer = @event.Attacker;
		if(killer == null || !killer.IsValid) return HookResult.Continue;

		if(!_stylesCache.TryGetValue(killer.SteamID, out var style))
			return HookResult.Continue;

		// Apply player's custom style
		if(style.AlwaysHeadshot && HasPermission(killer, Config.HeadshotPermission))	@event.Headshot        	= true;
		if(style.AlwaysWallbang && HasPermission(killer, Config.WallbangPermission)) 	@event.Penetrated      	= 1;
		if(style.AlwaysNoscope && HasPermission(killer, Config.NoscopePermission))  	@event.Noscope         	= true;
		if(style.AlwaysSmoke && HasPermission(killer, Config.SmokePermission))    		@event.Thrusmoke       	= true;
		if(style.AlwaysBlind && HasPermission(killer, Config.BlindPermission))    		@event.Attackerblind   	= true;
		if(style.AlwaysAir && HasPermission(killer, Config.AirPermission))		 		@event.Attackerinair	= true;

		return HookResult.Continue;
	}

	private void OnKillFeedCommand(CCSPlayerController? player, CommandInfo command)
	{
		if(player == null) return;

		if(!_stylesCache.TryGetValue(player.SteamID, out var style))
		{
			style = new PlayerStyle { SteamID = player.SteamID };
			_stylesCache[player.SteamID] = style;
		}

		string arg = command.GetArg(1).ToLower();

		switch(arg)
		{
			case "headshot":
				if(!HasPermission(player, Config.HeadshotPermission))
				{
					player.PrintToChat($" {ChatColors.Red}[SKF] {ChatColors.Default}You don't have permission to use this!");
					return;
				}
				style.AlwaysHeadshot = !style.AlwaysHeadshot;
				player.PrintToChat($" {ChatColors.Red}[SKF] {ChatColors.Default}Headshot: {Format(style.AlwaysHeadshot)}");
				break;

			case "wallbang":
				if(!HasPermission(player, Config.WallbangPermission))
				{
					player.PrintToChat($" {ChatColors.Red}[SKF] {ChatColors.Default}You don't have permission to use this!");
					return;
				}
				style.AlwaysWallbang = !style.AlwaysWallbang;
				player.PrintToChat($" {ChatColors.Red}[SKF] {ChatColors.Default}Wallbang: {Format(style.AlwaysWallbang)}");
				break;

			case "noscope":
				if(!HasPermission(player, Config.NoscopePermission))
				{
					player.PrintToChat($" {ChatColors.Red}[SKF] {ChatColors.Default}You don't have permission to use this!");
					return;
				}
				style.AlwaysNoscope = !style.AlwaysNoscope;
				player.PrintToChat($" {ChatColors.Red}[SKF] {ChatColors.Default}Noscope: {Format(style.AlwaysNoscope)}");
				break;

			case "smoke":
				if(!HasPermission(player, Config.SmokePermission))
				{
					player.PrintToChat($" {ChatColors.Red}[SKF] {ChatColors.Default}You don't have permission to use this!");
					return;
				}
				style.AlwaysSmoke = !style.AlwaysSmoke;
				player.PrintToChat($" {ChatColors.Red}[SKF] {ChatColors.Default}Through Smoke: {Format(style.AlwaysSmoke)}");
				break;

			case "blind":
				if(!HasPermission(player, Config.BlindPermission))
				{
					player.PrintToChat($" {ChatColors.Red}[SKF] {ChatColors.Default}You don't have permission to use this!");
					return;
				}
				style.AlwaysBlind = !style.AlwaysBlind;
				player.PrintToChat($" {ChatColors.Red}[SKF] {ChatColors.Default}Blind: {Format(style.AlwaysBlind)}");
				break;

			case "air":
				if(!HasPermission(player, Config.AirPermission))
				{
					player.PrintToChat($" {ChatColors.Red}[SKF] {ChatColors.Default}You don't have permission to use this!");
					return;
				}
				style.AlwaysAir = !style.AlwaysAir;
				player.PrintToChat($" {ChatColors.Red}[SKF] {ChatColors.Default}Air: {Format(style.AlwaysAir)}");
				break;


			case "reset":
				style = new PlayerStyle { SteamID = player.SteamID };
				_stylesCache[player.SteamID] = style;
				_ = _database.Styles.DeleteAsync(player.SteamID);
				player.PrintToChat($" {ChatColors.Red}[SKF] {ChatColors.Default}Your kill feed style has been reset!");
				return;

			default:
				// Show current settings
				player.PrintToChat($" {ChatColors.Red}[SKF] {ChatColors.Default}Your current style:");
				if(HasPermission(player, Config.HeadshotPermission))
					player.PrintToChat($" {ChatColors.Grey}  !killfeed headshot {ChatColors.Default}— Headshot: {Format(style.AlwaysHeadshot)}");
					
				if(HasPermission(player, Config.WallbangPermission))
					player.PrintToChat($" {ChatColors.Grey}  !killfeed wallbang {ChatColors.Default}— Wallbang: {Format(style.AlwaysWallbang)}");
				
				if(HasPermission(player, Config.NoscopePermission))
					player.PrintToChat($" {ChatColors.Grey}  !killfeed noscope  {ChatColors.Default}— Noscope:  {Format(style.AlwaysNoscope)}");
				
				if(HasPermission(player, Config.SmokePermission))
					player.PrintToChat($" {ChatColors.Grey}  !killfeed smoke    {ChatColors.Default}— Smoke:    {Format(style.AlwaysSmoke)}");
				
				if(HasPermission(player, Config.BlindPermission))
					player.PrintToChat($" {ChatColors.Grey}  !killfeed blind    {ChatColors.Default}— Blind:    {Format(style.AlwaysBlind)}");
				
				if(HasPermission(player, Config.AirPermission))
					player.PrintToChat($" {ChatColors.Grey}  !killfeed air      {ChatColors.Default}— Air:      {Format(style.AlwaysAir)}");

				player.PrintToChat($" {ChatColors.Grey}  !killfeed reset    {ChatColors.Default}— Reset all");
				return;
		}

		// Save to database
		_ = _database.Styles.SaveAsync(style);
	}

	private static string Format(bool value) =>
		value
			? $"{ChatColors.Lime}✔ ON"
			: $"{ChatColors.Red}✘ OFF";

	private bool HasPermission(CCSPlayerController player, string permission)
	{
		if(string.IsNullOrEmpty(permission)) return true;
		return AdminManager.PlayerHasPermissions(player, permission);
	}
}
