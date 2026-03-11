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
        [Header("Basic Info")]
        public string nameKo;

        [Header("Rewards")]
        public int rewardMoneyBase;
        [Tooltip("Bonus affection points granted on mini-game completion.")]
        public int affectionSupportPoint;

        [Header("Limits")]
        [Tooltip("Retry cooldown in minutes. 0 means no limit.")]
        public int cooldownMin;
    }
}
