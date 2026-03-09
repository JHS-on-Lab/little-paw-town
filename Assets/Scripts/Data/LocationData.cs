using System.Collections.Generic;
using UnityEngine;

namespace LittlePawTown.Data
{
    public enum LocationCategory
    {
        Home,
        Outdoor,
        Shop,
        Square,
    }

    /// <summary>
    /// 장소 마스터 데이터.
    /// Assets/Data/Locations/ 에 home.asset, park.asset 등으로 생성.
    /// </summary>
    [CreateAssetMenu(fileName = "NewLocation", menuName = "LittlePawTown/Master/Location")]
    public class LocationData : MasterDataBase
    {
        [Header("표시")]
        public string           nameKo;
        public LocationCategory category;
        public bool             enabledMvp = true;
        public int              displayOrder;

        [Header("분위기 조건")]
        [Tooltip("이 장소가 강조되는 날씨 태그 목록. 비어 있으면 모든 날씨 허용.")]
        public List<string> weatherTags  = new();
        [Tooltip("이 장소가 강조되는 시간대 태그 목록. 비어 있으면 모든 시간 허용.")]
        public List<string> timeTags     = new();

        [Header("리소스 키")]
        public string bgAssetKey;
        public string bgmKey;
        public string previewTextKey;

        [Header("Ambient 연출 태그")]
        [Tooltip("홈 진입 직후 짧은 생활 반응 연출에 사용되는 태그 목록.")]
        public List<string> ambientReactionTags = new();
    }
}
