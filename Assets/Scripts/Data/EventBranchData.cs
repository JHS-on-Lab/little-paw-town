using UnityEngine;

namespace LittlePawTown.Data
{
    public enum BranchConditionType
    {
        Species,
        Trait,
        AffectionLevel,
        Relationship,
        FirstTime,
        Choice,
    }

    public enum BranchOperator
    {
        Eq,
        Gte,
        Lte,
        Contains,
    }

    /// <summary>
    /// 이벤트 조건 분기 마스터 데이터.
    /// EventTemplateData.branches 리스트에서 참조됨.
    /// 우선순위(priority) 높은 것부터 조건 평가 → 최초 매칭 분기 적용.
    /// </summary>
    [CreateAssetMenu(fileName = "NewEventBranch", menuName = "LittlePawTown/Master/EventBranch")]
    public class EventBranchData : MasterDataBase
    {
        [Header("소속 이벤트")]
        public string eventId;

        [Header("조건")]
        public BranchConditionType conditionType;
        [Tooltip("조건 키. 예: trait=curiosity, species=dog, npc=npc_baker_mina")]
        public string conditionKey;
        public BranchOperator @operator;
        [Tooltip("조건 비교값. 예: dog / 3 / 1")]
        public string conditionValue;

        [Header("연출")]
        [Tooltip("DialogueTextData 의 text_key.")]
        public string actionTextKey;
        public string animationKey;

        [Header("우선순위")]
        [Tooltip("높을수록 먼저 평가됨.")]
        public int priority;
    }
}
