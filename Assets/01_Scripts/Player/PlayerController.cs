using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [Header("--Player Info--")]
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Transform  _wallChecker;
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private Tilemap _groundTile;
    [SerializeField] private float _timeDuration;
    [SerializeField] private float _fallCheckDistance;

    [Header("--Enrgy Info")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _airMoveSpeed;

    [Header("--Slope Info--")]
    [SerializeField] private float _slopeCheckDistance;
    [SerializeField] private PhysicsMaterial2D _fullFriction;
    [SerializeField] private PhysicsMaterial2D _noFriction;
    [SerializeField] private float _maxSlopeAngle;

    [Header("--Anim info--")]
    [SerializeField] private float _increaseScaleY;
    [SerializeField] private float _increaseDuration;
    [SerializeField] private float _deathJumpForce;

    #region Property

    public UnityEvent JumpEvent;
    public bool Active { get; set; } = false;
    public Vector3 MoveDir { get { return _moveDir; } set { _moveDir = value; } }
    public bool GoldKey = false;
    public bool SilverKey = false;
    public int Count => _cnt;
    public int MaxCount => _maxCnt;
    public bool JumpPad;

    #endregion

    #region privateVariable

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private CircleCollider2D _circleCollider2D;

    private Vector3 _moveDir;
    private Vector2 _slopeNormalPerp;
    private Vector2 _startPos;
    private float _circleColiderRadius;
    private float _slopeDownAngle;
    private float _lastSlopeAngle;
    private bool _canWalkOnSlope;
    private bool _isDie;
    private bool _isOneCall;
    private bool _isMove;
    private int _cnt;
    private int _maxCnt;
    private float _time;

    #endregion

    #region GameLogic

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = transform.Find("Visual").GetComponent<Animator>();
        _circleCollider2D = GetComponent<CircleCollider2D>();

        _moveDir = new Vector3(1, 0, 0);
        _circleColiderRadius = _circleCollider2D.radius;
    }

    private void Start()
    {
        StageSystem.Instance.OnStartEvt += OnStart;
    }

    private void Update()
    { 
        if (Active)
        {
            SlopeCheck();
            Flip();
            FallCheck();

            if(_isMove && !JumpPad)
            {
                EnergyMove();
            }
        }
        else if(!_isOneCall && _isDie)
        {
            _animator.SetTrigger("Die");
            Jump(_deathJumpForce);
            _isOneCall = true;
        }
        
        if(!Active)
            StopAllCoroutines();
    }

    public void OnStart()
    {
        StartCoroutine(DefualtJump());

        Movement(9);
        Active = true;
    }

    private bool _isFallOneCall;
    
    private void FallCheck()
    {
        if(transform.position.y <= _fallCheckDistance && !_isFallOneCall)
        {
            _isFallOneCall = true;
            StageSystem.Instance.GameLose();
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

    public void Movement(int r)
    {
        _cnt = 0;
        _maxCnt += r;
        if(_maxCnt > 15) _maxCnt = 15;
        _isMove = true;
        _startPos = transform.position;
    }
    #endregion

    #region Coroutine
    private IEnumerator DefualtJump()
    {
        while(true)
        {
            if(IsGroundDected() && _isMove)
            {
                Jump(_jumpForce);
            }
            yield return null;
        }
    }

    private IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(_timeDuration);
        StageSystem.Instance.GameLose();
    }
    #endregion

    #region EnergyLogic

    private bool _isStageOneCall;

    private void EnergyMove()
    {
        if(!IsWallDected())
        {
            float moveSpeed = _isJump ? _airMoveSpeed : _moveSpeed;

            transform.position += moveSpeed * Time.deltaTime * _moveDir; // ���� ���߸� �������ֱ�
        }

        if(Mathf.Abs(_startPos.x - transform.position.x) >= 1f)
        {
            _startPos = transform.position;
            ++_cnt;
            _time = 0;

            if(_cnt >= _maxCnt)
            {
                _isMove = false;
                StartCoroutine(WaitCoroutine());
                _maxCnt -= _cnt;
            }
        }
        else
        {
            _time += Time.deltaTime;

            if(_time >= _timeDuration)
            {
                if(!_isStageOneCall)
                {
                    _isStageOneCall = true;
                    StageSystem.Instance.GameLose();
                }
            }
        }
    }

    #endregion

    #region publicMethod

    private bool _isJump;

    public void Jump(float jumpVelocity, bool isJump = false)
    {
        if(jumpVelocity == _jumpForce && _rigidbody2D.velocity == new Vector2(0, 7))
            return;

        _isJump = isJump;

        _rigidbody2D.velocity = new Vector2(0, jumpVelocity);
        JumpEvent?.Invoke();
    }

    public void Die()
    {
        _isDie = true;
        Active = false;
    }

    #endregion

    #region boolMethod

    private bool IsGroundDected() => Physics2D.Raycast(_groundChecker.position, Vector2.down, _groundCheckDistance, _whatIsGround);

    private bool IsWallDected() => Physics2D.Raycast(_wallChecker.position, _moveDir, _wallCheckDistance, _whatIsGround);

    #endregion

    #region Slope

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

    #endregion

#if UNITY_EDITOR
    protected virtual void OnDrawGizmos()
    {
        if (_groundChecker != null)
            Gizmos.DrawLine(_groundChecker.position,
                _groundChecker.position + new Vector3(0, -_groundCheckDistance, 0));
        if (_wallChecker != null)
            Gizmos.DrawLine(_wallChecker.position,
                _wallChecker.position + new Vector3(_wallCheckDistance, 0, 0));
    }
#endif
}
