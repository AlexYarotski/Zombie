using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float _damage = 20;
    
    [SerializeField]
    private Transform _bulletPosition = null;

    private PoolManager _poolManager = null;

    private void OnEnable()
    {
        GameWindow.FireClicked += GameWindow_FireClicked;
    }

    private void OnDisable()
    {
        GameWindow.FireClicked -= GameWindow_FireClicked;
    }

    private void Start()
    {
        _poolManager = PoolManager.Instance;
    }

    private void GameWindow_FireClicked()
    {
        Fire();
    }
    
    private void Fire()
    {
        var bulletPrefab = _poolManager.GetObject<Bullet>(PooledType.Bullet, _bulletPosition.position);
        
        bulletPrefab.SetDamage(_damage);
        
        bulletPrefab.Fire(transform);
    }
}
