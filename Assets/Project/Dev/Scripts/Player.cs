using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Singleton<Player>, IDamageable
{
    public static event Action Died = delegate { };

    private readonly List<Enemy> EnemyList = new List<Enemy>();

    [SerializeField]
    private Hand _hand = null;

    [SerializeField]
    private Inventory _inventory = null;

    [Header("Health")]
    [SerializeField]
    private HealthBar _healthBar = null;
    [SerializeField]
    private float _health = 100;
    
    private NavMeshAgent _agent = null;
    private Transform _targetEnemy;

    public float Health => _health;

    protected override void SingleAwake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void Start()
    {
        SaveManager.Instance.Load();
    }

    private void Update()
    {
        if (_targetEnemy != null)
        {
            RotateHand();
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
        _health += amount;
        
        _healthBar.SetHealth(_health);
    }

    public void AddResource(ResourceType resourceType)
    {
        _inventory.AddResources(resourceType);
    }

    public void SetStartHealth(float health)
    {
        _health = health;
        
        _healthBar.SetHealth(_health);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            if (!EnemyList.Contains(enemy))
            {
                EnemyList.Add(enemy);
                SetTargetEnemy();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            if (EnemyList.Contains(enemy))
            {
                EnemyList.Remove(enemy);

                SetTargetEnemy();
            }
        }
    }

    private void SetTargetEnemy()
    {
        _targetEnemy = EnemyList.Count > 0 ? EnemyList[0].transform : null;
    }

    private void RotateHand()
    {
        Vector2 direction = _targetEnemy.position - transform.position;

        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

            _hand.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
    
    private void Die()
    {
        gameObject.SetActive(false);

        Died();
    }
}
