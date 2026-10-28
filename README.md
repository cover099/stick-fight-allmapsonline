# AllMapsOnline

A [BepInEx 5](https://github.com/BepInEx/BepInEx) plugin for **Stick Fight: The Game** that lets you play the local-only maps online.

Online, the game never picks five maps, even when they're ticked in the map list: Desert 2, 7 and 8, Factory 5 and Winter 13. This mod lets the host play them like any other map. Only the **host** needs it, since the other players load whatever map the host picks. Works on Windows and Linux (Proton).

It can be switched off with `Enabled` in `BepInEx/config/allmapsonline.stickfight.cfg`.

## Install

### Easy install (Windows)

1. Download `AllMapsOnline-windows-installer.zip` from the [latest release](https://github.com/cover099/stick-fight-allmapsonline/releases/latest) and extract it anywhere.
2. Close the game, then double-click `Install.bat`.

It finds the game through Steam (or asks for the folder), installs BepInEx 5 if it isn't there yet, and puts the mod in place. If Windows shows "Windows protected your PC", click **More info** → **Run anyway**.

### Manual install

1. Install [BepInEx 5](https://github.com/BepInEx/BepInEx/releases) (`BepInEx_win_x64`) into the game folder.
2. Copy `AllMapsOnline.dll` into `BepInEx/plugins`.

On Linux, set the game's Steam launch options to `WINEDLLOVERRIDES="winhttp=n,b" %command%` so BepInEx loads under Proton.

## Build

Requires the .NET SDK and a copy of the game with BepInEx 5 installed (none of the game files are included):

```sh
dotnet build -c Release -p:GameDir=/path/to/StickFightTheGame/
```

The built DLL is copied into that install's `BepInEx/plugins` folder.
