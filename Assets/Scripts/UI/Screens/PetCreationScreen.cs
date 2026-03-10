using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LittlePawTown.Data;
using LittlePawTown.Core;

namespace LittlePawTown.UI.Screens
{
    /// <summary>
    /// SCR-012 반려동물 외형 생성 화면.
    /// Hierarchy:
    ///   [PetCreationScreen]  CanvasGroup + PetCreationScreen
    ///     SafeArea (SafeAreaFitter)
    ///       Header / TitleText
    ///       PetPreview         (PetRenderer)
    ///       NameInputField     (TMP_InputField)
    ///       PartTabGroup       (TabGroup — Ear/Eye/Tail/Pattern/Accessory)
    ///         BtnEar / BtnEye / BtnTail / BtnPattern / BtnAccessory
    ///       PartSelector
    ///         BtnPrev  (Button)
    ///         PartNameText (TMP_Text)
    ///         BtnNext  (Button)
    ///       ColorSwatchGroup   (HorizontalLayoutGroup)
    ///         [Swatch ×N]      (Button + Image)
    ///       BottomButtons
    ///         BtnRandom  (Button)
    ///         BtnNext    (Button)
    /// </summary>
    public class PetCreationScreen : BaseScreen
    {
        [Header("References")]
        [SerializeField] private PetRenderer     petPreview;
        [SerializeField] private TMP_InputField  nameInput;

        [Header("Part Tabs")]
        [SerializeField] private Button btnTabEar;
        [SerializeField] private Button btnTabEye;
        [SerializeField] private Button btnTabTail;
        [SerializeField] private Button btnTabPattern;
        [SerializeField] private Button btnTabAccessory;

        [Header("Part Selector")]
        [SerializeField] private Button   btnPrev;
        [SerializeField] private Button   btnNext;
        [SerializeField] private TMP_Text partNameText;

        [Header("Color Swatches")]
        [SerializeField] private Transform   swatchGroup;
        [SerializeField] private Button      swatchPrefab;   // 런타임 Instantiate 용

        [Header("Bottom Buttons")]
        [SerializeField] private Button btnRandom;
        [SerializeField] private Button btnConfirm;

        [Header("Next Screen")]
        [SerializeField] private PetTraitScreen petTraitScreen;

        // ── 내부 상태 ────────────────────────────────────────────
        private PetCreationContext _ctx;
        private PartCategory       _activeCategory = PartCategory.Ear;
        private List<PartData>     _categoryParts  = new();
        private int                _partIndex;

        // 색상 팔레트 (MVP 고정 8색)
        private static readonly Color[] Palette =
        {
            new(1.00f, 0.85f, 0.65f),   // 크림
            new(0.85f, 0.65f, 0.45f),   // 카라멜
            new(0.55f, 0.35f, 0.20f),   // 초코
            new(0.20f, 0.15f, 0.10f),   // 블랙
            new(0.90f, 0.90f, 0.90f),   // 화이트
            new(0.70f, 0.55f, 0.45f),   // 그레이 브라운
            new(0.95f, 0.80f, 0.70f),   // 핑크 베이지
            new(0.65f, 0.50f, 0.80f),   // 라벤더
        };

        // ── 초기화 ───────────────────────────────────────────────
        protected override void Awake()
        {
            base.Awake();
            btnTabEar.onClick.AddListener(()      => SelectTab(PartCategory.Ear));
            btnTabEye.onClick.AddListener(()      => SelectTab(PartCategory.Eye));
            btnTabTail.onClick.AddListener(()     => SelectTab(PartCategory.Tail));
            btnTabPattern.onClick.AddListener(()  => SelectTab(PartCategory.Pattern));
            btnTabAccessory.onClick.AddListener(()=> SelectTab(PartCategory.Accessory));

            btnPrev.onClick.AddListener(OnPrev);
            btnNext.onClick.AddListener(OnNext);
            btnRandom.onClick.AddListener(OnRandom);
            btnConfirm.onClick.AddListener(OnConfirm);

            nameInput.onValueChanged.AddListener(v => { if (_ctx != null) _ctx.PetName = v; });
        }

        // ── 화면 진입 ────────────────────────────────────────────
        public void SetContext(PetCreationContext ctx) => _ctx = ctx;

        protected override void OnShow()
        {
            _ctx ??= new PetCreationContext();
            nameInput.text = _ctx.PetName;

            BuildSwatches();
            OnRandom();               // 첫 진입 시 랜덤 외형 적용
            SelectTab(PartCategory.Ear);
        }

        // ── 탭 선택 ──────────────────────────────────────────────
        private void SelectTab(PartCategory category)
        {
            _activeCategory = category;
            _categoryParts  = MasterDataManager.Instance.GetPartsByCategory(_ctx.SpeciesId, category);
            _partIndex      = FindCurrentIndex();
            RefreshPartSelector();
            RefreshSwatches();
        }

