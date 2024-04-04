using System.Collections;
using UnityEngine;

public class Bullet : PooledBehaviour
{
    [SerializeField]
    private float _dieDelay = 7;
    
    [SerializeField]
    private float _speed = 10;

    [SerializeField]
    private Rigidbody2D _rigidbody = null;

    private float _damage = 0;
    
    public void Fire(Transform weaponTransform)
    {
        var direction = weaponTransform.TransformDirection(Vector3.right);
        
        _rigidbody.velocity = direction.normalized * _speed;        
        
        transform.rotation = weaponTransform.rotation;

        StartCoroutine(DieTimer());
    }

    public void SetDamage(float value)
    {
        _damage = value;
    }

    private IEnumerator DieTimer()
    {
        var delay = new WaitForSeconds(_dieDelay);

        yield return delay;

        Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var enemy = other.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(_damage);
            
            Die();
        }
    }
}