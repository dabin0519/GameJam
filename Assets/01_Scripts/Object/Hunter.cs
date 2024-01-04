using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hunter : MonoBehaviour
{
    [SerializeField] private float _shootDistance;
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private bool _isRight;
    [SerializeField] private HunterBullet _shootObj;
    [SerializeField] private float _shootTime;

    public UnityEvent ShootEvent;

    public bool Active { get; set; } = false;

    private LineRenderer _lineRenderer;
    private Animator _animator;

    private Vector2 _shootDir;
    private float _time;

    private void Awake()
    {
        _lineRenderer = transform.Find("Line").GetComponent<LineRenderer>();
        _animator = transform.Find("Visual").GetComponent<Animator>();

        _shootDir = _isRight ? Vector2.right : -Vector2.right;
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, new Vector3(transform.position.x + _shootDir.x * _shootDistance, transform.position.y));
    }

    private void Start()
    {
        StageSystem.Instance.OnStartEvt += () => Active = true;

        transform.Find("Visual").GetComponent<SpriteRenderer>().flipX = !_isRight;
    }

    private void Update()
    {
        if (!Active) return;

        if(PlayerDetected())
        {
            _time += Time.deltaTime;
        }
        else
        {
            _time = 0;
        }

        if(_time >= _shootTime)
        {
            Shoot();
            _time = 0;
        }

        LineLogic();
    }

    private void Shoot()
    {
        ShootEvent?.Invoke();


        HunterBullet newBullet = Instantiate(_shootObj, transform.position, Quaternion.identity);

        newBullet.Dir = _shootDir;
    }

    private void LineLogic()
    {
        if(PlayerDetected())
        {
            _lineRenderer.startColor = Color.red;
            _lineRenderer.endColor = Color.red;
        }
        else
        {
            _lineRenderer.startColor = Color.white;
            _lineRenderer.startColor = Color.white;
        }
    }

    private bool PlayerDetected() => Physics2D.Raycast(transform.position, _shootDir, _shootDistance, _whatIsPlayer);

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //Debug.DrawRay(transform.position, Vector2.right * _shootDistance, Color.red);
        Gizmos.DrawRay(transform.position, Vector2.right * _shootDistance);
    }
#endif
}
