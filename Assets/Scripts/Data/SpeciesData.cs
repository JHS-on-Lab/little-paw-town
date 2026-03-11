using UnityEngine;

namespace LittlePawTown.Data
{
    /// <summary>
    /// 반려동물 종 마스터 데이터.
    /// Assets/Data/Species/ 에 dog.asset, cat.asset 등으로 생성.
    /// </summary>
    [CreateAssetMenu(fileName = "NewSpecies", menuName = "LittlePawTown/Master/Species")]
    public class SpeciesData : MasterDataBase
    {
        [Header("Display")]
        public string nameKo;
        public Sprite uiIcon;

        [Header("Resource Keys")]
        public string bodyTypeKey;
        public string animationSetId;
        public string defaultVoiceSet;

        [Header("Sort")]
        public int displayOrder;
    }
}
