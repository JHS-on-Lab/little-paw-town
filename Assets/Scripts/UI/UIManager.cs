using System.Collections.Generic;
using UnityEngine;
using LittlePawTown.Utils;

namespace LittlePawTown.UI
{
    /// <summary>
    /// 화면 스택 관리. DontDestroyOnLoad 싱글톤.
    /// - Push: 새 화면을 스택에 쌓고 이전 화면을 숨김
    /// - Pop : 현재 화면을 닫고 이전 화면을 복원
    /// - Replace: 현재 화면을 교체 (스택 깊이 유지)
    /// </summary>
    public class UIManager : Singleton<UIManager>
    {
        private readonly Stack<BaseScreen> _screenStack = new();

        // ── Public API ─────────────────────────────────────────
        /// <summary>새 화면을 스택에 쌓는다. 이전 화면은 숨김.</summary>
        public void Push(BaseScreen screen, bool instant = false)
        {
            if (_screenStack.TryPeek(out var current))
                current.Hide(instant);

            _screenStack.Push(screen);
            screen.Show(instant);
        }

        /// <summary>현재 화면을 닫고 이전 화면으로 돌아간다.</summary>
        public void Pop(bool instant = false)
        {
            if (_screenStack.Count == 0) return;

            var top = _screenStack.Pop();
            top.Hide(instant);

            if (_screenStack.TryPeek(out var prev))
                prev.Show(instant);
        }

        /// <summary>현재 화면을 닫고 새 화면으로 교체. 스택 깊이 유지.</summary>
        public void Replace(BaseScreen screen, bool instant = false)
        {
            if (_screenStack.Count > 0)
            {
                var top = _screenStack.Pop();
                top.Hide(instant);
            }

            _screenStack.Push(screen);
            screen.Show(instant);
        }

        /// <summary>스택을 전부 비우고 새 화면을 최초로 표시. 씬 전환 후 사용.</summary>
        public void SetRoot(BaseScreen screen, bool instant = true)
        {
            while (_screenStack.Count > 0)
            {
                var s = _screenStack.Pop();
                s.Hide(true);
            }

            _screenStack.Push(screen);
            screen.Show(instant);
        }

        /// <summary>현재 최상단 화면.</summary>
        public BaseScreen Current => _screenStack.TryPeek(out var s) ? s : null;

        /// <summary>스택 깊이.</summary>
        public int Depth => _screenStack.Count;

        // ── Android 뒤로가기 ───────────────────────────────────
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                HandleBackPressed();
        }

        private void HandleBackPressed()
        {
            if (Current != null && Current.OnBackPressed())
                return;  // 화면이 자체 처리

            if (_screenStack.Count > 1)
                Pop();
            // 스택이 1 이하면 아무것도 안 함 (앱 종료는 하지 않음)
        }
    }
}
