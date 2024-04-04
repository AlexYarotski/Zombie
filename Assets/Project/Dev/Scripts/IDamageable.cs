public interface IDamageable
{
    public float Health
    {
        get;
    }
    
    public void TakeDamage(float damage);
    public void AddHealth(float amount);
}
