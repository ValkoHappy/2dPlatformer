using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float MinGroundNormalY = .65f;
    [SerializeField] private float GravityModifier = 1f;
    [SerializeField] private Vector2 Velocity;
    [SerializeField] private LayerMask LayerMask;

    protected Vector2 TargetVelocity;
    protected bool Grounded;
    protected Vector2 GroundNormal;
    protected Rigidbody2D Rb2d;
    protected ContactFilter2D ContactFilter;
    protected RaycastHit2D[] HitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> HitBufferList = new List<RaycastHit2D>(16);

    protected const float MinMoveDistance = 0.001f;
    protected const float ShellRadius = 0.01f;

    private AnimatorControl _animatorControl;
    private SpriteRenderer _spriteRenderer;
    private bool _isMoving;

    private void Start()
    {
        ContactFilter.useTriggers = false;
        ContactFilter.SetLayerMask(LayerMask);
        ContactFilter.useLayerMask = true;
        Rb2d = GetComponent<Rigidbody2D>();
        _animatorControl = GetComponentInChildren<AnimatorControl>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        TargetVelocity = new Vector2(Input.GetAxis("Horizontal"), 0);
        _spriteRenderer.flipX = TargetVelocity.x < 0;
        _isMoving = TargetVelocity.x != 0;
        _animatorControl.IsMoving = _isMoving;

        if (Input.GetKey(KeyCode.Space) && Grounded)
            Velocity.y = 5;
    }

    private void FixedUpdate()
    {
        Velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
        Velocity.x = TargetVelocity.x;

        Grounded = false;

        Vector2 deltaPosition = Velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(GroundNormal.y, -GroundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Move(move, false);

        move = Vector2.up * deltaPosition.y;

        Move(move, true);
    }

    private void Move(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > MinMoveDistance)
        {
            int count = Rb2d.Cast(move, ContactFilter, HitBuffer, distance + ShellRadius);

            HitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                HitBufferList.Add(HitBuffer[i]);
            }

            for (int i = 0; i < HitBufferList.Count; i++)
            {
                Vector2 currentNormal = HitBufferList[i].normal;
                if (currentNormal.y > MinGroundNormalY)
                {
                    Grounded = true;
                    if (yMovement)
                    {
                        GroundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(Velocity, currentNormal);
                if (projection < 0)
                {
                    Velocity = Velocity - projection * currentNormal;
                }

                float modifiedDistance = HitBufferList[i].distance - ShellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        Rb2d.position = Rb2d.position + move.normalized * distance;
    }
}
