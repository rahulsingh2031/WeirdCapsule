using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Vector3 velocity;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 velocity)
    {
        this.velocity = velocity;
    }

    public void LookAt(Vector3 lookAtPoint)
    {
        Vector3 correctedHeightPoint = new Vector3(lookAtPoint.x, transform.position.y, lookAtPoint.z);
        transform.LookAt(correctedHeightPoint);
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + this.velocity * Time.fixedDeltaTime);

    }
}
