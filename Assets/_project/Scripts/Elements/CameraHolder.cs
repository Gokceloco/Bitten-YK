using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform followObject;

    Vector3 _vel;
    public float smoothTime;

    private void FixedUpdate()
    {
        var pos = followObject.position;
        pos.y = 0;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref _vel, smoothTime);
    }
}
