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

    [Header("--Anim info--")]
    [SerializeField] private float _increaseScaleY;
    [SerializeField] private float _increaseDuration;

    public UnityEvent JumpEvent;

    [SerializeField] private Vector3 _moveDir;

    private Rigidbody2D _rigidbody2D;
    private float _defultScaleY;

    public Vector3 MoveDir { get { return _moveDir; } set { _moveDir = value; } }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _defultScaleY = transform.localScale.y;
        StartCoroutine(DefualtJump());
    }

    private void Update()
    {
        Movement();
        Flip();
    }

    private void Movement()
    {
        transform.position += _moveDir.normalized * _moveSpeed * Time.deltaTime;
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

    protected virtual void OnDrawGizmos()
    {
        if (_groundChecker != null)
            Gizmos.DrawLine(_groundChecker.position,
                _groundChecker.position + new Vector3(0, -_groundCheckDistance, 0));
    }
}