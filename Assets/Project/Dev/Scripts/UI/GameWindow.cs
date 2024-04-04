using System;
using UnityEngine;
using UnityEngine.UI;

public class GameWindow : Window
{
    public static event Action FireClicked = delegate { };

    [SerializeField]
    private Button _fire = null;
    [SerializeField]
    private Button _inventory = null;

    [SerializeField]
    private FixedJoystick _joystick = null;

    private WindowSwitcher _windowSwitcher = null;

    public FixedJoystick Controller => _joystick;
    
    public override bool IsPopUp => false;

    private void Start()
    {
        _windowSwitcher = WindowSwitcher.Instance;
        
        _fire.onClick.AddListener(() => FireClicked());
        _inventory.onClick.AddListener(() => _windowSwitcher.Show<InventoryWindow>());
    }
}
