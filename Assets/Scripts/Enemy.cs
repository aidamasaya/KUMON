using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int _initiallife = 3;
    [SerializeField,Range(0,1.0f)]
    private float _width;
    [SerializeField]
    private GameObject _bulletPrefab = null;
    [SerializeField]
    private Player _player = null;
    [SerializeField, Range(0, 5.0f)]
    private float _distance = 0;
    [SerializeField]
    private Transform _muzzle = null;
    private float _interval = 1.0f;
    private float _timer;
    [SerializeField]
    private Vector2 _basePos = new Vector2(3f, -4f);
    private Vector2 _pos;
    [SerializeField]
    private Vector2 _baseVelo = new Vector2(0, 0.1f);
    private Vector2 _velo;
    private int _life = 0;
    public int Life { get => _life; set => _life = value; }
    public Vector2 GetPos => _pos;
    public float GetWidth => _width;
    private Bullet _bullet;

    void Start()
    {
        _pos = _basePos;
        _velo = _baseVelo;
        _life = _initiallife;
        transform.position = _pos;
    }

    
    void Update()
    {
        _timer += Time.deltaTime;
        if(Mathf.Abs(_player.transform.position.x - _pos.x) < _distance)
        {
            if (_timer >= _interval)
            {
                _timer = 0;
                Instantiate(_bulletPrefab, _muzzle.transform.position, Quaternion.identity);
            }
        }

        if (FindObjectOfType<Bullet>())
        {
            _bullet = FindObjectOfType<Bullet>();
            if (Collision())
            {
                if (_life > 0)
                {
                    _life--;
                    Debug.LogFormat("E_Life {0}", _life);
                }
                else
                {
                    Destroy(gameObject);
                }
                Destroy(_bullet.gameObject);
            }
        }
    }

    private bool Collision()
    {
        float dx = Mathf.Abs(_pos.x - _bullet.GetPos.x);
        float dy = Mathf.Abs(_pos.y - _bullet.GetPos.y);
        float d = Mathf.Sqrt(dx * dx + dy * dy);
        if (d <= _width + _bullet.GetWidth)
        {
            return true;
        }
        return false;
    }
}
