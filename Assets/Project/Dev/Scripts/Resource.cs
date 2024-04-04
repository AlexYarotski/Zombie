using DG.Tweening;
using UnityEngine;

public class Resource : PooledBehaviour
{
    private const float MinDistance = 0.1f;
    
    [SerializeField]
    private ResourceType _resourceType = default;

    [SerializeField]
    private float _speedFlight = 5f;
    [SerializeField]
    private float _dieDelay = 0.3f;
    
    public ResourceType ResourceType => _resourceType;
    
    private Transform playerTransform;
    private bool isFlying = false;

    private void FixedUpdate()
    {
        if (isFlying && playerTransform != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position, 
                Time.deltaTime * _speedFlight);

            if (Vector3.Distance(transform.position, playerTransform.position) <= MinDistance)
            {
                FinishFlying();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        
        if (player != null && !isFlying && other as BoxCollider2D)
        {
            playerTransform = other.transform;
            
            isFlying = true;
            
            player.AddResource(_resourceType);
        }
    }
    
    private void FinishFlying()
    {
        transform.DOScale(Vector3.zero, _dieDelay).OnComplete(() =>  gameObject.SetActive(false));
    }
}
