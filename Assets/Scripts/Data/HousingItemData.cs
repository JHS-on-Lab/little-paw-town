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
        [Header("Basic Info")]
        public string          nameKo;
        public HousingCategory category;
        public bool            enabledMvp = true;

        [Header("Economy")]
        public int priceMoney;

        [Header("Placement")]
        public bool   placeable = true;
        public int    sizeX     = 1;
        public int    sizeY     = 1;

        [Header("Interaction")]
        [Tooltip("Pet reaction tag triggered when this item is placed.")]
        public string interactionTag;

        [Header("Resources")]
        public Sprite previewSprite;
    }
}
