using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using LittlePawTown.Core;

namespace LittlePawTown.UI
{
    /// <summary>
    /// 씬 전환 시 전체 화면 페이드 인/아웃 처리.
    /// Canvas 최상단(Sort Order 높음)에 배치된 전체 화면 Image 에 붙인다.
    /// EventBus 의 FadeRequestEvent 를 구독한다.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class FadeScreen : MonoBehaviour
    {
        [SerializeField] private Color fadeColor = Color.black;

        private Image _image;

        private void Awake()
        {
            _image       = GetComponent<Image>();
            _image.color = new Color(fadeColor.r, fadeColor.g, fadeColor.b, 0f);
            _image.raycastTarget = false;
        }

        private void OnEnable()
        {
            EventBus.Subscribe<Events.FadeRequestEvent>(OnFadeRequest);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<Events.FadeRequestEvent>(OnFadeRequest);
        }

        private void OnFadeRequest(Events.FadeRequestEvent e)
        {
            StopAllCoroutines();
            StartCoroutine(e.FadeIn ? DoFadeIn(e.Duration) : DoFadeOut(e.Duration));
        }

        private IEnumerator DoFadeIn(float duration)
        {
            _image.raycastTarget = false;
            float elapsed    = 0f;
            float startAlpha = _image.color.a;
            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                SetAlpha(Mathf.Lerp(startAlpha, 0f, elapsed / duration));
                yield return null;
            }
            SetAlpha(0f);
        }

        private IEnumerator DoFadeOut(float duration)
        {
            _image.raycastTarget = true;
            float elapsed    = 0f;
            float startAlpha = _image.color.a;
            while (elapsed < duration)
            {
                elapsed += Time.unscaledDeltaTime;
                SetAlpha(Mathf.Lerp(startAlpha, 1f, elapsed / duration));
                yield return null;
            }
            SetAlpha(1f);
        }

        private void SetAlpha(float a)
        {
            var c   = _image.color;
            c.a     = a;
            _image.color = c;
        }
    }
}
