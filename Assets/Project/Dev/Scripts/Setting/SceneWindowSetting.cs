using UnityEngine;

[CreateAssetMenu(fileName = "SceneWindowSetting", menuName = "Settings/SceneWindowSetting", order = 0)]
public class SceneWindowSetting : ScriptableObject
{
    [SerializeField]
    private Window[] _windows = null;

    public Window[] GetWindows()
    {
        return _windows;
    }
}