        private int FindCurrentIndex()
        {
            var currentId = GetCurrentPartId(_activeCategory);
            var idx = _categoryParts.FindIndex(p => p.id == currentId);
            return idx >= 0 ? idx : 0;
        }

        private string GetCurrentPartId(PartCategory cat) => cat switch
        {
            PartCategory.Ear       => _ctx.EarPartId,
            PartCategory.Eye       => _ctx.EyePartId,
            PartCategory.Tail      => _ctx.TailPartId,
            PartCategory.Pattern   => _ctx.PatternPartId,
            PartCategory.Accessory => _ctx.AccessoryPartId,
            _                      => "",
        };

        private void SetCurrentPartId(PartCategory cat, string id)
        {
            switch (cat)
            {
                case PartCategory.Ear:       _ctx.EarPartId       = id; break;
                case PartCategory.Eye:       _ctx.EyePartId       = id; break;
                case PartCategory.Tail:      _ctx.TailPartId      = id; break;
                case PartCategory.Pattern:   _ctx.PatternPartId   = id; break;
                case PartCategory.Accessory: _ctx.AccessoryPartId = id; break;
            }
        }

        // ── 파츠 선택 ────────────────────────────────────────────
        private void OnPrev()
        {
            if (_categoryParts.Count == 0) return;
            _partIndex = (_partIndex - 1 + _categoryParts.Count) % _categoryParts.Count;
            ApplyCurrentPart();
        }

        private void OnNext()
        {
            if (_categoryParts.Count == 0) return;
            _partIndex = (_partIndex + 1) % _categoryParts.Count;
            ApplyCurrentPart();
        }

        private void ApplyCurrentPart()
        {
            if (_categoryParts.Count == 0) return;
            var part = _categoryParts[_partIndex];
            SetCurrentPartId(_activeCategory, part.id);
            petPreview.SetPart(part);
            RefreshPartSelector();
        }

        private void RefreshPartSelector()
        {
            if (_categoryParts.Count == 0)
            {
                partNameText.text = "-";
                btnPrev.interactable = btnNext.interactable = false;
            }
            else
            {
                partNameText.text = _categoryParts[_partIndex].nameKo;
                btnPrev.interactable = btnNext.interactable = _categoryParts.Count > 1;
            }
        }

        // ── 색상 스와치 ──────────────────────────────────────────
        private void BuildSwatches()
        {
            // 최초 1회만 생성
            if (swatchGroup.childCount > 0) return;
            foreach (var col in Palette)
            {
                var btn = Instantiate(swatchPrefab, swatchGroup);
                var captured = col;
                btn.GetComponent<Image>().color = col;
                btn.onClick.AddListener(() => OnSwatchClicked(captured));
            }
        }

        private void RefreshSwatches()
        {
            // 현재 카테고리가 colorable 파츠를 갖고 있는지 확인
            bool hasColorable = _categoryParts.Count > 0 && _categoryParts[_partIndex].colorable;
            swatchGroup.gameObject.SetActive(hasColorable);
        }

        private void OnSwatchClicked(Color color)
        {
            switch (_activeCategory)
            {
                case PartCategory.Ear:     _ctx.EarColor     = color; break;
                case PartCategory.Tail:    _ctx.TailColor    = color; break;
                case PartCategory.Pattern: _ctx.PatternColor = color; break;
            }
            petPreview.SetPartColor(_activeCategory, color);
        }

        // ── 랜덤 ─────────────────────────────────────────────────
        private void OnRandom()
        {
            var master = MasterDataManager.Instance;
            foreach (PartCategory cat in System.Enum.GetValues(typeof(PartCategory)))
            {
                var parts = master.GetPartsByCategory(_ctx.SpeciesId, cat);
                if (parts == null || parts.Count == 0) continue;
                var part = parts[Random.Range(0, parts.Count)];
                SetCurrentPartId(cat, part.id);
                petPreview.SetPart(part);
            }
            // 바디 색상도 랜덤
            var bodyCol = Palette[Random.Range(0, Palette.Length)];
            _ctx.BodyColor = bodyCol;
            petPreview.SetBodyColor(bodyCol);

            // 탭 UI 갱신
            SelectTab(_activeCategory);
        }

        // ── 다음 화면 ────────────────────────────────────────────
        private void OnConfirm()
        {
            if (string.IsNullOrWhiteSpace(_ctx.PetName))
            {
                // TODO: 이름 입력 안내 토스트 (PHASE 2 UI polish)
                Debug.LogWarning("[PetCreation] 이름을 입력해주세요.");
                return;
            }

            petTraitScreen.SetContext(_ctx);
            UIManager.Instance.Push(petTraitScreen);
        }

        // ── 뒤로가기 ─────────────────────────────────────────────
        public override bool OnBackPressed()
        {
            UIManager.Instance.Pop();
            return true;
        }
    }
}
