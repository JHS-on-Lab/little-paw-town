using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using LittlePawTown.Data;
using LittlePawTown.Utils;

namespace LittlePawTown.Core
{
    /// <summary>
    /// ScriptableObject 마스터 데이터 레지스트리. DontDestroyOnLoad 싱글톤.
    /// Inspector 에서 각 리스트에 SO asset 을 할당한다.
    /// </summary>
    public class MasterDataManager : Singleton<MasterDataManager>
    {
        [Header("Species")]
        [SerializeField] private List<SpeciesData>        species         = new();

        [Header("Parts")]
        [SerializeField] private List<PartData>           parts           = new();

        [Header("Traits")]
        [SerializeField] private List<TraitData>          traits          = new();

        [Header("Locations")]
        [SerializeField] private List<LocationData>       locations       = new();

        [Header("NPCs")]
        [SerializeField] private List<NPCData>            npcs            = new();

        [Header("Events")]
        [SerializeField] private List<EventTemplateData>  eventTemplates  = new();
        [SerializeField] private List<EventBranchData>    eventBranches   = new();
        [SerializeField] private List<EventRewardData>    eventRewards    = new();
        [SerializeField] private List<DialogueTextData>   dialogues       = new();

        [Header("Affection Levels")]
        [SerializeField] private List<AffectionLevelData> affectionLevels = new();

        [Header("Housing")]
        [SerializeField] private List<HousingItemData>    housingItems    = new();

        [Header("Mini Games")]
        [SerializeField] private List<MiniGameData>       miniGames       = new();

        [Header("Push")]
        [SerializeField] private List<PushScenarioData>   pushScenarios   = new();

        // ── Lookup Dictionaries ────────────────────────────────
        private Dictionary<string, SpeciesData>        _speciesMap;
        private Dictionary<string, PartData>           _partMap;
        private Dictionary<string, TraitData>          _traitMap;
        private Dictionary<string, LocationData>       _locationMap;
        private Dictionary<string, NPCData>            _npcMap;
        private Dictionary<string, EventTemplateData>  _eventMap;
        private Dictionary<string, EventBranchData>    _branchMap;
        private Dictionary<string, EventRewardData>    _rewardMap;
        private Dictionary<string, DialogueTextData>   _dialogueMap;
        private Dictionary<int,    AffectionLevelData> _affectionMap;
        private Dictionary<string, HousingItemData>    _housingMap;
        private Dictionary<string, MiniGameData>       _miniGameMap;
        private Dictionary<string, PushScenarioData>   _pushMap;

        // ── Init ───────────────────────────────────────────────
        protected override void OnAwake()
        {
            BuildMaps();
            Debug.Log("[MasterDataManager] Initialized.");
        }

        private void BuildMaps()
        {
            _speciesMap   = species.Where(x => x != null).ToDictionary(x => x.id);
            _partMap      = parts.Where(x => x != null).ToDictionary(x => x.id);
            _traitMap     = traits.Where(x => x != null).ToDictionary(x => x.id);
            _locationMap  = locations.Where(x => x != null).ToDictionary(x => x.id);
            _npcMap       = npcs.Where(x => x != null).ToDictionary(x => x.id);
            _eventMap     = eventTemplates.Where(x => x != null).ToDictionary(x => x.id);
            _branchMap    = eventBranches.Where(x => x != null).ToDictionary(x => x.id);
            _rewardMap    = eventRewards.Where(x => x != null).ToDictionary(x => x.id);
            _dialogueMap  = dialogues.Where(x => x != null).ToDictionary(x => x.id);
            _affectionMap = affectionLevels.Where(x => x != null).ToDictionary(x => x.level);
            _housingMap   = housingItems.Where(x => x != null).ToDictionary(x => x.id);
            _miniGameMap  = miniGames.Where(x => x != null).ToDictionary(x => x.id);
            _pushMap      = pushScenarios.Where(x => x != null).ToDictionary(x => x.id);
        }

        // ── Single Getters ─────────────────────────────────────
        public SpeciesData        GetSpecies(string id)       => _speciesMap.GetValueOrDefault(id);
        public PartData           GetPart(string id)          => _partMap.GetValueOrDefault(id);
        public TraitData          GetTrait(string id)         => _traitMap.GetValueOrDefault(id);
        public LocationData       GetLocation(string id)      => _locationMap.GetValueOrDefault(id);
        public NPCData            GetNPC(string id)           => _npcMap.GetValueOrDefault(id);
        public EventTemplateData  GetEvent(string id)         => _eventMap.GetValueOrDefault(id);
        public EventBranchData    GetBranch(string id)        => _branchMap.GetValueOrDefault(id);
        public EventRewardData    GetReward(string id)        => _rewardMap.GetValueOrDefault(id);
        public DialogueTextData   GetDialogue(string id)      => _dialogueMap.GetValueOrDefault(id);
        public AffectionLevelData GetAffectionLevel(int lvl)  => _affectionMap.GetValueOrDefault(lvl);
        public HousingItemData    GetHousingItem(string id)   => _housingMap.GetValueOrDefault(id);
        public MiniGameData       GetMiniGame(string id)      => _miniGameMap.GetValueOrDefault(id);
        public PushScenarioData   GetPushScenario(string id)  => _pushMap.GetValueOrDefault(id);

        // ── List Getters ───────────────────────────────────────
        public IReadOnlyList<SpeciesData>        AllSpecies         => species;
        public IReadOnlyList<PartData>           AllParts           => parts;
        public IReadOnlyList<LocationData>       AllLocations       => locations;
        public IReadOnlyList<NPCData>            AllNPCs            => npcs;
        public IReadOnlyList<EventTemplateData>  AllEventTemplates  => eventTemplates;
        public IReadOnlyList<AffectionLevelData> AllAffectionLevels => affectionLevels;
        public IReadOnlyList<HousingItemData>    AllHousingItems    => housingItems;
        public IReadOnlyList<MiniGameData>       AllMiniGames       => miniGames;
        public IReadOnlyList<PushScenarioData>   AllPushScenarios   => pushScenarios;
    }
}
