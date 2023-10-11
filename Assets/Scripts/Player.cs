using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private EnemyBullet _ebullet = null;
    [SerializeField]
    private int _initialLife = 5;
    [SerializeField]
    private GameObject _f = null;
    [SerializeField]
    private float _movePower = 10f;
    [SerializeField]
    private float _jumpPower = 10f;
    [SerializeField]
    private GameObject _bulletPrefab = null;
    [SerializeField]
    private Transform _muzzle = null;
    [SerializeField]
    private float _interval = 0.5f;
    [SerializeField] 
    private float _gravityDrag = -0.003f;
    [SerializeField, Range(0, 1.0f)]
    private float _width = 0.5f;

    private float _timer;
    private int _life;
    public int Life { get => _life; set => _life = value; }
    private int _jumpCount = 0;

    private Vector2 _basePos = new Vector2(-3f, -4f);
    private Vector2 _pos;
    private Vector2 _baseVelo = new Vector2(0.0f, 0.3f);
    private Vector2 _velo;

    private float _radian;
    private Vector3 _point;
    public Vector3 Point => _point;
    private float _fvelo = 0.15f;
    private bool _jump = false;
    public float GetWidth => _width;
    public Vector2 GetPos => _pos;

    void Start()
    {
        _pos = _basePos;
        _velo = _baseVelo;
        transform.position = _pos;
        _life = _initialLife;
    }

    void Update()
    {
        //マウス検知
        _point = Input.mousePosition;
        _radian = Mathf.Atan2(_point.y, _point.x);
        transform.rotation = Quaternion.AngleAxis(_radian * 180 / Mathf.PI, new Vector3(0, 0, 1));

        //ジャンプ
        if (Input.GetButtonDown("Jump") && !_jump && _jumpCount < 2)
        {
            _jumpCount++;
            _jump = true;
        }

        if (_jump && Mathf.Abs(_f.transform.position.y - _pos.y) >= 1.0f)
        {
            _pos += _velo * 0.25f;
            _velo.y += _gravityDrag;
        }
        else
        {
            _jump = false;
            _jumpCount = 0;
            _pos.y = -4.0f;
            _velo = _baseVelo;
        }

        _timer += Time.deltaTime;
        if (_timer > _interval)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _timer = 0;
                Instantiate(_bulletPrefab, _muzzle.position, transform.rotation);
            }
        }
    }

    //移動
    void FixedUpdate()
    {
        Vector2 velo = new Vector2(0.0f, 0.0f);
        velo.x = Input.GetAxis("Horizontal") * _fvelo;
        float fInputVel = Mathf.Sqrt(velo.x * velo.x);
        if(fInputVel > _fvelo)
        {
            velo = velo / fInputVel * _fvelo;
        }
        _pos += velo;

        transform.position = _pos;
    }
}
