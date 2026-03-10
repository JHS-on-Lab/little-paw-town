using UnityEngine;
using LittlePawTown.Data;

namespace LittlePawTown.UI
{
    /// <summary>
    /// 반려동물 생성 화면들 사이에서 공유되는 임시 데이터.
    /// TitleScreen 이 생성하여 각 화면에 주입한다.
    /// </summary>
    public class PetCreationContext
    {
        // ── 외형 (SCR-012) ───────────────────────────────────────
        public string PetName      = "";
        public string SpeciesId    = "dog_01";   // MVP 고정
        public Color  BodyColor    = Color.white;

        public string EarPartId        = "";
        public string EyePartId        = "";
        public string TailPartId       = "";
        public string PatternPartId    = "";
        public string AccessoryPartId  = "";

        // 파츠별 색상 오버라이드 (json 직렬화 전 임시 보관)
        public Color EarColor       = Color.white;
        public Color EyeColor       = Color.white;
        public Color TailColor      = Color.white;
        public Color PatternColor   = Color.white;

        // ── 성향 (SCR-013) ───────────────────────────────────────
        public int Curiosity   = 2;   // 1~3
        public int Activity    = 2;
        public int Sociability = 2;
        public int Appetite    = 2;
        public int Caution     = 2;

        // ── 유틸 ─────────────────────────────────────────────────

        /// <summary>외형 데이터를 SaveData 형식으로 변환.</summary>
        public PetAppearanceSaveData ToAppearanceSaveData()
        {
            return new PetAppearanceSaveData
            {
                baseColor       = "#" + ColorUtility.ToHtmlStringRGB(BodyColor),
                earPartId       = EarPartId,
                eyePartId       = EyePartId,
                tailPartId      = TailPartId,
                patternPartId   = PatternPartId,
                accessoryPartId = AccessoryPartId,
            };
        }

        /// <summary>성향 데이터를 SaveData 형식으로 변환.</summary>
        public PetTraitSaveData ToTraitSaveData()
        {
            return new PetTraitSaveData
            {
                curiosity   = Curiosity,
                activity    = Activity,
                sociability = Sociability,
                appetite    = Appetite,
                caution     = Caution,
            };
        }
    }
}
