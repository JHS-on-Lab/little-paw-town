using UnityEngine;
using LittlePawTown.UI.Screens;

namespace LittlePawTown.UI
{
    /// <summary>
    /// Game 씬에 배치. 씬 로드 직후 UIManager 스택을 초기화하고
    /// HomeScreen 을 루트로 설정한다.
    /// Hierarchy:
    ///   [GameSceneInitializer]  이 컴포넌트
    ///   [FadeCanvas]            FadeScreen (Sort Order 높음)
    ///   [UIRoot] Canvas
    ///     [HomeScreen]          CanvasGroup + HomeScreen
    ///     [LocationSelectScreen]
    ///     [AlbumScreen]
    ///     [HousingScreen]
    /// </summary>
    public class GameSceneInitializer : MonoBehaviour
    {
        [SerializeField] private HomeScreen homeScreen;

        private void Start()
        {
            UIManager.Instance.SetRoot(homeScreen, instant: true);
            Debug.Log("[Game] Scene initialized. Root: HomeScreen.");
        }
    }
}
