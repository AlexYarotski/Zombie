using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceSetting", menuName = "Settings/ResourceSetting", order = 0)]
public class ResourceSetting : ScriptableObject
{
    [Serializable]
    public class ResourceConfig
    {
        [field: SerializeField]
        public ResourceType ResourceType
        {
            get;
            private set;
        }
        
        [field: SerializeField]
        public Sprite ResourceSprite
        {
            get;
            private set;
        }
        
        [field: SerializeField]
        public Resource Resource
        {
            get;
            private set;
        }
    }
    
    [SerializeField]
    private ResourceConfig[] _resourceConfigs = null;

    public Resource GetResource(ResourceType resourceType)
    {
        for (int i = 0; i < _resourceConfigs.Length; i++)
        {
            if (_resourceConfigs[i].ResourceType == resourceType)
            {
                return _resourceConfigs[i].Resource;
            }
        }
        
        Debug.LogError($"Resource with type {resourceType} is not found!");

        
        return null;
    }
    
    public Sprite GetResourceSprite(ResourceType resourceType)
    {
        for (int i = 0; i < _resourceConfigs.Length; i++)
        {
            if (_resourceConfigs[i].ResourceType == resourceType)
            {
                return _resourceConfigs[i].ResourceSprite;
            }
        }
        
        Debug.LogError($"Resource image with type {resourceType} is not found!");

        
        return null;
    }
}