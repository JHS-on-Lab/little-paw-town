using System.Collections.Generic;
using UnityEngine;

namespace LittlePawTown.Data
{
    /// <summary>
    /// 이벤트 결과 세트 마스터 데이터.
    /// EventTemplateData 에서 참조됨.
    /// </summary>
    [CreateAssetMenu(fileName = "NewEventReward", menuName = "LittlePawTown/Master/EventReward")]
    public class EventRewardData : MasterDataBase
    {
        [Header("Affection / Currency")]
        public int affectionPoint;
        public int moneyDelta;

        [Header("Item Rewards")]
        public List<string> itemRewardIds = new();

        [Header("Memory Card")]
        [Tooltip("Leave empty for no memory card.")]
        public string memoryCardTemplateId;

        [Header("Unlock Flags")]
        public List<string> unlockFlags = new();

        [Header("NPC Relationship Deltas")]
        public List<RelationshipDelta> relationshipDeltas = new();

        [System.Serializable]
        public struct RelationshipDelta
        {
            public string npcId;
            public int    delta;
        }
    }
}
