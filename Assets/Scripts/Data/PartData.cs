using UnityEngine;

namespace LittlePawTown.Data
{
    public enum PartCategory
    {
        Ear,
        Eye,
        Tail,
        Pattern,
        Accessory,
    }

    /// <summary>
    /// 외형 파츠 마스터 데이터.
    /// Assets/Data/Parts/ 에 종·카테고리별로 생성.
    /// </summary>
    [CreateAssetMenu(fileName = "NewPart", menuName = "LittlePawTown/Master/Part")]
    public class PartData : MasterDataBase
    {
        [Header("Classification")]
        public string      speciesId;
        public PartCategory partCategory;

        [Header("Display")]
        public string nameKo;
        public Sprite  sprite;

        [Header("Rendering")]
        [Tooltip("Lower value is drawn behind.")]
        public int  sortOrder;
        public bool colorable;

        [Header("Unlock")]
        public bool defaultUnlock = true;
    }
}
