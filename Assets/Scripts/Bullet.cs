using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Bullet : MonoBehaviour
{
    private Vector3 _basepos;
    private Vector3 _basevelo = new Vector2(1.0f, 0f);
    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private float _width = 0.5f;
    private Vector3 _pos;
    private Vector3 _velo;
    public Vector3 Velo { set => _basevelo = value; }
    public Vector2 GetPos => transform.position;
    public float GetWidth => _width;

    void Start()
    {
        _basepos = Input.mousePosition;
        _pos = (_basepos - transform.position) * _speed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        transform.position += _pos.normalized;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
} 