using System;
using System.Collections.Generic;
using UnityEngine;

namespace LittlePawTown.Core
{
    /// <summary>
    /// 타입 안전 Pub/Sub 이벤트 버스.
    /// MonoBehaviour 의존 없이 어디서든 사용 가능.
    ///
    /// 사용 예:
    ///   EventBus.Subscribe&lt;Events.AffectionChangedEvent&gt;(OnAffectionChanged);
    ///   EventBus.Publish(new Events.AffectionChangedEvent { Delta = 5 });
    ///   EventBus.Unsubscribe&lt;Events.AffectionChangedEvent&gt;(OnAffectionChanged);
    /// </summary>
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<Delegate>> _handlers = new();

        public static void Subscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            if (!_handlers.TryGetValue(type, out var list))
            {
                list = new List<Delegate>();
                _handlers[type] = list;
            }
            list.Add(handler);
        }

        public static void Unsubscribe<T>(Action<T> handler)
        {
            var type = typeof(T);
            if (_handlers.TryGetValue(type, out var list))
                list.Remove(handler);
        }

        public static void Publish<T>(T eventData)
        {
            var type = typeof(T);
            if (!_handlers.TryGetValue(type, out var list)) return;

            // 복사 후 순회 — 핸들러 내에서 Subscribe/Unsubscribe 해도 안전
            var snapshot = new List<Delegate>(list);
            foreach (var handler in snapshot)
            {
                try
                {
                    ((Action<T>)handler).Invoke(eventData);
                }
                catch (Exception e)
                {
                    Debug.LogError($"[EventBus] Exception in handler for {type.Name}: {e}");
                }
            }
        }

        /// <summary>씬 전환 시 구독자 전부 정리할 때 사용.</summary>
        public static void Clear<T>()
        {
            _handlers.Remove(typeof(T));
        }

        public static void ClearAll()
        {
            _handlers.Clear();
        }
    }
}
