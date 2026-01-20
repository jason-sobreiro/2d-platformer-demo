using UnityEngine;

namespace Scripts.Utilities.Singletons
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {

        public static T Instance { get; protected set; }

        protected virtual void Awake()
        {

            // Singleton pattern implementation
            if (Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }

        }
    }

    public class NonPersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
    {

        protected override void Awake()
        {

            // Singleton pattern implementation
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

        }
    }

}