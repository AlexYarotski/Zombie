using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public static event Action<ResourceType> ThrownOut = delegate { };

    [SerializeField]
    private Button _slotButton = null;
    [SerializeField]
    private Button _delleteButton = null;
    
    [SerializeField]
    private Image _image = null;
    
    [SerializeField]
    private TextMeshProUGUI _countTMP = null;

    private ResourceSetting _resourceSetting = null;
    private ResourceType _resourceType = default;
    private int _count = 0;

    private void Start()
    {
        _delleteButton.onClick.AddListener(DeleteResource);
        _slotButton.onClick.AddListener(SetActiveDelete);
        
        _delleteButton.gameObject.SetActive(false);
    }

    public void Enable(ResourceType resourceType, int count)
    {
        _resourceType = resourceType;
        _count = count;

        SetResourceSetting();

        SetResource();

        _delleteButton.gameObject.SetActive(false);
    }

    public void Disable()
    {
        _image.gameObject.SetActive(false);
        _countTMP.gameObject.SetActive(false);
        _delleteButton.gameObject.SetActive(false);

        _count = 0;
    }
    
    private void SetResourceSetting()
    {
        if (_resourceSetting == null)
        {
            _resourceSetting = SceneContexts.Instance.ResourceSetting;
        }
    }

    private void DeleteResource()
    {
        ThrownOut(_resourceType);
    }

    private void SetResource()
    {
        _image.sprite = _resourceSetting.GetResourceSprite(_resourceType);
        _image.gameObject.SetActive(true);

        if (_count > 1)
        {
            _countTMP.text = _count.ToString();
            _countTMP.gameObject.SetActive(true);
        }
        else
        {
            _countTMP.gameObject.SetActive(false);
        }
    }

    private void SetActiveDelete()
    {
        if (_count > 0)
        {
            _delleteButton.gameObject.SetActive(!_delleteButton.gameObject.activeSelf);
        }
    }
}
