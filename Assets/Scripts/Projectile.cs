using UnityEngine;

public class Projectile : MonoBehaviour
{

    float speed = 10;
    public void SetSpeed(float newSpeed)
    {
        this.speed = newSpeed;
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        // transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
    }
}
