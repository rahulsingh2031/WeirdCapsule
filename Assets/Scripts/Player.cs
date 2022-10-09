using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    public float moveSpeed = 5;
    PlayerController controller;
    Camera viewCamera;
    void Start()
    {
        controller = GetComponent<PlayerController>();
        viewCamera = Camera.main;
    }


    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        float magnitude = Mathf.Clamp01(moveInput.magnitude);
        Vector3 moveVelocity = moveInput.normalized * moveSpeed * magnitude;
        controller.Move(moveVelocity);

        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        //just making a plane at (0,0,0) for ray intersection
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            // Debug.DrawLine(ray.origin, point, Color.red);

            controller.LookAt(point);
        }
    }
}
