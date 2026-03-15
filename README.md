# 🎯 SKF — SimpleKillFeed

[![GitHub release](https://img.shields.io/github/v/release/simple-cs2/SimpleKillFeed)](https://github.com/simple-cs2/SimpleKillFeed/releases)
[![GitHub stars](https://img.shields.io/github/stars/simple-cs2/SimpleKillFeed)](https://github.com/simple-cs2/SimpleKillFeed/stargazers)
[![GitHub issues](https://img.shields.io/github/issues/simple-cs2/SimpleKillFeed)](https://github.com/simple-cs2/SimpleKillFeed/issues)
[![License](https://img.shields.io/github/license/simple-cs2/SimpleKillFeed)](LICENSE)
[![CounterStrikeSharp](https://img.shields.io/badge/CounterStrikeSharp-1.0.363+-blue)](https://github.com/roflmuffin/CounterStrikeSharp)

> A lightweight CS2 plugin that lets players customize their kill feed icons — headshot, wallbang, noscope, smoke, blind and more.

---

## 📦 Dependencies

- [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp) `v1.0.363+`
- MySQL `5.7+` or MariaDB `10.3+`

---

## ⚙️ Installation

1. Download the latest release from [Releases](https://github.com/simple-cs2/SimpleKillFeed/releases)
2. Extract `SimpleKillFeed` folder to:
```
   csgo/addons/counterstrikesharp/plugins/
```
3. Configure `SimpleKillFeed.json` (see Configuration)
4. Restart your server

---

## 🔧 Configuration

`csgo/addons/counterstrikesharp/configs/plugins/SimpleKillFeed/SimpleKillFeed.json`
```json
{
  "Host": "127.0.0.1",
  "Port": 3306,
  "Database": "skf",
  "Username": "root",
  "Password": "",
  "HeadshotPermission": "",
  "WallbangPermission": "",
  "NoscopePermission":  "",
  "SmokePermission":    "",
  "BlindPermission":    "",
  "AirPermission":      ""
}
```

| Field | Description |
|---|---|
| `Host` | MySQL server host |
| `Port` | MySQL server port |
| `Database` | Database name |
| `Username` | MySQL username |
| `Password` | MySQL password |
| `HeadshotPermission` | Required permission. Empty = everyone |
| `WallbangPermission` | Required permission. Empty = everyone |
| `NoscopePermission` | Required permission. Empty = everyone |
| `SmokePermission` | Required permission. Empty = everyone |
| `BlindPermission` | Required permission. Empty = everyone |
| `AirPermission` | Required permission. Empty = everyone |

### 💡 Permission examples
```json
"HeadshotPermission": ""            // everyone
"WallbangPermission": "@css/vip"    // VIP only
"NoscopePermission":  "@css/admin"  // admins only
```

---

## 📋 Commands

| Command | Description |
|---|---|
| `!killfeed` | Show your current style |
| `!killfeed headshot` | Toggle headshot icon |
| `!killfeed wallbang` | Toggle wallbang icon |
| `!killfeed noscope` | Toggle noscope icon |
| `!killfeed smoke` | Toggle through smoke icon |
| `!killfeed blind` | Toggle blind icon |
| `!killfeed air` | Toggle in-air icon |
| `!killfeed reset` | Reset all to default |

---

## 🎮 How it works

Each player can independently choose which kill feed icons to always display on their kills — regardless of how the kill was actually made.

For example, if a player enables **headshot** — every kill they make will show the headshot icon in the kill feed, even if it wasn't a headshot.

Settings are saved to MySQL and persist between sessions.

---

## 📸 Screenshots

> Coming soon!

---

## 👤 Author

Made with ❤️ by [kotyarakryt](https://t.me/kotyarakryt)