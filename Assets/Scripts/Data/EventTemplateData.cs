using System.Collections.Generic;
using UnityEngine;

namespace LittlePawTown.Data
{
    public enum EventCategory
    {
        Environment,  // 환경 반응
        Habit,        // 습관 반응
        Relation,     // 관계 이벤트
        Affection,    // 애정도 이벤트
        Chain,        // 연쇄 이벤트
    }

    public enum PresentationType
    {
        Ambient,  // 홈 진입 직후 짧은 생활 반응
        Scene,    // 장소에서 소비하는 본격 장면
        Push,     // 푸시 알림형 돌발
    }

    public enum EventEntryPoint
    {
        HomeAuto,       // 홈 진입 시 자동 재생
        HomeSuggested,  // 홈 상태 패널 추천 CTA
        LocationDirect, // 장소 진입 후 발생
        Push,           // 푸시 알림 진입
    }

    /// <summary>
    /// 이벤트 원형 마스터 데이터. 가장 핵심 SO.
    /// Assets/Data/Events/ 에 evt_xxx.asset 으로 생성.
    /// </summary>
    [CreateAssetMenu(fileName = "NewEventTemplate", menuName = "LittlePawTown/Master/EventTemplate")]
    public class EventTemplateData : MasterDataBase
    {
        [Header("Basic Info")]
        public string         title;   // internal title
        public EventCategory  category;
        public bool           repeatable = true;

        [Header("Presentation Type")]
        public PresentationType  presentationType = PresentationType.Scene;
        public EventEntryPoint   entryPoint       = EventEntryPoint.LocationDirect;
        [Tooltip("CTA text key for home status panel suggestion.")]
        public string            entryCTATextKey;

        [Header("Trigger Conditions — Location / Species / Weather / Time")]
        [Tooltip("Leave empty to allow in all locations.")]
        public List<string> locationScope  = new();
        [Tooltip("Leave empty or 'all' to allow for all species.")]
        public List<string> speciesScope   = new();
        public List<string> weatherScope   = new();
        public List<string> timeScope      = new();

        [Header("Trigger Conditions — Affection")]
        public int affectionMinLevel = 0;
        public int affectionMaxLevel = 999;

        [Header("Weight / Cooldown")]
        [Tooltip("Higher value means more frequent appearance.")]
        public int weight       = 1;
        [Tooltip("In hours. 0 means no limit.")]
        public int cooldownHours = 0;

        [Header("Branches / Rewards")]
        public List<EventBranchData> branches    = new();
        public EventRewardData       defaultReward;

        [Header("Group Keys")]
        public string branchGroupId;
        public string memoryCardGroup;
        public string followupGroupId;
    }
}
