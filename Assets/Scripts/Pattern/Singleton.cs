using UnityEngine;

namespace Pattern
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance = null;

        public static T Instance
        {
            get
            {
                if (!instance)
                    instance = FindObjectOfType<T>();

                return instance;
            }
        }

        protected void AssignInstance(T _instance)
        {
            if (!instance)
                instance = _instance;
        }

        protected void UnassignInstance()
        {
            instance = null;
        }
    }
}