using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Horizontal movement")]
    public Transform CameraHolder;
    public float Speed = 1f;

    [Header("Zooming")]
    public float MinZoom = 5f;
    public float MaxZoom = 40f;
    public float ZoomSpeed = 1f;

    private bool _dragging;

    private void Update()
    {
        _dragging = Input.GetAxis("Fire3") > 0;

        if (_dragging)
        {
            MoveCameraUpdate();
        }

        if (Input.GetAxis("ScrollWheel") != 0)
        {
            ZoomUpdate();
        }
    }

    private void ZoomUpdate()
    {
        float scrollAmount = -Input.GetAxis("ScrollWheel") * Time.deltaTime * ZoomSpeed;
        float y = transform.position.y;
        float newY = Mathf.Clamp(y + scrollAmount, MinZoom, MaxZoom);

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void MoveCameraUpdate()
    {
        float moveX = -Input.GetAxis("Mouse X") * Speed;
        float moveZ = -Input.GetAxis("Mouse Y") * Speed;
        Vector3 moveVector = new Vector3(moveX, 0f, moveZ);

        CameraHolder.Translate(moveVector);
    }
}