using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform pivot;
    private float radius = 10f;
    public float moveSpeed = 10f;
    public float rotateSpeed = 40f;

    private float minPitch = 40f;
    private float maxPitch = 80f;

    private Vector3 offsetPosition = new Vector3(0, 8f, 0);

    private float angle = 0f;
    private float pitch = 30f;

    private Camera cam;
    private float zoomSpeed = 40f;
    private float minFOV = 20f;
    private float maxFOV = 70f;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        HandleInput();
        UpdateCameraPosition();
    }

    private void HandleInput()
    {
        float x = Input.GetAxis("Horizontal");
        angle += x * moveSpeed * Time.deltaTime;

        float y = Input.GetAxis("Vertical");
        pitch -= y * rotateSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cam.fieldOfView -= scroll * zoomSpeed;
        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, minFOV, maxFOV);

    }

    private void UpdateCameraPosition()
    {
        float rad = angle * Mathf.Deg2Rad;

        Vector3 circlePos =
            pivot.position
            + new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * radius;

        transform.position = circlePos + offsetPosition;

        transform.LookAt(pivot.position + offsetPosition);
        transform.rotation = Quaternion.Euler(pitch, transform.eulerAngles.y, 0f);
    }
}
