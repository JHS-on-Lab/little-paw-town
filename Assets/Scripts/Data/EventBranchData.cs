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
        [Header("Parent Event")]
        public string eventId;

        [Header("Condition")]
        public BranchConditionType conditionType;
        [Tooltip("Condition key. e.g. trait=curiosity, species=dog, npc=npc_baker_mina")]
        public string conditionKey;
        public BranchOperator @operator;
        [Tooltip("Condition comparison value. e.g. dog / 3 / 1")]
        public string conditionValue;

        [Header("Presentation")]
        [Tooltip("text_key of DialogueTextData.")]
        public string actionTextKey;
        public string animationKey;

        [Header("Priority")]
        [Tooltip("Higher value is evaluated first.")]
        public int priority;
    }
}
