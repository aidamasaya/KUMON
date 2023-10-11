using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.0f;
    private Player _player;
    private float _width = 0.5f;

    void Start()
    {
        if (FindObjectOfType<Player>())
        {
            _player = FindObjectOfType<Player>();
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos -= Vector2.right * _speed * 0.25f;
        transform.position = pos;

        if (_player != null)
        {
            if (Collision())
            {
                if (_player.Life > 0)
                {
                    _player.Life--;
                    Destroy(gameObject);
                    Debug.LogFormat("Life {0}", _player.Life);
                }
            }
        }
        }

    private bool Collision()
    {
        float dx = Mathf.Abs(transform.position.x - _player.GetPos.x);
        float dy = Mathf.Abs(transform.position.y - _player.GetPos.y);
        float d = Mathf.Sqrt(dx * dx + dy * dy);
        if (d < _player.GetWidth + _width)
        {
            return true;
        }
        return false;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
