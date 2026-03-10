using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LittlePawTown.Data;
using LittlePawTown.Core;

namespace LittlePawTown.UI
{
    /// <summary>
    /// 반려동물 외형 미리보기용 UI 렌더러.
    /// 자식 Image 들을 파츠 카테고리별로 관리하고 Sprite / Color 를 교체한다.
    /// Hierarchy:
    ///   [PetRenderer]  PetRenderer 컴포넌트
    ///     Body         Image  (베이스 바디)
    ///     Pattern      Image
    ///     Tail         Image
    ///     Ear          Image
    ///     Eye          Image
    ///     Accessory    Image
    /// </summary>
    public class PetRenderer : MonoBehaviour
    {
        [Header("Layer Images")]
        [SerializeField] private Image imgBody;
        [SerializeField] private Image imgPattern;
        [SerializeField] private Image imgTail;
        [SerializeField] private Image imgEar;
        [SerializeField] private Image imgEye;
        [SerializeField] private Image imgAccessory;

        // 현재 적용된 파츠 (카테고리 → PartData)
        private readonly Dictionary<PartCategory, PartData> _currentParts = new();
        // 카테고리별 색상 오버라이드
        private readonly Dictionary<PartCategory, Color> _colorOverrides = new();

        private Color _baseBodyColor = Color.white;

        // ── Public API ──────────────────────────────────────────────

        /// <summary>베이스 바디 색상 설정.</summary>
        public void SetBodyColor(Color color)
        {
            _baseBodyColor = color;
            if (imgBody != null) imgBody.color = color;
        }

        /// <summary>특정 카테고리 파츠를 교체한다.</summary>
        public void SetPart(PartData part)
        {
            if (part == null) return;
            _currentParts[part.partCategory] = part;
            var img = GetImage(part.partCategory);
            if (img == null) return;

            img.sprite  = part.sprite;
            img.enabled = part.sprite != null;

            // 색상 오버라이드가 있으면 적용, 없으면 흰색
            img.color = _colorOverrides.TryGetValue(part.partCategory, out var c) ? c : Color.white;
        }

        /// <summary>특정 카테고리 파츠를 숨긴다.</summary>
        public void ClearPart(PartCategory category)
        {
            _currentParts.Remove(category);
            var img = GetImage(category);
            if (img != null) img.enabled = false;
        }

        /// <summary>colorable 파츠에 색상 오버라이드를 적용한다.</summary>
        public void SetPartColor(PartCategory category, Color color)
        {
            _colorOverrides[category] = color;
            if (_currentParts.TryGetValue(category, out var part) && part.colorable)
            {
                var img = GetImage(category);
                if (img != null) img.color = color;
            }
        }

        /// <summary>저장된 외형 데이터로 전체 복원.</summary>
        public void ApplyAppearance(PetAppearanceSaveData data, MasterDataManager master)
        {
            if (ColorUtility.TryParseHtmlString(data.baseColor, out var bodyCol))
                SetBodyColor(bodyCol);

            ApplyPartById(data.earPartId,       master);
            ApplyPartById(data.eyePartId,       master);
            ApplyPartById(data.tailPartId,      master);
            ApplyPartById(data.patternPartId,   master);
            ApplyPartById(data.accessoryPartId, master);
        }

        // ── 내부 ────────────────────────────────────────────────────

        private void ApplyPartById(string partId, MasterDataManager master)
        {
            if (string.IsNullOrEmpty(partId)) return;
            var part = master.GetPart(partId);
            if (part != null) SetPart(part);
        }

        private Image GetImage(PartCategory category) => category switch
        {
            PartCategory.Ear       => imgEar,
            PartCategory.Eye       => imgEye,
            PartCategory.Tail      => imgTail,
            PartCategory.Pattern   => imgPattern,
            PartCategory.Accessory => imgAccessory,
            _                      => null,
        };

        // ── 현재 상태 읽기 ──────────────────────────────────────────

        public Color BodyColor => _baseBodyColor;

        public string GetPartId(PartCategory category) =>
            _currentParts.TryGetValue(category, out var p) ? p.id : "";

        public Color GetPartColor(PartCategory category) =>
            _colorOverrides.TryGetValue(category, out var c) ? c : Color.white;
    }
}
