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
        [Header("애정도 / 재화")]
        public int affectionPoint;
        public int moneyDelta;

        [Header("아이템 보상")]
        public List<string> itemRewardIds = new();

        [Header("추억 카드")]
        [Tooltip("비어 있으면 추억 카드 미생성.")]
        public string memoryCardTemplateId;

        [Header("해금 플래그")]
        public List<string> unlockFlags = new();

        [Header("NPC 친밀도 변화")]
        public List<RelationshipDelta> relationshipDeltas = new();

        [System.Serializable]
        public struct RelationshipDelta
        {
            public string npcId;
            public int    delta;
        }
    }
}
