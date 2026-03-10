using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LittlePawTown.UI.Screens
{
    /// <summary>
    /// SCR-013 반려동물 성향/특성 설정 화면.
    /// 5가지 특성(호기심·활동성·사교성·식욕·신중함)을 1~3 단계로 설정한다.
    /// Hierarchy:
    ///   [PetTraitScreen]  CanvasGroup + PetTraitScreen
    ///     SafeArea
    ///       Header / TitleText
    ///       TraitList
    ///         TraitRow_Curiosity    (TraitRow)
    ///         TraitRow_Activity
    ///         TraitRow_Sociability
    ///         TraitRow_Appetite
    ///         TraitRow_Caution
    ///       DescriptionText
    ///       BottomButtons
    ///         BtnBack    (Button)
    ///         BtnConfirm (Button)
    ///
    /// 각 TraitRow 구조:
    ///   TraitRow
    ///     LabelText   (TMP_Text)
    ///     BtnMinus    (Button)
    ///     ValueText   (TMP_Text)   "1" / "2" / "3"
    ///     BtnPlus     (Button)
    /// </summary>
    public class PetTraitScreen : BaseScreen
    {
        [Header("Trait Rows — Curiosity")]
        [SerializeField] private Button   btnCuriosityMinus;
        [SerializeField] private Button   btnCuriosityPlus;
        [SerializeField] private TMP_Text txtCuriosityValue;

        [Header("Trait Rows — Activity")]
        [SerializeField] private Button   btnActivityMinus;
        [SerializeField] private Button   btnActivityPlus;
        [SerializeField] private TMP_Text txtActivityValue;

        [Header("Trait Rows — Sociability")]
        [SerializeField] private Button   btnSociabilityMinus;
        [SerializeField] private Button   btnSociabilityPlus;
        [SerializeField] private TMP_Text txtSociabilityValue;

        [Header("Trait Rows — Appetite")]
        [SerializeField] private Button   btnAppetiteMinus;
        [SerializeField] private Button   btnAppetitePlus;
        [SerializeField] private TMP_Text txtAppetiteValue;

        [Header("Trait Rows — Caution")]
        [SerializeField] private Button   btnCautionMinus;
        [SerializeField] private Button   btnCautionPlus;
        [SerializeField] private TMP_Text txtCautionValue;

        [Header("Misc")]
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Button   btnBack;
        [SerializeField] private Button   btnConfirm;

        [Header("Next Screen")]
        [SerializeField] private PetCreationCompleteScreen completeScreen;

        private PetCreationContext _ctx;

        // ── 초기화 ───────────────────────────────────────────────
        protected override void Awake()
        {
            base.Awake();
            btnCuriosityMinus.onClick.AddListener(()    => Adjust(TraitKey.Curiosity,   -1));
            btnCuriosityPlus.onClick.AddListener(()     => Adjust(TraitKey.Curiosity,   +1));
            btnActivityMinus.onClick.AddListener(()     => Adjust(TraitKey.Activity,    -1));
            btnActivityPlus.onClick.AddListener(()      => Adjust(TraitKey.Activity,    +1));
            btnSociabilityMinus.onClick.AddListener(()  => Adjust(TraitKey.Sociability, -1));
            btnSociabilityPlus.onClick.AddListener(()   => Adjust(TraitKey.Sociability, +1));
            btnAppetiteMinus.onClick.AddListener(()     => Adjust(TraitKey.Appetite,    -1));
            btnAppetitePlus.onClick.AddListener(()      => Adjust(TraitKey.Appetite,    +1));
            btnCautionMinus.onClick.AddListener(()      => Adjust(TraitKey.Caution,     -1));
            btnCautionPlus.onClick.AddListener(()       => Adjust(TraitKey.Caution,     +1));

            btnBack.onClick.AddListener(OnBack);
            btnConfirm.onClick.AddListener(OnConfirm);
        }

        // ── 화면 진입 ────────────────────────────────────────────
        public void SetContext(PetCreationContext ctx) => _ctx = ctx;

        protected override void OnShow()
        {
            RefreshAll();
        }

        // ── 값 조정 ──────────────────────────────────────────────
        private void Adjust(TraitKey key, int delta)
        {
            const int min = 1, max = 3;
            switch (key)
            {
                case TraitKey.Curiosity:
                    _ctx.Curiosity   = Mathf.Clamp(_ctx.Curiosity   + delta, min, max); break;
                case TraitKey.Activity:
                    _ctx.Activity    = Mathf.Clamp(_ctx.Activity    + delta, min, max); break;
                case TraitKey.Sociability:
                    _ctx.Sociability = Mathf.Clamp(_ctx.Sociability + delta, min, max); break;
                case TraitKey.Appetite:
                    _ctx.Appetite    = Mathf.Clamp(_ctx.Appetite    + delta, min, max); break;
                case TraitKey.Caution:
                    _ctx.Caution     = Mathf.Clamp(_ctx.Caution     + delta, min, max); break;
            }
            RefreshAll();
        }

        private void RefreshAll()
        {
            SetRow(txtCuriosityValue,   btnCuriosityMinus,   btnCuriosityPlus,   _ctx.Curiosity);
            SetRow(txtActivityValue,    btnActivityMinus,    btnActivityPlus,    _ctx.Activity);
            SetRow(txtSociabilityValue, btnSociabilityMinus, btnSociabilityPlus, _ctx.Sociability);
            SetRow(txtAppetiteValue,    btnAppetiteMinus,    btnAppetitePlus,    _ctx.Appetite);
            SetRow(txtCautionValue,     btnCautionMinus,     btnCautionPlus,     _ctx.Caution);
        }

        private static void SetRow(TMP_Text label, Button minus, Button plus, int value)
        {
            label.text            = value.ToString();
            minus.interactable    = value > 1;
            plus.interactable     = value < 3;
        }

        // ── 버튼 ─────────────────────────────────────────────────
        private void OnBack()
        {
            UIManager.Instance.Pop();
        }

        private void OnConfirm()
        {
            completeScreen.SetContext(_ctx);
            UIManager.Instance.Push(completeScreen);
        }

        public override bool OnBackPressed()
        {
            OnBack();
            return true;
        }

        // ── 내부 Enum ─────────────────────────────────────────────
        private enum TraitKey { Curiosity, Activity, Sociability, Appetite, Caution }
    }
}
