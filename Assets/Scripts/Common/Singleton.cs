using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance != null) return _instance;
           
            T[] instances = FindObjectsOfType<T>();
            int count = instances.Length;
            if (count > 0)
            {
                if (count == 1) return _instance = instances[0];
                for (int i = 1; i < instances.Length; i++) Destroy(instances[i]);
                return _instance = instances[0];
            }
            return _instance = new GameObject(typeof(T).Name).AddComponent<T>();
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}