using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform followObject;

    Vector3 _vel;
    public float smoothTime;

    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, followObject.position, ref _vel, smoothTime);
    }
}
