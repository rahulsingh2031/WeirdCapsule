using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public event System.Action OnDeath;
    public float startingHealth;
    protected float health;
    protected bool isDead;

    protected virtual void Start()
    {
        health = startingHealth;
    }
    public virtual void takeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        takeDamage(damage);
    }
    public virtual void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }
    [ContextMenu("Self Destruct")]
    protected void Die()
    {
        isDead = true;
        OnDeath?.Invoke();
        GameObject.Destroy(this.gameObject);
    }


}
