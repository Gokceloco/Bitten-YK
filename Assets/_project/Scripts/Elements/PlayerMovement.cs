using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;

    public LayerMask jumpLayerMask;
    public LayerMask lookAtLayerMask;

    public float fallSpeedMultiplier;

    private Rigidbody _rb;

    Vector3 vel;
    public float lookSmoothTime;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsTouchingGround())
        {
            Jump();
        }
        MovePlayer(GetDirection());
        LookAtMouse();
    }

    private void LookAtMouse()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out var hit, 50, lookAtLayerMask))
        {
            var lookPoint = Vector3.SmoothDamp(transform.position + transform.forward * 2, hit.point, ref vel, lookSmoothTime);

            lookPoint.y = transform.position.y;

            transform.LookAt(lookPoint);
        }
    }

    private bool IsTouchingGround()
    {
        return Physics.Raycast(transform.position + Vector3.up * .1f, Vector3.down, .2f, jumpLayerMask);
    }

    private void Jump()
    {
        _rb.linearVelocity += Vector3.up * jumpForce;
    }

    private Vector3 GetDirection()
    {
        var dir = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            dir += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir += Vector3.right;
        }

        return dir;
    }

    private void MovePlayer(Vector3 dir)
    {
        var yVelocity = _rb.linearVelocity;
        yVelocity.x = 0;
        yVelocity.z = 0;

        if (yVelocity.y < 0)
        {
            yVelocity *= fallSpeedMultiplier;
        }

        var speed = walkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }

        _rb.linearVelocity = dir.normalized * speed + yVelocity;
    }
}
