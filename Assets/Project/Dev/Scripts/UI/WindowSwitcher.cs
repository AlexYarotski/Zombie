using System.Collections.Generic;
using UnityEngine;

public class WindowSwitcher : Singleton<WindowSwitcher>
{
    private readonly List<Window> WindowList = new List<Window>();

    private Window _currentWindow = null;
    
    protected override void SingleAwake()
    {
        var windowArray = SceneContexts.Instance.SceneWindowSetting.GetWindows();

        for (var i = 0; i < windowArray.Length; i++)
        {
            var newWindow = Instantiate(windowArray[i], transform);
            
            newWindow.gameObject.SetActive(false);
            
            WindowList.Add(newWindow);
        }
        
        Show<GameWindow>();
    }

    private void OnEnable()
    {
        Player.Died += Player_Died;
    }
    
    private void OnDisable()
    {
        Player.Died -= Player_Died;
    }

    private void Player_Died()
    {
        Show<LoseWindow>();
    }

    public void Show<T>() where T : Window
    {
        var windowToShow = GetWindow<T>();

        if (_currentWindow != null && !windowToShow.IsPopUp)
        {
            _currentWindow.Hide();
        }

        if (!windowToShow.IsPopUp)
        {
            _currentWindow = windowToShow;
        }
        
        windowToShow.Show();
    }
    
    public Window GetWindow<T>() where T : Window
    {
        foreach(var window in WindowList)
        {
            if(window is T)
            {
                return window;
            }
        }

#if UNITY_EDITOR
        Debug.LogError($"Window type not found {typeof(T)}"); 
#endif
        
        return null;
    }
}
