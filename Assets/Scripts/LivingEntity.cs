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
    public void takeHit(float damage, RaycastHit hit)
    {
        health -= damage;
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    protected void Die()
    {
        isDead = true;
        OnDeath?.Invoke();
        GameObject.Destroy(this.gameObject);
    }


}
