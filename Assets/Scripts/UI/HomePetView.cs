using System.Collections;
using UnityEngine;
using LittlePawTown.Core;
using LittlePawTown.Data;

namespace LittlePawTown.UI
{
    /// <summary>
    /// 홈 화면의 반려동물 표시 컴포넌트.
    /// 저장된 외형을 PetRenderer 에 적용하고, 아이들 애니메이션을 실행한다.
    /// </summary>
    [RequireComponent(typeof(PetRenderer))]
    public class HomePetView : MonoBehaviour
    {
        [Header("Idle Animation")]
        [SerializeField] private float idleAmplitude = 8f;    // 픽셀 단위 (RectTransform)
        [SerializeField] private float idlePeriod    = 2.4f;  // 1사이클 시간(초)

        [Header("Tap Reaction")]
        [SerializeField] private float tapSquishDuration = 0.25f;

        private PetRenderer      _renderer;
        private RectTransform    _rect;
        private Vector2          _originAnchoredPos;
        private Coroutine        _idleRoutine;

        private void Awake()
        {
            _renderer = GetComponent<PetRenderer>();
            _rect     = GetComponent<RectTransform>();
        }

        // ── Public API ──────────────────────────────────────────

        /// <summary>저장된 외형 데이터로 반려동물을 표시한다.</summary>
        public void Load()
        {
            var appearance = SaveManager.Instance.Appearance;
            _renderer.ApplyAppearance(appearance, MasterDataManager.Instance);
        }

        /// <summary>아이들 애니메이션 시작.</summary>
        public void StartIdle()
        {
            _originAnchoredPos = _rect.anchoredPosition;
            _idleRoutine = StartCoroutine(IdleLoop());
        }

        /// <summary>아이들 애니메이션 정지.</summary>
        public void StopIdle()
        {
            if (_idleRoutine != null)
            {
                StopCoroutine(_idleRoutine);
                _idleRoutine = null;
            }
            _rect.anchoredPosition = _originAnchoredPos;
        }

        /// <summary>탭(터치) 반응 — 살짝 눌리는 느낌.</summary>
        public void PlayTapReaction()
        {
            StopAllCoroutines();
            StartCoroutine(TapSquish());
        }

        // ── 애니메이션 코루틴 ────────────────────────────────────

        private IEnumerator IdleLoop()
        {
            float t = 0f;
            while (true)
            {
                t += Time.deltaTime;
                float y = Mathf.Sin(t * Mathf.PI * 2f / idlePeriod) * idleAmplitude;
                _rect.anchoredPosition = _originAnchoredPos + new Vector2(0f, y);
                yield return null;
            }
        }

        private IEnumerator TapSquish()
        {
            var originalScale = transform.localScale;
            // 눌리기
            float half = tapSquishDuration * 0.5f;
            float elapsed = 0f;
            while (elapsed < half)
            {
                elapsed += Time.unscaledDeltaTime;
                float t  = elapsed / half;
                transform.localScale = Vector3.Lerp(originalScale,
                    new Vector3(1.15f, 0.85f, 1f), t);
                yield return null;
            }
            // 복원
            elapsed = 0f;
            while (elapsed < half)
            {
                elapsed += Time.unscaledDeltaTime;
                float t  = elapsed / half;
                transform.localScale = Vector3.Lerp(
                    new Vector3(1.15f, 0.85f, 1f), originalScale, t);
                yield return null;
            }
            transform.localScale = originalScale;
            StartIdle();
        }
    }
}
