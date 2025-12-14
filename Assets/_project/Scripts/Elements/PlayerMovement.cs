using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameDirector gameDirector;
    public PlayerAnimator playerAnimator;

    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;

    public LayerMask jumpLayerMask;
    public LayerMask lookAtLayerMask;

    public float fallSpeedMultiplier;

    private Rigidbody _rb;

    private Vector3 _direction;

    Vector3 vel;
    public float lookSmoothTime;

    public Transform shadowTransform;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (gameDirector.gameState != GameState.GamePlay)
        {
            _rb.linearVelocity = Vector3.zero;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && IsTouchingGround())
        {
            Jump();
        }
        MovePlayer(GetDirection());
        LookAtMouse();
        SetShadowPosition();
    }

    private void SetShadowPosition()
    {
        var shadowPos = transform.position;

        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out var hit, 5, jumpLayerMask)) 
        {
            shadowTransform.gameObject.SetActive(true);
            shadowPos.y = hit.point.y + .1f;
        }
        else
        {
            shadowTransform.gameObject.SetActive(false);
        }

        shadowTransform.position = shadowPos;
    }

    private void LookAtMouse()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray.origin, ray.direction, out var hit, 50, lookAtLayerMask))
        {
            var lookPoint = Vector3.SmoothDamp(transform.position + transform.forward * 2, hit.point, ref vel, lookSmoothTime);

            lookPoint.y = transform.position.y;

            transform.LookAt(lookPoint);

            var angle = Vector3.SignedAngle(_direction, lookPoint - transform.position, Vector3.up);
            playerAnimator.SetWalkDirection(angle);
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
        _direction = dir;
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

        if (gameDirector.gameState == GameState.GamePlay)
        {
            if (dir.magnitude == 0)
            {
                playerAnimator.ChangeAnimationState("Idle");
            }
            else
            {
                playerAnimator.ChangeAnimationState("Walk");
            }
        }        

        _rb.linearVelocity = dir.normalized * speed + yVelocity;
    }
}
