namespace LittlePawTown.Core
{
    /// <summary>
    /// 게임 전역에서 사용하는 EventBus 이벤트 데이터 구조체 모음.
    /// 새 이벤트 추가 시 이 파일에 append.
    /// </summary>
    public static class Events
    {
        // ── UI / 씬 ───────────────────────────────────────────
        public struct FadeRequestEvent
        {
            public bool  FadeIn;
            public float Duration;
        }

        // ── 게임 상태 ──────────────────────────────────────────
        public struct GameStateChangedEvent
        {
            public GameState Prev;
            public GameState Next;
        }

        // ── 반려동물 ───────────────────────────────────────────
        public struct PetCreatedEvent
        {
            public string PetId;
            public string PetName;
            public string SpeciesId;
        }

        // ── 애정도 ─────────────────────────────────────────────
        public struct AffectionChangedEvent
        {
            public int Delta;
            public int TotalPoint;
            public int NewLevel;
            public bool LeveledUp;
        }

        // ── 이벤트 시스템 ──────────────────────────────────────
        public struct GameEventStartedEvent
        {
            public string EventId;
        }

        public struct GameEventCompletedEvent
        {
            public string EventId;
            public string BranchId;
            public string ChoiceKey;
        }

        // ── 추억 카드 ──────────────────────────────────────────
        public struct MemoryCardAcquiredEvent
        {
            public string MemoryId;
            public string Title;
        }

        // ── 장소 ──────────────────────────────────────────────
        public struct LocationChangedEvent
        {
            public string LocationId;
        }

        // ── 경제 ──────────────────────────────────────────────
        public struct MoneyChangedEvent
        {
            public int Delta;
            public int Total;
        }
    }
}
