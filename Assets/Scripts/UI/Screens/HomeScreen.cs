using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LittlePawTown.Core;
using LittlePawTown.Utils;

namespace LittlePawTown.UI.Screens
{
    /// <summary>
    /// SCR-020 홈 화면 — 핵심 관찰 허브.
    /// Hierarchy:
    ///   [HomeScreen]  CanvasGroup + HomeScreen
    ///     BgImage           (Image) — 시간대별 배경 색조
    ///     PetArea
    ///       PetView         (HomePetView + PetRenderer)
    ///         Body / Pattern / Tail / Ear / Eye / Accessory   (Image)
    ///     TopBar
    ///       PetNameText     (TMP_Text)
    ///       AffectionText   (TMP_Text)  "Lv.0 낯선 사이"
    ///       TimeTagText     (TMP_Text)  "아침 ☀"
    ///     BtnPetArea        (Button, 투명) — 탭 반응 트리거
    ///     NavBar            (NavBar)
    ///       BtnHome / BtnLocation / BtnAlbum / BtnHousing
    /// </summary>
    public class HomeScreen : BaseScreen
    {
        [Header("References")]
        [SerializeField] private HomePetView petView;
        [SerializeField] private NavBar      navBar;

        [Header("Top Bar")]
        [SerializeField] private TMP_Text txtPetName;
        [SerializeField] private TMP_Text txtAffection;
        [SerializeField] private TMP_Text txtTimeTag;

        [Header("Background")]
        [SerializeField] private Image bgImage;
        [SerializeField] private Color bgMorning = new(1.00f, 0.96f, 0.85f);
        [SerializeField] private Color bgNoon    = new(0.87f, 0.95f, 1.00f);
        [SerializeField] private Color bgEvening = new(1.00f, 0.82f, 0.65f);
        [SerializeField] private Color bgNight   = new(0.55f, 0.60f, 0.80f);

        [Header("Tab Screens (lazy — pushed on demand)")]
        [SerializeField] private LocationSelectScreen locationScreen;
        [SerializeField] private AlbumScreen          albumScreen;
        [SerializeField] private HousingScreen        housingScreen;

        [Header("Pet Tap Button")]
        [SerializeField] private Button btnPetArea;

        // ── 초기화 ───────────────────────────────────────────────
        protected override void Awake()
        {
            base.Awake();
            navBar.OnTabChanged += OnTabChanged;
            if (btnPetArea != null)
                btnPetArea.onClick.AddListener(OnPetTapped);
        }

        // ── 화면 진입 ────────────────────────────────────────────
        protected override void OnShow()
        {
            // 반려동물 외형 로드 + 아이들 시작
            petView.Load();
            petView.StartIdle();

            // 상단 정보 갱신
            RefreshTopBar();

            // 배경 색조
            ApplyTimeBg();

            // 홈 탭 하이라이트 (silent = 콜백 없이)
            navBar.SelectTab(NavBar.Tab.Home, silent: true);
        }

        protected override void OnHide()
        {
            petView.StopIdle();
        }

        // ── 상단 정보 ────────────────────────────────────────────
        private void RefreshTopBar()
        {
            var pet       = SaveManager.Instance.PetData;
            var affection = SaveManager.Instance.AffectionData;
            var levelData = MasterDataManager.Instance.GetAffectionLevel(affection.affectionLevel);

            txtPetName.text  = pet.name;
            txtAffection.text = levelData != null
                ? $"Lv.{affection.affectionLevel} {levelData.nameKo}"
                : $"Lv.{affection.affectionLevel}";

            txtTimeTag.text = GetTimeTagDisplay();
        }

        // ── 배경 ─────────────────────────────────────────────────
        private void ApplyTimeBg()
        {
            if (bgImage == null) return;
            bgImage.color = GetCurrentTimeTag() switch
            {
                Constants.TimeTags.Morning => bgMorning,
                Constants.TimeTags.Noon    => bgNoon,
                Constants.TimeTags.Evening => bgEvening,
                _                          => bgNight,
            };
        }

        private static string GetCurrentTimeTag()
        {
            int hour = System.DateTime.Now.Hour;
            if (hour >= 6  && hour < 12) return Constants.TimeTags.Morning;
            if (hour >= 12 && hour < 18) return Constants.TimeTags.Noon;
            if (hour >= 18 && hour < 21) return Constants.TimeTags.Evening;
            return Constants.TimeTags.Night;
        }

        private static string GetTimeTagDisplay() => GetCurrentTimeTag() switch
        {
            Constants.TimeTags.Morning => "Morning",
            Constants.TimeTags.Noon    => "Noon",
            Constants.TimeTags.Evening => "Evening",
            _                          => "Night",
        };

        // ── 탭 전환 ──────────────────────────────────────────────
        private void OnTabChanged(NavBar.Tab tab)
        {
            switch (tab)
            {
                case NavBar.Tab.Location:
                    if (locationScreen != null)
                        UIManager.Instance.Push(locationScreen);
                    break;
                case NavBar.Tab.Album:
                    if (albumScreen != null)
                        UIManager.Instance.Push(albumScreen);
                    break;
                case NavBar.Tab.Housing:
                    if (housingScreen != null)
                        UIManager.Instance.Push(housingScreen);
                    break;
            }
        }

        // ── 반려동물 탭 ──────────────────────────────────────────
        private void OnPetTapped()
        {
            petView.PlayTapReaction();
            // TODO: PHASE 6 — 일일 애정도 한도 내 포인트 적립
        }

        // ── 뒤로가기 차단 ─────────────────────────────────────────
        public override bool OnBackPressed() => true;
    }
}
