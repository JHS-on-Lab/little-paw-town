namespace LittlePawTown.Utils
{
    public static class Constants
    {
        // ── Save ──────────────────────────────────────────────
        public const string SaveFileName       = "save.json";
        public const string SettingsFileName   = "settings.json";

        // ── Scene Names ───────────────────────────────────────
        public static class Scenes
        {
            public const string Boot        = "Boot";
            public const string Title       = "Title";
            public const string PetCreation = "PetCreation";
            public const string Game        = "Game";
        }

        // ── Species ───────────────────────────────────────────
        public static class Species
        {
            public const string Dog = "dog";
            public const string Cat = "cat";
        }

        // ── Locations ─────────────────────────────────────────
        public static class Locations
        {
            public const string Home    = "home";
            public const string Park    = "park";
            public const string Bakery  = "bakery";
            public const string Plaza   = "plaza";
        }

        // ── Time Tags ─────────────────────────────────────────
        public static class TimeTags
        {
            public const string Morning = "morning";   // 06:00 ~ 11:59
            public const string Noon    = "noon";      // 12:00 ~ 17:59
            public const string Evening = "evening";   // 18:00 ~ 20:59
            public const string Night   = "night";     // 21:00 ~ 05:59
        }

        // ── Weather Tags ──────────────────────────────────────
        public static class WeatherTags
        {
            public const string Sunny = "sunny";
            public const string Rainy = "rainy";
        }

        // ── Affection ─────────────────────────────────────────
        public static class Affection
        {
            public const int MaxLevel = 4;

            // 각 단계 진입에 필요한 누적 포인트 (AffectionLevelData 와 동일 값)
            public const int Level0Point = 0;
            public const int Level1Point = 20;
            public const int Level2Point = 50;
            public const int Level3Point = 100;
            public const int Level4Point = 180;
        }

        // ── Event Presentation Types ──────────────────────────
        public static class PresentationType
        {
            public const string Ambient  = "ambient";
            public const string Scene    = "scene";
            public const string Push     = "push";
        }

        // ── Event Entry Points ────────────────────────────────
        public static class EntryPoint
        {
            public const string HomeAuto       = "home_auto";
            public const string HomeSuggested  = "home_suggested";
            public const string LocationDirect = "location_direct";
            public const string Push           = "push";
        }
    }
}
