using UnityEngine;

namespace LittlePawTown.UI
{
    /// <summary>
    /// Android 노치 / 펀치홀 / 라운드 코너 대응.
    /// RectTransform 을 Screen.safeArea 에 맞게 조정한다.
    /// Canvas 바로 아래 SafeArea 패널에 붙여서 사용.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaFitter : MonoBehaviour
    {
        private RectTransform _rect;
        private Rect          _lastSafeArea;
        private Vector2       _lastScreenSize;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        private void Start()
        {
            Apply();
        }

#if UNITY_EDITOR
        // 에디터에서 Game View 해상도 바꿔도 즉시 반영
        private void Update()
        {
            var screenSize = new Vector2(Screen.width, Screen.height);
            if (_lastSafeArea != Screen.safeArea || _lastScreenSize != screenSize)
                Apply();
        }
#endif

        private void Apply()
        {
            var safeArea   = Screen.safeArea;
            var screenSize = new Vector2(Screen.width, Screen.height);

            if (screenSize.x == 0 || screenSize.y == 0) return;

            var anchorMin = new Vector2(safeArea.x / screenSize.x,
                                        safeArea.y / screenSize.y);
            var anchorMax = new Vector2((safeArea.x + safeArea.width)  / screenSize.x,
                                        (safeArea.y + safeArea.height) / screenSize.y);

            _rect.anchorMin = anchorMin;
            _rect.anchorMax = anchorMax;
            _rect.offsetMin = Vector2.zero;
            _rect.offsetMax = Vector2.zero;

            _lastSafeArea   = safeArea;
            _lastScreenSize = screenSize;
        }
    }
}
