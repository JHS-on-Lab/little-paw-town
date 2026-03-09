using UnityEngine;
using LittlePawTown.Utils;

namespace LittlePawTown.Core
{
    /// <summary>
    /// ScriptableObject 마스터 데이터 레지스트리. DontDestroyOnLoad 싱글톤.
    /// Inspector 에서 각 SO 컨테이너를 할당한다.
    /// PHASE 1 에서 각 SO 클래스를 구현하고 이곳에 필드를 추가한다.
    /// </summary>
    public class MasterDataManager : Singleton<MasterDataManager>
    {
        // ── 마스터 SO 레퍼런스 (PHASE 1 에서 채워질 예정) ──────
        // [SerializeField] private SpeciesDataContainer  speciesContainer;
        // [SerializeField] private PartDataContainer     partContainer;
        // [SerializeField] private TraitDataContainer    traitContainer;
        // [SerializeField] private LocationDataContainer locationContainer;
        // [SerializeField] private NPCDataContainer      npcContainer;
        // [SerializeField] private EventTemplateDataContainer eventContainer;
        // [SerializeField] private AffectionLevelDataContainer affectionContainer;
        // [SerializeField] private HousingItemDataContainer    housingContainer;
        // [SerializeField] private MiniGameDataContainer       miniGameContainer;
        // [SerializeField] private PushScenarioDataContainer   pushContainer;

        protected override void OnAwake()
        {
            // 각 컨테이너 유효성 검증은 PHASE 1 구현 후 추가
            Debug.Log("[MasterDataManager] Initialized.");
        }
    }
}
