using UnityEngine;
using UnityEngine.UI;

namespace LittlePawTown.UI.Screens
{
    /// <summary>
    /// 하우징 화면 — PHASE 9 구현 예정.
    /// 현재는 뒤로가기 버튼만 있는 플레이스홀더.
    /// </summary>
    public class HousingScreen : BaseScreen
    {
        [SerializeField] private Button btnBack;

        protected override void Awake()
        {
            base.Awake();
            if (btnBack != null)
                btnBack.onClick.AddListener(() => UIManager.Instance.Pop());
        }

        public override bool OnBackPressed()
        {
            UIManager.Instance.Pop();
            return true;
        }
    }
}
