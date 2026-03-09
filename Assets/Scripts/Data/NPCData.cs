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
        [Header("기본 정보")]
        public string  nameKo;
        public NpcType npcType;
        public bool    enabledMvp = true;

        [Header("기본 장소")]
        [Tooltip("이 NPC 가 주로 머무는 장소 ID.")]
        public string defaultLocationId;

        [Header("성격 태그")]
        public List<string> personalityTags = new();

        [Header("리소스")]
        public Sprite portrait;
    }
}
