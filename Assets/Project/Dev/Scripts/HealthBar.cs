using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image _imageFiller = null;

    public void SetHealth(float health)
    {
        var value = health / 100;
        
        _imageFiller.fillAmount = value <= 0 ? 0 : value;
    }
}
