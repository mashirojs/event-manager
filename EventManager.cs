using System;
using System.Collections.Generic;

namespace Mashirojs.Event
{
    public interface IEventListenerBase {}
    
    public interface IEventListener<in T> : IEventListenerBase where T : struct {
        void OnEvent(T e);
    }

    public static class EventListenerExtension
    {
        public static void AddListener<T>(this IEventListener<T> listener) where T : struct {
            EventManager.AddListener<T>(listener);
        }
        public static void RemoveListener<T>(this IEventListener<T> listener) where T : struct {
            EventManager.AddListener<T>(listener);
        }
    }

    public static class EventManager {
        private static Dictionary<Type, List<IEventListenerBase>> _listeners = new Dictionary<Type, List<IEventListenerBase>>();

        public static void AddListener<T>(IEventListener<T> listener) where T: struct 
        {
            if (!_listeners.ContainsKey(typeof(T)))
            {
                _listeners.Add(typeof(T), new List<IEventListenerBase>());
            }

            if (!_listeners[typeof(T)].Contains(listener))
            {
                _listeners[typeof(T)].Add(listener);
            }
        }

        public static void RemoveListener<T>(IEventListener<T> listener) where T: struct
        {
            _listeners[typeof(T)].Remove(listener);
        }

        public static void SendEvent<T>(T e) where T : struct
        {
            var listeners = _listeners[typeof(T)];
            foreach (var listener in listeners)
            {
                (listener as IEventListener<T>).OnEvent(e);
            }
        }
    }
}
