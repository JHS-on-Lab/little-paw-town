using UnityEngine;
using UnityEngine.UI;
using LittlePawTown.Core;

namespace LittlePawTown.UI.Screens
{
    /// <summary>
    /// SCR-002 타이틀 화면.
    /// Hierarchy:
    ///   [TitleScreen] CanvasGroup + TitleScreen
    ///     SafeArea (SafeAreaFitter)
    ///       Logo          (Image)
    ///       BtnStart      (Button)
    ///       BtnSettings   (Button)
    /// </summary>
    public class TitleScreen : BaseScreen
    {
        [Header("References")]
        [SerializeField] private Button btnStart;
        [SerializeField] private Button btnSettings;

        protected override void Awake()
        {
            base.Awake();
            btnStart.onClick.AddListener(OnStartClicked);
            btnSettings.onClick.AddListener(OnSettingsClicked);
        }

        protected override void OnShow()
        {
            // 이미 반려동물이 있으면 게임 씬으로 바로 이동
            var petData = SaveManager.Instance.PetData;
            if (!string.IsNullOrEmpty(petData.petId))
            {
                SceneController.Instance.GoToGame();
                return;
            }
        }

        // ── 버튼 핸들러 ────────────────────────────────────────
        private void OnStartClicked()
        {
            // 최초 실행 → 튜토리얼, 이후 → PetCreation 또는 Game
            var player = SaveManager.Instance.PlayerData;
            if (!player.tutorialCleared)
            {
                // TODO: 튜토리얼 화면으로 Push (PHASE 12)
                // 임시: 바로 PetCreation 으로
                GoToPetCreation();
            }
            else
            {
                SceneController.Instance.GoToGame();
            }
        }

        private void OnSettingsClicked()
        {
            // TODO: 설정 화면 Push (PHASE 13)
            Debug.Log("[Title] Settings clicked.");
        }

        private void GoToPetCreation()
        {
            // PetCreation 은 같은 Title 씬 내 화면 스택으로 처리
            // TODO: PHASE 2 구현 후 UIManager.Instance.Push(petCreationScreen) 로 교체
            Debug.Log("[Title] Go to PetCreation.");
        }

        // ── 뒤로가기 ───────────────────────────────────────────
        public override bool OnBackPressed()
        {
            // 타이틀에서 뒤로가기 → 아무것도 안 함 (앱 종료 방지)
            return true;
        }
    }
}
