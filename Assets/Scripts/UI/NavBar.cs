using UnityEngine;
using UnityEngine.UI;

namespace LittlePawTown.UI
{
    /// <summary>
    /// 게임 씬 하단 네비게이션 바.
    /// 현재 활성 탭을 하이라이트하고, 탭 클릭 시 콜백을 호출한다.
    /// Hierarchy:
    ///   [NavBar]  NavBar 컴포넌트
    ///     BtnHome     (Button + Image)
    ///     BtnLocation (Button + Image)
    ///     BtnAlbum    (Button + Image)
    ///     BtnHousing  (Button + Image)
    /// </summary>
    public class NavBar : MonoBehaviour
    {
        public enum Tab { Home, Location, Album, Housing }

        [Header("Tab Buttons")]
        [SerializeField] private Button btnHome;
        [SerializeField] private Button btnLocation;
        [SerializeField] private Button btnAlbum;
        [SerializeField] private Button btnHousing;

        [Header("Highlight Colors")]
        [SerializeField] private Color activeColor   = new(1.00f, 0.75f, 0.40f);
        [SerializeField] private Color inactiveColor = new(0.65f, 0.65f, 0.65f);

        private Tab _currentTab = Tab.Home;

        // 탭 전환 콜백 (HomeScreen 에서 등록)
        public System.Action<Tab> OnTabChanged;

        private void Awake()
        {
            btnHome.onClick.AddListener(()     => SelectTab(Tab.Home));
            btnLocation.onClick.AddListener(() => SelectTab(Tab.Location));
            btnAlbum.onClick.AddListener(()    => SelectTab(Tab.Album));
            btnHousing.onClick.AddListener(()  => SelectTab(Tab.Housing));
        }

        public void SelectTab(Tab tab, bool silent = false)
        {
            _currentTab = tab;
            RefreshHighlight();
            if (!silent) OnTabChanged?.Invoke(tab);
        }

        private void RefreshHighlight()
        {
            SetTabColor(btnHome,     _currentTab == Tab.Home);
            SetTabColor(btnLocation, _currentTab == Tab.Location);
            SetTabColor(btnAlbum,    _currentTab == Tab.Album);
            SetTabColor(btnHousing,  _currentTab == Tab.Housing);
        }

        private void SetTabColor(Button btn, bool active)
        {
            // 버튼 자식의 첫 번째 Image (아이콘) 에 색상 적용
            var img = btn.GetComponentInChildren<Image>();
            if (img != null) img.color = active ? activeColor : inactiveColor;
        }

        public Tab CurrentTab => _currentTab;
    }
}
