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
        [Header("표시")]
        public string nameKo;
        public Sprite uiIcon;

        [Header("리소스 키")]
        public string bodyTypeKey;
        public string animationSetId;
        public string defaultVoiceSet;

        [Header("정렬")]
        public int displayOrder;
    }
}
