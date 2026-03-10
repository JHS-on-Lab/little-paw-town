using UnityEngine;
using LittlePawTown.UI.Screens;

namespace LittlePawTown.UI
{
    /// <summary>
    /// Title 씬에 배치. 씬 로드 직후 UIManager 스택을 초기화하고
    /// TitleScreen 을 루트로 설정한다.
    /// </summary>
    public class TitleSceneInitializer : MonoBehaviour
    {
        [SerializeField] private TitleScreen titleScreen;

        private void Start()
        {
            UIManager.Instance.SetRoot(titleScreen, instant: true);
            Debug.Log("[Title] Scene initialized. Root: TitleScreen.");
        }
    }
}
