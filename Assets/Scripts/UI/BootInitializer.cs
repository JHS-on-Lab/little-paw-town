using UnityEngine;
using LittlePawTown.Core;

namespace LittlePawTown.UI
{
    /// <summary>
    /// Boot 씬에 배치. 매니저 싱글톤들을 순서대로 깨운 뒤 Title 씬으로 이동.
    /// Hierarchy 구조:
    ///   [BootInitializer]        ← 이 컴포넌트
    ///   [Managers]
    ///     - GameManager
    ///     - SaveManager
    ///     - MasterDataManager
    ///     - SceneController
    ///     - UIManager
    /// </summary>
    public class BootInitializer : MonoBehaviour
    {
        private void Start()
        {
            // 싱글톤 Instance 프로퍼티 접근 → 없으면 자동 생성 + DontDestroyOnLoad
            _ = GameManager.Instance;
            _ = SaveManager.Instance;
            _ = MasterDataManager.Instance;
            _ = SceneController.Instance;
            _ = UIManager.Instance;

            Debug.Log("[Boot] All managers initialized.");
        }
    }
}
