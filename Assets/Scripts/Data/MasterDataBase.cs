using UnityEngine;

namespace LittlePawTown.Data
{
    /// <summary>
    /// 모든 마스터 ScriptableObject 의 베이스 클래스.
    /// id 와 enabled 플래그를 공통으로 가진다.
    /// </summary>
    public abstract class MasterDataBase : ScriptableObject
    {
        [Tooltip("데이터 고유 ID (snake_case). 코드에서 참조하는 키.")]
        public string id;

        [Tooltip("false 면 런타임에서 사용하지 않음.")]
        public bool enabled = true;
    }
}
