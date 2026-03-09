using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using LittlePawTown.Utils;

namespace LittlePawTown.Core
{
    /// <summary>
    /// 씬 전환 담당. DontDestroyOnLoad 싱글톤.
    /// - Boot → Title → Game 씬 전환
    /// - 게임 씬 내부 이동(홈, 장소 등)은 UI Stack 으로 처리하므로 씬 로드 불필요
    /// - 페이드 인/아웃 연출 포함
    /// </summary>
    public class SceneController : Singleton<SceneController>
    {
        [SerializeField] private float fadeDuration = 0.3f;

        // ── Public Navigation ──────────────────────────────────
        public void GoToTitle()
        {
            LoadScene(Constants.Scenes.Title, GameState.Title);
        }

        public void GoToGame()
        {
            LoadScene(Constants.Scenes.Game, GameState.Home);
        }

        // ── Internal ───────────────────────────────────────────
        private void LoadScene(string sceneName, GameState nextState)
        {
            StartCoroutine(LoadSceneCoroutine(sceneName, nextState));
        }

        private IEnumerator LoadSceneCoroutine(string sceneName, GameState nextState)
        {
            // 페이드 아웃 (EventBus 로 UI 에 위임)
            EventBus.Publish(new Events.FadeRequestEvent { FadeIn = false, Duration = fadeDuration });
            yield return new WaitForSeconds(fadeDuration);

            var op = SceneManager.LoadSceneAsync(sceneName);
            yield return op;

            GameManager.Instance.ChangeState(nextState);

            // 페이드 인
            EventBus.Publish(new Events.FadeRequestEvent { FadeIn = true, Duration = fadeDuration });
        }
    }
}
