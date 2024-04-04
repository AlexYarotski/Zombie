using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public event Action<Enemy> Died = delegate { };

    [Header("Health")]
    [SerializeField]
    private HealthBar _healthBar = null;
    [SerializeField]
    private float _health = 80;

    [SerializeField]
    private float _damage = 40;

    [SerializeField]
    private float _delayDamage = 1f;

    private DropResource _dropResource = null;
    private Player _player = null;
    private NavMeshAgent _agent = null;
    
    public float Health => _health;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    private void Update()
    {
        if (_player != null)
        {
            _agent.SetDestination(_player.transform.position);
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        
        _healthBar.SetHealth(_health);

        if (_health <=  0)
        {
            Die();
        }
    }

    public void AddHealth(float amount)
    {
    }

    public void SetDrop(DropResource dropResource)
    {
        _dropResource = dropResource;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();

        if (player != null && other as CircleCollider2D)
        {
            _player = player;
        }
        
        if (player != null && other as BoxCollider2D)
        {
            _player = player;

            StartCoroutine(TakeDamage());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        
        if (player != null && other as BoxCollider2D)
        {
            _player = player;
            
            StopCoroutine(TakeDamage());
        }
    }

    private IEnumerator TakeDamage()
    {
        var delay = new WaitForSeconds(_delayDamage);
        
        _player.TakeDamage(_damage);

        yield return delay;
    }
    
    private void Die()
    {
        _dropResource.Drop();

        Died(this);
        
        gameObject.SetActive(false);
    }
}
