using System.Collections.Generic;

public class Inventory : Singleton<Inventory>
{
    private readonly Dictionary<ResourceType, int> ResourceDictionary = new Dictionary<ResourceType, int>();

    public Dictionary<ResourceType, int> Resources => ResourceDictionary;

    protected override void SingleAwake()
    {
        
    }

    private void OnEnable()
    {
        Slot.ThrownOut += Slot_ThrownOut;
    }
    
    private void OnDisable()
    {
        Slot.ThrownOut -= Slot_ThrownOut;
    }

    public void AddResources(ResourceType resourceType)
    {
        if (!ResourceDictionary.TryAdd(resourceType, 1))
        {
            var value = ResourceDictionary[resourceType];

            ResourceDictionary[resourceType] = ++value;
        }
    }
    
    public void SetStartResources(ResourceType resourceType, int amount)
    {
        ResourceDictionary[resourceType] = amount;
    }
    
    private void Slot_ThrownOut(ResourceType resourceType)
    {
        ResourceDictionary[resourceType] -= 1;

        if (ResourceDictionary[resourceType] <= 0)
        {
            ResourceDictionary.Remove(resourceType);
        }
    }
}