using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace AllMapsOnline
{
    // The host never picks maps on LevelSelection.multiplayerBlackList online, even if they're
    // ticked in the map list: Desert 2, 7, 8 (scenes 6, 11, 12), Factory 5 (17) and Winter 13 (69).
    // The list is only checked here when picking the next map; clients load whatever the host
    // sends, so emptying it lets the host play them online. Only the host needs the mod.
    // Same fix as CompetitiveMod's AllowAllMapsOnline; running both is harmless.
    [BepInPlugin(Guid, Name, Version)]
    public class Plugin : BaseUnityPlugin
    {
        public const string Guid = "allmapsonline.stickfight";
        public const string Name = "AllMapsOnline";
        public const string Version = "1.0.0";

        internal static ManualLogSource Log;
        internal static ConfigEntry<bool> Enabled;

        private void Awake()
        {
            Log = Logger;
            Enabled = Config.Bind("General", "Enabled", true,
                "Let the host play the local-only maps online when they're ticked in the map list.");
            new Harmony(Guid).PatchAll(typeof(Plugin).Assembly);
            Logger.LogInfo($"{Name} {Version} loaded (Enabled={Enabled.Value})");
        }
    }

    [HarmonyPatch(typeof(LevelSelection), nameof(LevelSelection.GetNextLevel))]
    internal static class OnlineMapsPatch
    {
        private static void Prefix(LevelSelection __instance)
        {
            if (Plugin.Enabled.Value && __instance.multiplayerBlackList != null && __instance.multiplayerBlackList.Length > 0)
            {
                Plugin.Log.LogInfo($"Allowing local-only maps online (scenes {string.Join(", ", System.Array.ConvertAll(__instance.multiplayerBlackList, i => i.ToString()))}).");
                __instance.multiplayerBlackList = new int[0];
            }
        }
    }
}
