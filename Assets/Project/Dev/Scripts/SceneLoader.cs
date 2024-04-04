using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    private const string Game = "Game";

    [SerializeField]
    private WindowSwitcher _windowSwitcher = null;
    
    protected override void SingleAwake()
    {
    }
    
    public void LoadGame()
    {
        DOTween.KillAll();

        UploadSceneAsync(Game);
        
        _windowSwitcher.Show<GameWindow>();
    }

    private void UploadSceneAsync(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}

