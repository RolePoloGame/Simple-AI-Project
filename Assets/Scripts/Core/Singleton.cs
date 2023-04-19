using UnityEngine;

namespace Assets.Scripts.Core
{
    /// <summary>
    /// Abstract class for Singletons. Can be used to call all generic Singletons . 
    /// </summary>
    public abstract class Singleton : EnhancedBehaviour
    {
        public bool IsInitialized => m_Init;
        protected bool m_Init = false;
        /// <summary>
        /// Performs necessary initialization for the Singleton. By default it only logs initialization.
        /// </summary>
        public virtual void OnInitialize() => Log($"{name} is initialized.");
    }
    public class Singleton<T> : Singleton where T : Component
    {
        public static T Instance { get; protected set; }

        protected virtual void Awake() => InstantiateSingleton();

        private void InstantiateSingleton()
        {
            if (Instance == this) return;

            if (Instance != null)
            {
                Log($"Instance of {name} already exists! Deleting duplicate...");
                Destroy(this);
                return;
            }

            Instance = this as T;
            OnInitialize();
            m_Init = true;
        }
    }
}