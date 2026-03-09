using UnityEngine;

namespace LittlePawTown.Data
{
    /// <summary>
    /// 미니게임 마스터 데이터.
    /// Assets/Data/MiniGames/ 에 생성.
    /// </summary>
    [CreateAssetMenu(fileName = "NewMiniGame", menuName = "LittlePawTown/Master/MiniGame")]
    public class MiniGameData : MasterDataBase
    {
        [Header("기본 정보")]
        public string nameKo;

        [Header("보상")]
        public int rewardMoneyBase;
        [Tooltip("미니게임 완료 시 지급되는 보조 애정도 포인트.")]
        public int affectionSupportPoint;

        [Header("제한")]
        [Tooltip("재도전 쿨다운 (분). 0이면 제한 없음.")]
        public int cooldownMin;
    }
}
