using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance = default(T);
    [SerializeField, Header("Keep it through levels")] bool keep = false;

    public static T Instance
    {
        get
        {
            if ((instance == null)) return default(T);
            return instance;
        }
    }

    protected virtual void Awake() => InitSingleton();

    void InitSingleton()
    {
        if (instance != null && instance != this)
            Destroy(this);
        if (keep)
        {
            DontDestroyOnLoad(this);
            instance = this as T;
            name = $" {typeof(T).Name}";
        }
    }
}
