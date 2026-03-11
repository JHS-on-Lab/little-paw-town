using UnityEngine;
using UnityEngine.UI;
using LittlePawTown.Core;

namespace LittlePawTown.UI.Screens
{
    /// <summary>
    /// SCR-002 타이틀 화면.
    /// Hierarchy:
    ///   [TitleScreen] CanvasGroup + TitleScreen
    ///     Logo          (Image)
    ///     BtnStart      (Button)
    ///     BtnSettings   (Button)
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
            // 이미 반려동물이 있으면 바로 게임으로
            if (!string.IsNullOrEmpty(SaveManager.Instance.PetData.petId))
            {
                SceneController.Instance.GoToGame();
            }
        }

        private void OnStartClicked()
        {
            SceneController.Instance.GoToPetCreation();
        }

        private void OnSettingsClicked()
        {
            // TODO: 설정 화면 (PHASE 13)
            Debug.Log("[Title] Settings clicked.");
        }

        public override bool OnBackPressed() => true;
    }
}
