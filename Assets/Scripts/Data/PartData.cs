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
        [Header("분류")]
        public string      speciesId;
        public PartCategory partCategory;

        [Header("표시")]
        public string nameKo;
        public Sprite  sprite;

        [Header("렌더링")]
        [Tooltip("낮을수록 뒤에 그려짐.")]
        public int  sortOrder;
        public bool colorable;

        [Header("해금")]
        public bool defaultUnlock = true;
    }
}
