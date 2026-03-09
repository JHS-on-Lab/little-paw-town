using UnityEngine;
using LittlePawTown.Utils;

namespace LittlePawTown.Core
{
    /// <summary>
    /// 게임 전역 상태 관리. DontDestroyOnLoad 싱글톤.
    /// - 현재 GameState 보관 및 전이
    /// - 앱 라이프사이클 (포커스, 일시정지) 처리
    /// - 다른 Manager 들의 초기화 순서 조율
    /// </summary>
    public class GameManager : Singleton<GameManager>
    {
        // ── State ──────────────────────────────────────────────
        public GameState CurrentState { get; private set; } = GameState.Boot;

        // ── Events ─────────────────────────────────────────────
        public event System.Action<GameState, GameState> OnStateChanged;  // (prev, next)

        // ── Init ───────────────────────────────────────────────
        protected override void OnAwake()
        {
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            var saveManager     = SaveManager.Instance;
            var masterData      = MasterDataManager.Instance;
            var sceneController = SceneController.Instance;

            // 저장 데이터 로드
            saveManager.Load();

            // 최초 실행 여부에 따라 첫 화면 결정
            if (!saveManager.PlayerData.tutorialCleared)
                sceneController.GoToTitle();
            else
                sceneController.GoToTitle();   // 항상 타이틀부터 (로그인 화면 연결 예정)
        }

        // ── Public API ─────────────────────────────────────────
        public void ChangeState(GameState next)
        {
            if (CurrentState == next) return;

            var prev = CurrentState;
            CurrentState = next;
            OnStateChanged?.Invoke(prev, next);

            Debug.Log($"[GameManager] State: {prev} → {next}");
        }

        // ── App Lifecycle ──────────────────────────────────────
        private void OnApplicationPause(bool isPaused)
        {
            if (isPaused)
                SaveManager.Instance.Save();
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
                SaveManager.Instance.Save();
        }
    }
}
