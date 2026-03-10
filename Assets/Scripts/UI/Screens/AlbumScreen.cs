using UnityEngine;
using UnityEngine.UI;

namespace LittlePawTown.UI.Screens
{
    /// <summary>
    /// SCR-050 추억 앨범 화면 — PHASE 7 구현 예정.
    /// 현재는 뒤로가기 버튼만 있는 플레이스홀더.
    /// </summary>
    public class AlbumScreen : BaseScreen
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
