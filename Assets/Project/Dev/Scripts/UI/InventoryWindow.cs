using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : Window
{
    [SerializeField]
    private Button _button = null;
    [SerializeField]
    private InventorySlots _inventorySlots = null;

    public override bool IsPopUp => true;

    private void OnEnable()
    {
        Slot.ThrownOut += Slot_ThrownOut;
    }
    
    private void OnDisable()
    {
        Slot.ThrownOut -= Slot_ThrownOut;
    }

    private void Start()
    {
        _button.onClick.AddListener(Hide);
    }

    public override void Show()
    {
        base.Show();

        _inventorySlots.Open();
        
        Time.timeScale = 0;
    }

    public override void Hide()
    {
        base.Hide();

        _inventorySlots.Close();
        
        Time.timeScale = 1;
    }
    
    private void Slot_ThrownOut(ResourceType resourceType)
    {
        Show();
    }
}