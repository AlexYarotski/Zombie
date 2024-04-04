using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    _instance = new GameObject().GetComponent<T>();
                }
            }

            return _instance;
        }
    }

    protected void Awake()
    {
        if (_instance == this)
        {
            Destroy(gameObject);
        }
        else
        {
            SingleAwake();
        }
    }

    protected abstract void SingleAwake();
}