using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DropResource : MonoBehaviour
{
    [Serializable]
    public class DropResourceConfig
    {
        [field: SerializeField]
        public Resource Resource
        {
            get; 
            private set;
        }

        [field: SerializeField]
        public int Count
        {
            get;
            private set;
        }
    }

    private const float DurationAnimation = 0.1f;
    
    private readonly List<Resource> ResourceList = new List<Resource>();
    
    private DropResourceConfig[] _dropResourceConfig = null;
    
    private void Start()
    {
        SetResource();
    }

    public void Drop()
    {
        Sequence dropSequence = DOTween.Sequence();
        foreach (var resourcePrefab in ResourceList)
        {
            resourcePrefab.gameObject.SetActive(true);
            resourcePrefab.transform.localScale = Vector3.zero;
            resourcePrefab.transform.parent = null;

            dropSequence.Append(resourcePrefab.transform.DOMoveY(0.5f, DurationAnimation).SetRelative()
                .SetEase(Ease.OutQuad));
            dropSequence.Join(resourcePrefab.transform.DOScale(Vector3.one * 1.5f, DurationAnimation));
            dropSequence.Append(resourcePrefab.transform.DOMoveY(-1f, DurationAnimation).SetRelative()
                .SetEase(Ease.InQuad));
            dropSequence.Join(resourcePrefab.transform.DOScale(Vector3.one, DurationAnimation));
        }
    }
    
    public void SetDrop(DropResourceConfig[] dropResourceConfigs)
    {
        _dropResourceConfig = dropResourceConfigs;
    }
    
    private void SetResource()
    {
        for (int i = 0; i < _dropResourceConfig.Length; i++)
        {
            for (int j = 0; j < _dropResourceConfig[i].Count; j++)
            {
                var resourcePrefab = Instantiate(_dropResourceConfig[i].Resource, transform);
                
                resourcePrefab.gameObject.SetActive(false);
                
                ResourceList.Add(resourcePrefab);
            }
        }
    }
}