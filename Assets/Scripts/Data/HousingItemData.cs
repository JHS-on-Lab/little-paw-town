using UnityEngine;

namespace LittlePawTown.Data
{
    public enum HousingCategory
    {
        Furniture,
        Deco,
        Toy,
        Wall,
        Floor,
    }

    /// <summary>
    /// 하우징 소품 마스터 데이터.
    /// Assets/Data/Housing/ 에 생성.
    /// </summary>
    [CreateAssetMenu(fileName = "NewHousingItem", menuName = "LittlePawTown/Master/HousingItem")]
    public class HousingItemData : MasterDataBase
    {
        [Header("기본 정보")]
        public string          nameKo;
        public HousingCategory category;
        public bool            enabledMvp = true;

        [Header("경제")]
        public int priceMoney;

        [Header("배치")]
        public bool   placeable = true;
        public int    sizeX     = 1;
        public int    sizeY     = 1;

        [Header("반응 연동")]
        [Tooltip("이 소품이 배치되면 트리거되는 반려동물 반응 태그.")]
        public string interactionTag;

        [Header("리소스")]
        public Sprite previewSprite;
    }
}
