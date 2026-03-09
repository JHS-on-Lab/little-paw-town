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
        [Header("알림 텍스트 키")]
        public string titleTextKey;
        public string bodyTextKey;

        [Header("트리거")]
        public PushTriggerType triggerType;
        [Tooltip("중복 방지 쿨다운 (시간).")]
        public int cooldownHours;

        [Header("연계")]
        [Tooltip("알림 진입 시 연결되는 이벤트 그룹 ID.")]
        public string relatedEventGroup;
        [Tooltip("알림 열람 시 지급되는 추가 애정도.")]
        public int affectionBonusOnOpen;
    }
}
