using UnityEngine;

[RequireComponent(typeof(PlayerController))]

[RequireComponent(typeof(GunController))]

public class Player : MonoBehaviour
{
    public float moveSpeed = 5;
    PlayerController controller;
    GunController gunController;
    Camera viewCamera;
    void Start()
    {
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();

        viewCamera = Camera.main;
    }


    void Update()
    {   //Movement Input
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        float magnitude = Mathf.Clamp01(moveInput.magnitude);
        Vector3 moveVelocity = moveInput.normalized * moveSpeed * magnitude;
        controller.Move(moveVelocity);


        //LookAt Input
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

        //Weapon Input
        if (Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }

    }
}
