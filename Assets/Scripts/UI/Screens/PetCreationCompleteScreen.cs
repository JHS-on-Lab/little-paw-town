using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using LittlePawTown.Core;
using LittlePawTown.Data;

namespace LittlePawTown.UI.Screens
{
    /// <summary>
    /// SCR-014 반려동물 생성 완료 연출 화면.
    /// 이름 호명 텍스트 + PetRenderer 등장 애니메이션 + "시작하기" 버튼.
    /// Hierarchy:
    ///   [PetCreationCompleteScreen]  CanvasGroup + PetCreationCompleteScreen
    ///     SafeArea
    ///       PetPreview         (PetRenderer)
    ///       GreetingText       (TMP_Text)   "안녕, {이름}!"
    ///       SubText            (TMP_Text)   "함께해줘서 고마워요 :)"
    ///       BtnStart           (Button)
    /// </summary>
    public class PetCreationCompleteScreen : BaseScreen
    {
        [Header("References")]
        [SerializeField] private PetRenderer petPreview;
        [SerializeField] private TMP_Text    greetingText;
        [SerializeField] private TMP_Text    subText;
        [SerializeField] private Button      btnStart;

        [Header("Animation")]
        [SerializeField] private float petPopDuration   = 0.45f;
        [SerializeField] private float textFadeDelay    = 0.3f;
        [SerializeField] private float textFadeDuration = 0.35f;

        private PetCreationContext _ctx;

        // ── 초기화 ───────────────────────────────────────────────
        protected override void Awake()
        {
            base.Awake();
            btnStart.onClick.AddListener(OnStart);
        }

        // ── 화면 진입 ────────────────────────────────────────────
        public void SetContext(PetCreationContext ctx) => _ctx = ctx;

        protected override void OnShow()
        {
            // 외형 적용
            ApplyPreview();

            // 텍스트 초기 숨김
            greetingText.text  = $"안녕, {_ctx.PetName}!";
            subText.text       = "함께해줘서 고마워요 :)";
            SetTextAlpha(greetingText, 0f);
            SetTextAlpha(subText,      0f);

            // 애니메이션 시작
            StopAllCoroutines();
            StartCoroutine(PlayIntroAnimation());
        }

        // ── 외형 적용 ─────────────────────────────────────────────
        private void ApplyPreview()
        {
            var master = MasterDataManager.Instance;
            petPreview.SetBodyColor(_ctx.BodyColor);

            ApplyPart(_ctx.EarPartId,       master);
            ApplyPart(_ctx.EyePartId,       master);
            ApplyPart(_ctx.TailPartId,      master);
            ApplyPart(_ctx.PatternPartId,   master);
            ApplyPart(_ctx.AccessoryPartId, master);

            // 색상 오버라이드
            petPreview.SetPartColor(PartCategory.Ear,     _ctx.EarColor);
            petPreview.SetPartColor(PartCategory.Tail,    _ctx.TailColor);
            petPreview.SetPartColor(PartCategory.Pattern, _ctx.PatternColor);
        }

        private void ApplyPart(string partId, MasterDataManager master)
        {
            if (string.IsNullOrEmpty(partId)) return;
            var part = master.GetPart(partId);
            if (part != null) petPreview.SetPart(part);
        }

        // ── 등장 애니메이션 ────────────────────────────────────────
        private IEnumerator PlayIntroAnimation()
        {
            // Pet 팝업: scale 0 → 1
            var petTransform = petPreview.transform;
            petTransform.localScale = Vector3.zero;
            float elapsed = 0f;
            while (elapsed < petPopDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                float t  = Mathf.Clamp01(elapsed / petPopDuration);
                // 약간 오버슛 느낌 (간이 spring)
                float scale = t < 0.8f
                    ? Mathf.Lerp(0f, 1.1f, t / 0.8f)
                    : Mathf.Lerp(1.1f, 1.0f, (t - 0.8f) / 0.2f);
                petTransform.localScale = Vector3.one * scale;
                yield return null;
            }
            petTransform.localScale = Vector3.one;

            // 텍스트 페이드인
            yield return new WaitForSecondsRealtime(textFadeDelay);
            yield return StartCoroutine(FadeInText(greetingText));
            yield return StartCoroutine(FadeInText(subText));
        }

        private IEnumerator FadeInText(TMP_Text text)
        {
            float elapsed = 0f;
            while (elapsed < textFadeDuration)
            {
                elapsed += Time.unscaledDeltaTime;
                SetTextAlpha(text, Mathf.Clamp01(elapsed / textFadeDuration));
                yield return null;
            }
            SetTextAlpha(text, 1f);
        }

        private static void SetTextAlpha(TMP_Text text, float alpha)
        {
            var c   = text.color;
            c.a     = alpha;
            text.color = c;
        }

        // ── 시작 ─────────────────────────────────────────────────
        private void OnStart()
        {
            SavePetData();
            SceneController.Instance.GoToGame();
        }

        private void SavePetData()
        {
            var save   = SaveManager.Instance;
            var petData = save.PetData;
            petData.petId     = System.Guid.NewGuid().ToString("N")[..8];
            petData.name      = _ctx.PetName;
            petData.speciesId = _ctx.SpeciesId;
            petData.createdAt = System.DateTime.UtcNow.ToString("o");

            var app  = _ctx.ToAppearanceSaveData();
            var trait= _ctx.ToTraitSaveData();

            // SaveManager 에 직접 덮어쓰기
            save.AllData.pet        = petData;
            save.AllData.appearance = app;
            save.AllData.trait      = trait;

            // 튜토리얼 완료 처리 (생성 자체가 온보딩 완료)
            save.PlayerData.tutorialCleared = true;

            save.Save();

            EventBus.Publish(new Events.PetCreatedEvent
            {
                PetId     = petData.petId,
                PetName   = petData.name,
                SpeciesId = petData.speciesId,
            });
        }

        // ── 뒤로가기 차단 ─────────────────────────────────────────
        public override bool OnBackPressed() => true;
    }
}
