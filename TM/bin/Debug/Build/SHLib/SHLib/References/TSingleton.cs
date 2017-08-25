using UnityEngine;

namespace Mga.SHLib
{
    public class Singleton<T> where T : new()
    {
        private static readonly T s_instance = new T();
        public static T Instance { get { return s_instance; } }
    }

    public class SingletonResetable<T> where T : new()
    {
        private static T s_instance = new T();
        public static T Instance 
        {
            get
            {
                if (s_instance == null)
                    s_instance = new T();
                return s_instance; 
            } 
        }

        public static void ResetInstance()
        {
            s_instance = default(T);
        }
    }

    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : new()
    {
        private static T s_instance;
        public static T Instance { get { return s_instance; } }

        protected void SetInstance(T inst)
        {
            s_instance = inst;
        }
    }
} 