using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField]
    private DontDestroyElement[] _dotnDestroyElements = null;

    private void Start()
    {
        for (int i = 0; i < _dotnDestroyElements.Length; i++)
        {
            _dotnDestroyElements[i].transform.SetParent(null);
            
            DontDestroyOnLoad(_dotnDestroyElements[i]);
        }
        
        SceneLoader.Instance.LoadGame();
    }
}
