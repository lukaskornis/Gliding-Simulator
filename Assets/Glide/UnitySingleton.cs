using UnityEngine;

public abstract class UnitySingleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T instance
    {
        get
        {
            if (_instance != null) return _instance;
            
            _instance = GameObject.FindObjectOfType<T>();
            
            if (_instance != null) return _instance;
            
            GameObject obj = new(typeof(T).Name);
            _instance = obj.AddComponent<T>();
            DontDestroyOnLoad(obj);
            return _instance;
        }
    }
}