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
        [Header("Display")]
        public string           nameKo;
        public LocationCategory category;
        public bool             enabledMvp = true;
        public int              displayOrder;

        [Header("Atmosphere Conditions")]
        [Tooltip("Weather tags that highlight this location. Leave empty to allow all weather.")]
        public List<string> weatherTags  = new();
        [Tooltip("Time tags that highlight this location. Leave empty to allow all times.")]
        public List<string> timeTags     = new();

        [Header("Resource Keys")]
        public string bgAssetKey;
        public string bgmKey;
        public string previewTextKey;

        [Header("Ambient Reaction Tags")]
        [Tooltip("Tags used for short ambient reaction on home entry.")]
        public List<string> ambientReactionTags = new();
    }
}
