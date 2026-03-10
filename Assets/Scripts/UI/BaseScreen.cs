using System.Collections;
using UnityEngine;

namespace LittlePawTown.UI
{
    /// <summary>
    /// 모든 화면(Screen)의 베이스 클래스.
    /// UIManager 가 Push/Pop 방식으로 관리한다.
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class BaseScreen : MonoBehaviour
    {
        [Header("Transition")]
        [SerializeField] private float fadeDuration = 0.2f;

        private CanvasGroup _canvasGroup;

        public bool IsVisible { get; private set; }

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        // ── UIManager 가 호출 ──────────────────────────────────
        public void Show(bool instant = false)
        {
            gameObject.SetActive(true);
            StopAllCoroutines();
            if (instant)
            {
                SetAlpha(1f, true);
                IsVisible = true;
                OnShow();
            }
            else
            {
                StartCoroutine(FadeIn());
            }
        }

        public void Hide(bool instant = false)
        {
            StopAllCoroutines();
            if (instant)
            {
                SetAlpha(0f, false);
                IsVisible = false;
                OnHide();
                gameObject.SetActive(false);
            }
            else
            {
                StartCoroutine(FadeOut());
            }
        }

        // ── 하위 클래스 훅 ─────────────────────────────────────
        /// <summary>화면이 완전히 표시된 직후 호출.</summary>
        protected virtual void OnShow() { }

        /// <summary>화면이 완전히 사라진 직후 호출.</summary>
        protected virtual void OnHide() { }

        /// <summary>Android 뒤로가기 버튼. true 반환 시 이벤트 소비.</summary>
        public virtual bool OnBackPressed() => false;

        // ── 내부 ───────────────────────────────────────────────
        private void SetAlpha(float alpha, bool interactable)
        {
            _canvasGroup.alpha          = alpha;
            _canvasGroup.interactable   = interactable;
            _canvasGroup.blocksRaycasts = interactable;
        }

        private IEnumerator FadeIn()
        {
            SetAlpha(0f, false);
            float elapsed = 0f;
            while (elapsed < fadeDuration)
            {
                elapsed           += Time.unscaledDeltaTime;
                _canvasGroup.alpha = Mathf.Clamp01(elapsed / fadeDuration);
                yield return null;
            }
            SetAlpha(1f, true);
            IsVisible = true;
            OnShow();
        }

        private IEnumerator FadeOut()
        {
            SetAlpha(_canvasGroup.alpha, false);
            float elapsed    = 0f;
            float startAlpha = _canvasGroup.alpha;
            while (elapsed < fadeDuration)
            {
                elapsed           += Time.unscaledDeltaTime;
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeDuration);
                yield return null;
            }
            SetAlpha(0f, false);
            IsVisible = false;
            OnHide();
            gameObject.SetActive(false);
        }
    }
}
