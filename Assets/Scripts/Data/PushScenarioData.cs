using UnityEngine;

namespace LittlePawTown.Data
{
    public enum PushTriggerType
    {
        Idle,           // 일정 시간 미접속
        TimeBased,      // 특정 시간대
        EventFollowup,  // 이전 이벤트 후속
    }

    /// <summary>
    /// 돌발 이벤트 / 푸시 알림 템플릿 마스터 데이터.
    /// Assets/Data/Push/ 에 생성.
    /// </summary>
    [CreateAssetMenu(fileName = "NewPushScenario", menuName = "LittlePawTown/Master/PushScenario")]
    public class PushScenarioData : MasterDataBase
    {
        [Header("Notification Text Keys")]
        public string titleTextKey;
        public string bodyTextKey;

        [Header("Trigger")]
        public PushTriggerType triggerType;
        [Tooltip("Cooldown in hours to prevent duplicates.")]
        public int cooldownHours;

        [Header("Linkage")]
        [Tooltip("Event group ID linked when notification is opened.")]
        public string relatedEventGroup;
        [Tooltip("Bonus affection points granted when notification is opened.")]
        public int affectionBonusOnOpen;
    }
}
