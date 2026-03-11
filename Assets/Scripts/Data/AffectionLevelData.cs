using System.Collections.Generic;
using UnityEngine;

namespace LittlePawTown.Data
{
    /// <summary>
    /// 애정도 단계 마스터 데이터.
    /// level 0~4 각각 asset 으로 생성하거나
    /// AffectionLevelContainer 에 리스트로 묶어 관리.
    /// </summary>
    [CreateAssetMenu(fileName = "NewAffectionLevel", menuName = "LittlePawTown/Master/AffectionLevel")]
    public class AffectionLevelData : MasterDataBase
    {
        [Header("Level Info")]
        public int    level;
        public string nameKo;
        [Tooltip("Cumulative points required to reach this level.")]
        public int    requiredPoint;

        [Header("Unlocked Content")]
        [Tooltip("List of home reaction tags unlocked.")]
        public List<string> unlockReactionTags     = new();
        [Tooltip("List of event group IDs unlocked.")]
        public List<string> unlockEventGroups      = new();
        [Tooltip("List of home behavior tags unlocked.")]
        public List<string> unlockHomeBehaviorTags = new();
    }
}
