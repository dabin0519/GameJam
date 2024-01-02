using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [Header("--Player info--")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private LayerMask _whatIsGround;

    [Header("--SlopeInfo--")]
    [SerializeField] private float _slopeCheckDistance;
    [SerializeField] private PhysicsMaterial2D _fullFriction;
    [SerializeField] private PhysicsMaterial2D _noFriction;
    [SerializeField] private float _maxSlopeAngle;

    [Header("--Anim info--")]
    [SerializeField] private float _increaseScaleY;
    [SerializeField] private float _increaseDuration;
    [SerializeField] private float _deathJumpForce;

    public UnityEvent JumpEvent;
    public bool Active;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private CircleCollider2D _circleCollider2D;

    private Vector3 _moveDir;
    private Vector2 _slopeNormalPerp;
    private float _defultScaleY;
    private float _circleColiderRadius;
    private float _slopeDownAngle;
    private float _lastSlopeAngle;
    private bool _canWalkOnSlope;

    public Vector3 MoveDir { get { return _moveDir; } set { _moveDir = value; } }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = transform.Find("Visual").GetComponent<Animator>();  
        _circleCollider2D = GetComponent<CircleCollider2D>();

        _defultScaleY = transform.localScale.y;
        _moveDir = new Vector3(1, 0, 0);
        _circleColiderRadius = _circleCollider2D.radius;

        StartCoroutine(DefualtJump());
    }

    private bool _isOneCall;

    private void Update()
    {
        if(Active)
        {
            SlopeCheck();
            Movement();
            Flip();
        }
        else if(!_isOneCall)
        {
            _animator.SetTrigger("Die");
            Jump(_deathJumpForce);
            StopAllCoroutines();
            _isOneCall = true;
        }
    }

    private void Movement()
    {
        transform.position += _moveDir.normalized * _moveSpeed * Time.deltaTime;
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - (Vector3)(new Vector2(0.0f, _circleColiderRadius / 2));

        SlopeCheckHorizontal(checkPos);
        SlopeCheckVertical(checkPos);
    }

    private bool _isOnSlope;
    private float _slopeSideAngle;

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, _slopeCheckDistance, _whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, _slopeCheckDistance, _whatIsGround);

        if (slopeHitFront)
        {
            _isOnSlope = true;

            _slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);

        }
        else if (slopeHitBack)
        {
            _isOnSlope = true;

            _slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            _slopeSideAngle = 0.0f;
            _isOnSlope = false;
        }

    }


    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, _slopeCheckDistance, _whatIsGround);

        if (hit)
        {
            _slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            _slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (_slopeDownAngle != _lastSlopeAngle)
            {
                _isOnSlope = true;
            }

            _lastSlopeAngle = _slopeDownAngle;

            Debug.DrawRay(hit.point, _slopeNormalPerp, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.green);

        }

        if (_slopeDownAngle > _maxSlopeAngle || _slopeSideAngle > _maxSlopeAngle)
        {
            _canWalkOnSlope = false;
        }
        else
        {
            _canWalkOnSlope = true;
        }

        if (_isOnSlope && _canWalkOnSlope /*&& xInput == 0.0f*/)
        {
            _rigidbody2D.sharedMaterial = _fullFriction;
        }
        else
        {
            _rigidbody2D.sharedMaterial = _noFriction;
        }
    }

    private void Flip()
    {
        if (_moveDir.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private IEnumerator DefualtJump()
    {
        while(true)
        {
            if(IsGroundDected())
            {
                transform.DOScaleY(_increaseScaleY, _increaseDuration);
                yield return new WaitForSeconds(_increaseDuration);
                transform.DOScaleY(_defultScaleY, _increaseDuration);
                yield return new WaitForSeconds(_increaseDuration);
                JumpEvent?.Invoke();
            }
            yield return null;
        }
    }

    public void Jump(float jumpVelocity)
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpVelocity);
        JumpEvent?.Invoke();
    }

    private bool IsGroundDected() => Physics2D.Raycast(_groundChecker.position, Vector2.down, _groundCheckDistance, _whatIsGround);

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        if (_groundChecker != null)
            Gizmos.DrawLine(_groundChecker.position,
                _groundChecker.position + new Vector3(0, -_groundCheckDistance, 0));
    }
#endif
}
