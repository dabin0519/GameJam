using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [Header("--Player Info--")]
    [SerializeField] private float _moveDuration;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Tilemap _groundTile;
    [SerializeField] private float _stopOffset;

    [Header("--Enrgy Info")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

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

    #endregion

    #region privateVariable

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private CircleCollider2D _circleCollider2D;

    private Vector3 _moveDir;
    private Vector2 _slopeNormalPerp;
    private Vector3 _lastPos;
    private Vector3 _currentPos;
    private float _circleColiderRadius;
    private float _slopeDownAngle;
    private float _lastSlopeAngle;
    private bool _canWalkOnSlope;
    private bool _isDie;
    private bool _isOneCall;
    private bool _isMove;
    private bool _isStop;

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
            _currentPos = transform.position;

            /*if (IsStop() && !_isStop)
            {
                _isStop = true;
                StartCoroutine(StopCoroutine());
            }*/

            SlopeCheck();
            Flip();

            if(_isMove)
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
    #endregion

    #region EnergyLogic

    [SerializeField] private float _timeDuration;
    private Vector2 _startPos;
    private int _cnt;
    public int Count => _cnt;
    private int _maxCnt;
    public int MaxCount => _maxCnt;
    private float _time;

    private void EnergyMove()
    {
        Vector3 moveDir = _moveDir * _moveSpeed * Time.deltaTime;

        if (IsCanNextTile(_moveDir))
        {
            transform.position += moveDir; // º®¿¡ ¸ØÃß¸é ¹º°¡ÇØÁÖ±â
            _lastPos = transform.position;
        }

        if(Vector2.Distance(_startPos, transform.position) >= 1f)
        {
            _startPos = transform.position;
            ++_cnt;
            _time = 0;

            if(_cnt >= _maxCnt)
            {
                _isMove = false;
                _maxCnt -= _cnt;
            }
        }
        else
        {
            _time += Time.deltaTime;

            if(_time >= _timeDuration)
            {
                StageSystem.Instance.GameLose();
            }
        }
    }

    #endregion

    #region publicMethod

    public void Jump(float jumpVelocity)
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpVelocity);
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

    private bool IsCanNextTile(Vector2 direction)
    {
        Vector3Int cellPos = _groundTile.WorldToCell(transform.position + (Vector3)direction);

        return !_groundTile.HasTile(cellPos);
    }

    private bool IsStop() => Mathf.Abs(_currentPos.x - _lastPos.x) < _stopOffset;

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
    }
#endif
}
