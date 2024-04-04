using System.Linq;
using UnityEngine;

public class InventorySlots : MonoBehaviour
{ 
    private Slot[] _slots = null;
    private Inventory _inventory = null;
    
    private void Start()
    {
        SetProperty();
    }
    
    public void Open()
    {
        SetProperty();

        SetSlot();
    }

    public void Close()
    {
        
    }

    private void SetProperty()
    {
        if (_inventory == null)
        {
            _inventory = Inventory.Instance;
        }

        _inventory ??= Inventory.Instance;

        _slots ??= GetComponentsInChildren<Slot>();
    }

    private void SetSlot()
    {
        var resourcesCount = _inventory.Resources.Count;
        var keysArray = _inventory.Resources.Keys.ToArray();

        for (var i = 0; i < _slots.Length; i++)
        {
            if (resourcesCount > i)
            {
                var resourceType = keysArray[i];
                var count = _inventory.Resources[resourceType];

                _slots[i].Enable(resourceType, count);
            }
            else
            {
                _slots[i].Disable();
            }
        }
    }
}
