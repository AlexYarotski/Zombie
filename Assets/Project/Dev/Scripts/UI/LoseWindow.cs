using UnityEngine;
using UnityEngine.UI;

public class LoseWindow : Window
{
    [SerializeField]
    private Button _restart = null;

    public override bool IsPopUp => false;

    private void Start()
    {
        _restart.onClick.AddListener(Restart);
    }

    private void Restart()
    {
        SceneLoader.Instance.LoadGame();
    }
}
