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
        [Header("단계 정보")]
        public int    level;
        public string nameKo;
        [Tooltip("이 단계에 진입하는 데 필요한 누적 포인트.")]
        public int    requiredPoint;

        [Header("해금 콘텐츠")]
        [Tooltip("해금되는 홈 반응 태그 목록.")]
        public List<string> unlockReactionTags     = new();
        [Tooltip("해금되는 이벤트 그룹 ID 목록.")]
        public List<string> unlockEventGroups      = new();
        [Tooltip("해금되는 홈 행동 태그 목록.")]
        public List<string> unlockHomeBehaviorTags = new();
    }
}
