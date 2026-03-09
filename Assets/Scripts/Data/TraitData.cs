using UnityEngine;

namespace LittlePawTown.Data
{
    /// <summary>
    /// 반려동물 공통 특성 마스터 데이터.
    /// curiosity / activity / sociability / appetite / caution 5개 고정.
    /// Assets/Data/ 에 단일 TraitMaster.asset 으로 생성해도 되고,
    /// 특성별 개별 asset 으로 관리해도 무방.
    /// </summary>
    [CreateAssetMenu(fileName = "NewTrait", menuName = "LittlePawTown/Master/Trait")]
    public class TraitData : MasterDataBase
    {
        [Header("표시")]
        public string nameKo;
        public string description;

        [Header("값 범위")]
        public int minValue = 1;
        public int maxValue = 3;
    }
}
