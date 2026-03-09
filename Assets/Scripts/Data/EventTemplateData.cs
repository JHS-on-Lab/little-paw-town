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
        [Header("기본 정보")]
        public string         title;   // 내부 제목
        public EventCategory  category;
        public bool           repeatable = true;

        [Header("노출 방식")]
        public PresentationType  presentationType = PresentationType.Scene;
        public EventEntryPoint   entryPoint       = EventEntryPoint.LocationDirect;
        [Tooltip("홈 상태 패널 추천 CTA 텍스트 키.")]
        public string            entryCTATextKey;

        [Header("발생 조건 — 장소 / 종 / 날씨 / 시간")]
        [Tooltip("비어 있으면 모든 장소에서 발생 가능.")]
        public List<string> locationScope  = new();
        [Tooltip("비어 있거나 'all' 이면 모든 종에서 발생.")]
        public List<string> speciesScope   = new();
        public List<string> weatherScope   = new();
        public List<string> timeScope      = new();

        [Header("발생 조건 — 애정도")]
        public int affectionMinLevel = 0;
        public int affectionMaxLevel = 999;

        [Header("가중치 / 쿨다운")]
        [Tooltip("높을수록 더 자주 등장.")]
        public int weight       = 1;
        [Tooltip("시간 단위. 0 이면 제한 없음.")]
        public int cooldownHours = 0;

        [Header("분기 / 결과")]
        public List<EventBranchData> branches    = new();
        public EventRewardData       defaultReward;

        [Header("그룹 키")]
        public string branchGroupId;
        public string memoryCardGroup;
        public string followupGroupId;
    }
}
