using UnityEngine;

public abstract class Window : MonoBehaviour
{
    public abstract bool IsPopUp
    {
        get;
    }
    
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
