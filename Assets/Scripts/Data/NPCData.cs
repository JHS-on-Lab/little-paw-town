using System.Collections.Generic;
using UnityEngine;

namespace LittlePawTown.Data
{
    public enum NpcType
    {
        Resident,
        AnimalFriend,
    }

    /// <summary>
    /// NPC (주민 / 동물 친구) 마스터 데이터.
    /// Assets/Data/NPCs/ 에 생성.
    /// </summary>
    [CreateAssetMenu(fileName = "NewNPC", menuName = "LittlePawTown/Master/NPC")]
    public class NPCData : MasterDataBase
    {
        [Header("Basic Info")]
        public string  nameKo;
        public NpcType npcType;
        public bool    enabledMvp = true;

        [Header("Default Location")]
        [Tooltip("Location ID where this NPC mainly resides.")]
        public string defaultLocationId;

        [Header("Personality Tags")]
        public List<string> personalityTags = new();

        [Header("Resources")]
        public Sprite portrait;
    }
}
