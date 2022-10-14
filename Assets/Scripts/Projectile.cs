using UnityEngine;

public class Projectile : MonoBehaviour
{

    public LayerMask collisionMask;
    float speed = 10;
    float damage = 1f;
    float skinWidth = 0.1f;// variable to improve accuracy of our collision of bullets and enemies(refer sebastian lecture 7 10:01)
    float lifeTime = 3f;

    void Start()
    {
        Destroy(this.gameObject, lifeTime);
        Collider[] initialCollider = Physics.OverlapSphere(transform.position, 0.1f, collisionMask);
        if (initialCollider.Length > 0)
        {
            OnHitDamage(initialCollider[0], transform.position);
        }
    }
    public void SetSpeed(float newSpeed)
    {
        this.speed = newSpeed;
    }
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        CheckCollision(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
        // transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }

    void CheckCollision(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance + skinWidth, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitDamage(hit.collider, hit.point);
        }
    }


    void OnHitDamage(Collider collider, Vector3 hitPoint)
    {
        IDamageable damageableObject = collider.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.takeHit(damage, hitPoint, transform.forward);
        }

    }
}
