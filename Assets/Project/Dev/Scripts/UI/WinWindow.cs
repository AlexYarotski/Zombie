using UnityEngine;
using UnityEngine.UI;

public class WinWindow : Window
{
    [SerializeField]
    private Button _next = null;
    
    public override bool IsPopUp => false;

    private void Start()
    {
        _next.onClick.AddListener(Next);
    }

    private void Next()
    {
        SceneLoader.Instance.LoadGame();
    }
}